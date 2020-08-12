using System;
using System.Collections.Generic;
using System.Linq;
using App_NetCore.Data;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Models;

namespace NetCoreApi.Controllers{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize]
    public class ComidaController : Controller{
        private readonly ApplicationDbContext _context;
        private readonly ITrackingEntityService<Comida, ComidaEditRequest> _trackingService = new ComidaTracking();
        public ComidaController(ApplicationDbContext context){
            _context = context;
        }
        [HttpPost]
        public LoadResult GetComida(DataSourceLoadOptions options){
            return DataSourceLoader.Load(_context.Set<Comida>(), options);
        }
        [HttpPost]
        public int AddComida(Comida values){
            _context.Set<Comida>().Add(values);
            return _context.SaveChanges();
            //return Ok(new {values, key=values.ComidaId});
        }
        [HttpPut]
        public int EditComida(Guid key, Comida values){
            var comidaEncontrada = _context.Set<Comida>().Find(key);
            comidaEncontrada.NombreComida = values.NombreComida;
            comidaEncontrada.DescripcionComida = values.DescripcionComida;
            comidaEncontrada.TipoComida = values.TipoComida;
            comidaEncontrada.PrecioComida = values.PrecioComida;
            //_trackingService.GetModifiedResult(ref comidaEncontrada, _trackingService.GetChanges(comidaEncontrada, values));
            return _context.SaveChanges();
            //return Ok(values);
        }
        [HttpDelete]
        public int DeleteComida(Guid key){
            var comidaEncontrada = _context.Set<Comida>().Find(key);
            _context.Set<Comida>().Remove(comidaEncontrada);
            return _context.SaveChanges();
            //return NoContent();
        }
        [HttpGet]
        public IEnumerable<SelectListUtil<int>> GetTiposComida(){
            return Enum.GetValues(typeof(TiposComida)).Cast<TiposComida>().Select(t => new SelectListUtil<int>(){
                name = t.ToString(),
                value = (int) t
            });
        }
    }
}