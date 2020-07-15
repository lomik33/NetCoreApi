using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Models.Request
{
    /// <summary>
    /// Modelo que abstrae las propiedades necesarias para registrar un usuario
    /// </summary>
    public class RegistrarUsuarioRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public Genero Gender { get; set; }
    }
}
