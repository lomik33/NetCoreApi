using App_NetCore.Data;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Models;
using System;
using System.ComponentModel;
using System.Linq;

namespace NetCoreApi.Controllers{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmpleadoController : Controller {
        private readonly ApplicationDbContext _context;
        public EmpleadoController(ApplicationDbContext context){
            _context = context;
        }
        [HttpPost]
        public IActionResult GetEmpleados(DataSourceLoadOptions options){
            return Ok(DataSourceLoader.Load(_context.Set<Empleado>().Include(e => e.InformacionContrato), options));
        }
        [HttpGet]
        public IActionResult GetEstadosCiviles(){
            var lista = Enum.GetValues(typeof(EdoCivil)).Cast<EdoCivil>().Select(e => new SelectListUtil<int>(){
                name = e.ToString(),
                value = (int) e
            }).ToList();
            return Ok(lista);
        }
        [HttpGet]
        public IActionResult GetTiposContratos(){
            return Ok(Enum.GetValues(typeof(TipoContrato)).Cast<TipoContrato>().Select(c => new SelectListUtil<int>(){
                name = c.ToString(),
                value = (int) c
            }));
        }
        [HttpPost]
        public int AgregarEmpleado(Empleado nuevo){
            _context.Set<Empleado>().Add(nuevo);
            return _context.SaveChanges();
        }
        [HttpPut]
        public int EditarEmpleado(Guid key, Empleado editado){
            Empleado encontrado = _context.Set<Empleado>().Include(e => e.InformacionContrato).FirstOrDefault(e => e.EmpleadoId == key);
            encontrado.Nombre = editado.Nombre;
            encontrado.ApellidoPaterno = editado.ApellidoPaterno;
            encontrado.ApellidoMaterno = editado.ApellidoMaterno;
            encontrado.FechaNacimiento = editado.FechaNacimiento;
            encontrado.Genero = editado.Genero;
            encontrado.Rfc = editado.Rfc;
            encontrado.Nss = editado.Nss;
            encontrado.EstadoCivil = editado.EstadoCivil;
            encontrado.InformacionContrato.Puesto = editado.InformacionContrato.Puesto;
            encontrado.InformacionContrato.PuestoDescripcion = editado.InformacionContrato.PuestoDescripcion;
            encontrado.InformacionContrato.TipoContrato = editado.InformacionContrato.TipoContrato;
            encontrado.InformacionContrato.FechaContratacion = editado.InformacionContrato.FechaContratacion;
            encontrado.InformacionContrato.FechaTermino = editado.InformacionContrato.FechaTermino;
            encontrado.InformacionContrato.Salario = editado.InformacionContrato.Salario;
            return _context.SaveChanges();
        }
        [HttpDelete]
        public int BorrarEmpleado(Guid key){
            Empleado encontrado = _context.Set<Empleado>().Find(key);
            _context.Set<Empleado>().Remove(encontrado);
            return _context.SaveChanges();
        }
    }
}