using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Models
{
    public enum Genero { Masculino, Femenino }
    /// <summary>
    /// Clase que define la entidad de usuarios, hereda de Identity
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public Genero Sexo { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
