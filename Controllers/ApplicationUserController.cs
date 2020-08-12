using App_NetCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetCoreApi.Models;
using NetCoreApi.Models.Request;
using NetCoreApi.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using System.Net.Http;

namespace NetCoreApi.Controllers
{
    [ApiController]
    [Route("ApplicationUser")]
    public class ApplicationUserController : Controller
    {
        private readonly ILogger<ApplicationUserController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Constructor por inyección de dependencias
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public ApplicationUserController(ILogger<ApplicationUserController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }



        /// <summary>
        /// Registra un usuario
        /// </summary>
        /// <param name="registerAccountViewModels"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> RegisterAsync(RegistrarUsuarioRequest request)
        {
            if (request == null) return false;
            //Regla 1: Se deberá validad que no exista otro usuario con los mismos datos.
            if (_context.Set<ApplicationUser>().FirstOrDefault(x => x.UserName.Equals(request.Username)) != null) throw new Exception("El nombre de usuario ya esta siendo usado por otro usuario.");
            if(!string.IsNullOrEmpty(request.Email) && !RegexUtils.IsValidEmail(request.Email))            
                throw new Exception("Correo electrónico no valído.");
            //Regla 2: La contraseña se deberá validar que coincida en dos campos.
            if (!request.Password.Equals(request.PasswordRetype))
                throw new Exception("Contraseña no coincide.");


            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FechaCreacion = DateTime.Now, /*Creación*/
            };
            ///Regla 3: La contraseña deberá estar encriptada.
            ///Identity usa un algoritmo para encriptar las contraseñas
            //Regla 4: El id deberá ser generado automáticamente por la tabla de SQL.
            //Identity autogenera las pwd como GUID
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return true;
            }
            if (result.Errors != null)
                throw new Exception(string.Join(", ", result.Errors));
            return false;
        }

        /// <summary>
        /// Consulta de todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public IActionResult GetAll(DataSourceLoadOptions options)
        {
            //return _context.Set<ApplicationUser>().ToList();
            return Ok(DataSourceLoader.Load(_context.Set<ApplicationUser>(), options));
            //return _context.Set<ApplicationUser>().ToList();
        }

        /// <summary>
        /// Servicio que actualiza la información de un usuario, en caso de querer actualziar la contraseña
        ///     es necesaria la contraseña actual y nueva.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> EditAsync(string userName, EditarUsuarioRequest request)
        {
            bool centinela = false;
            var user = _context.Set<ApplicationUser>().FirstOrDefault(x => x.UserName.Equals(userName));
            if (user != null)
            {
                if (!string.IsNullOrEmpty(request.Email) && !RegexUtils.IsValidEmail(request.Email))
                    throw new Exception("Correo electrónico no valído.");

                user.Email = request.Email;
                user.Sexo = request.Gender;
                IdentityResult result = null;
                if (!string.IsNullOrEmpty(request.CurrentPassword) && !string.IsNullOrEmpty(request.NewPassword))
                    result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                centinela = await _context.SaveChangesAsync() > 0 || result.Succeeded;
            }
            return centinela;
        }

        /// <summary>
        /// Desactiva al usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteAsync(string userName)
        {
            //Regla 5: La acción eliminar solo inactiva los usuarios.
            bool centinela = false;
            var user = _context.Set<ApplicationUser>().FirstOrDefault(x => x.UserName.Equals(userName));
            if (user != null)
            {
                var result= await _userManager.SetLockoutEnabledAsync(user, true);
                centinela = result.Succeeded;
            }
            return centinela;
        }

        /// <summary>
        /// Servicio de inicio de sesión implementado con JWT
        /// En caso de devolver token el inicio de sesión es satisfactorio.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<string> LoginAsync(LoginRequest request)
        {
            string centinela = string.Empty;
            // Tu código para validar que el usuario ingresado es válido

            // Asumamos que tenemos un usuario válido
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, request.Rememberme, true);
            if (result.Succeeded)
            {


                // Leemos el secret_key desde nuestro appseting
                var secretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                // Creamos los claims (pertenencias, características) del usuario
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, request.Username),
                new Claim(ClaimTypes.Email, request.Username)
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    // Nuestro token va a durar un día
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                centinela = tokenHandler.WriteToken(createdToken);
            }
            //Se elabora el proceso de refresh token si se elige rememberme
            if (request.Rememberme){
                          var rToken = RefreshToken.GenerateRefreshToken(ipAddress());
            var userModify = _context.Set<ApplicationUser>().FirstOrDefault(a => a.UserName == request.Username);
            rToken.UserId = userModify.Id;
            userModify.RefreshTokens.Add(rToken);
            _context.Update(userModify);
            _context.SaveChanges();

            Response.Cookies.Append("refreshToken", rToken.Token, new CookieOptions(){
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            }
  
            return centinela;
        }
        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshTokenRequest(){
            var refreshToken = Request.Cookies["refreshToken"];
            var response = RefreshToken.Refresh(ipAddress(), refreshToken, _context, out var user);
            if (response == null) return Unauthorized(new {Mensaje = "Token Inválido"});

            var secretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                // Creamos los claims (pertenencias, características) del usuario
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.UserName)
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    // Nuestro token va a durar un día
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                var centinela = tokenHandler.WriteToken(createdToken);


            Response.Cookies.Append("refreshToken", response.Token, new CookieOptions(){
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            });
            return Ok(new {Jwt = centinela});
        }
        [HttpGet]
        [Route("GetGeneros")]
        [Authorize]
        public IActionResult GetGeneros(DataSourceLoadOptions options){
            return Ok(DataSourceLoader.Load(Enum.GetValues(typeof(Genero)).Cast<Genero>().Select(g => new SelectListUtil<int>(){
                name = g.ToString(),
                value = (int) g
            }), options));
        }
        private string ipAddress(){
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();


        }
    }
}
