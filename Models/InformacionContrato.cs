using System;
using EmpleadoDefinicion.Interfaces;

namespace NetCoreApi.Models{
    public class InformacionContrato : IDateObserver, ISoftDelete
    {
        public Guid InformacionContratoId {get; set;}
        public string Puesto {get; set;}
        public string PuestoDescripcion {get; set;}
        public TipoContrato TipoContrato {get; set;}
        public DateTime FechaContratacion {get;set;}
        public DateTime? FechaTermino {get; set;}
        public decimal Salario {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime? ModifiedAt {get; set;}
        public bool IsDeleted {get; set;}
        public DateTime? DateDeleted {get; set;}
    }
    public enum TipoContrato{
        Indefinido, Temporal, Obra, Interinidad, Beca, Practicas
    }
}