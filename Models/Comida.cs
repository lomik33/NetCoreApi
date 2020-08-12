using System;
using System.Xml.Serialization;

namespace NetCoreApi.Models{
    public class Comida{

        public Guid ComidaId {get; set;}
        public string NombreComida {get; set;}

        public TiposComida TipoComida {get; set;}
        public decimal PrecioComida {get; set;}
        public string DescripcionComida {get; set;}
        

    }
    public enum TiposComida{
        Platillo, Bebida, Café, Torta, Golosina, Postre, Antojito, Cerveza, Entrada, Snack, Caldo, Ingrediente, Fruta, Verdura, Carne
    }
    

}