using System;
namespace NetCoreApi.Models{
    public class UnidadMedida{
        public Guid UnidadDeMedidaId {get; set;}
        public string NombreUnidad {get; set;}
        public string AbreviacionUnidad {get; set;}
        public MagnitudesUnidad TipoUnidad {get; set;}
    }
    public enum MagnitudesUnidad{
        Masa, Longitud, Capacidad, Superficie, Volumen, Temperatura, Tiempo
    }
}