using System;
using App_NetCore.Data;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace NetCoreApi.Controllers{
    [ApiController]
    [Authorize]
    [Route("UnidadMedida")]
    public class UnidadMedidaController : Controller{
                private readonly ApplicationDbContext _context;
                
                public UnidadMedidaController(ApplicationDbContext context){
                    _context = context;
                }
        [HttpGet]
        public IActionResult GetUnidadesMedida(DataSourceLoadOptions options){
            return Ok(DataSourceLoader.Load(_context.Set<UnidadMedida>(), options));
        }
        [HttpPost]
        public IActionResult InsertarUnidadMedida([FromForm] string values){
            UnidadMedida unidadNueva = new UnidadMedida();
            JsonConvert.PopulateObject(values, unidadNueva);
            if (!TryValidateModel(unidadNueva)){
                return BadRequest();
            }
            _context.Set<UnidadMedida>().Add(unidadNueva);
            _context.SaveChanges();
            return Ok(unidadNueva);
        }
        [HttpPut]
        public IActionResult ModificarUnidadMedida([FromForm] Guid key, [FromForm]string values){
            var unidad = _context.Set<UnidadMedida>().Find(key);
            JsonConvert.PopulateObject(values, unidad);
            if (!TryValidateModel(unidad)){
                return BadRequest();
            }
            _context.SaveChanges();
            return Ok(unidad);
        }
        [HttpDelete]
        public IActionResult EliminarUnidadMedida([FromForm] Guid key){
                        var unidad = _context.Set<UnidadMedida>().Find(key);
                        _context.Set<UnidadMedida>().Remove(unidad);
                        _context.SaveChanges();
                        return NoContent();
        }
        [HttpGet]
        [Route("GetTiposUnidades")]
        public IEnumerable<SelectListUtil<int>> GetTiposUnidades(){
            return Enum.GetValues(typeof(MagnitudesUnidad)).Cast<MagnitudesUnidad>().Select(d => new SelectListUtil<int>(){
                name = d.ToString(),
                value = (int) d
            });
        
        }

    }
}