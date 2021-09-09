using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace PLN.API.Models
{
    public partial class PlanesPiloto
    {
        public string Nombre { get; set; }
        public long Telefono { get; set; }
        public string Cedula { get; set; }
        public string Plan { get; set; }
        public string Telco { get; set; }
        public bool? AceptaTerminos { get; set; }
        public DateTime? FechaAceptaTerminos { get; set; }
        public bool? RecargaRealizada { get; set; }
        public DateTime? Modificado { get; set; }
        public string Ubicacion { get; set; }
        public string Correo { get; set; }
        public string NombrePadron { get; set; }
        [NotMapped]
        public string token { get; set; }
    }
}
