using System;
using System.Xml.Serialization;

namespace NetCoreApi.Models{
    public class ComidaEditRequest{

        public Guid? ComidaId {get; set;}
        public string NombreComida {get; set;}

        public TiposComida? TipoComida {get; set;}
        public Decimal PrecioComida {get; set;}
        public string DescripcionComida {get; set;}
        

    }
}