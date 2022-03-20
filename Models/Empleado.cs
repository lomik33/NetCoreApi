using System;
using System.ComponentModel;
using EmpleadoDefinicion.Interfaces;

namespace NetCoreApi.Models{
    public class Empleado : IPersona, IIdentidadLaboral, IDateObserver, ISoftDelete
    {
        public Guid EmpleadoId {get; set;}
        public string Nombre {get; set;}
        public string ApellidoPaterno {get; set;}
        public string ApellidoMaterno {get; set;}
        public DateTime FechaNacimiento {get; set;}
        public Genero Genero {get; set;}
        public string Rfc {get; set;}
        public string Nss {get; set;}
        public EdoCivil EstadoCivil {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime? ModifiedAt {get; set;}
        public bool IsDeleted {get; set;}
        public DateTime? DateDeleted {get; set;}
        public virtual InformacionContrato InformacionContrato {get; set;}
    }

    public enum EdoCivil{
        [Description("Soltero/a")]
        Soltero,
        [Description("Casado/a")]
        Casado,
        [Description("Divorciado/a")]
        Divorciado,
        [Description("Viudo/a")]
        Viudo,
        [Description("Unión libre")]
        UnionLibre
    }
    
}