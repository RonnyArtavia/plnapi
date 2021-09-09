using System;
using System.Collections.Generic;

#nullable disable

namespace PLN.API.Models
{
    public partial class Plane
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string Plan { get; set; }
        public string Telco { get; set; }
        public bool? AceptaTerminos { get; set; }
        public DateTime? FechaAceptaTerminos { get; set; }
        public bool? RecargaRealizada { get; set; }
        public DateTime? Modificado { get; set; }
        public string Ubicacion { get; set; }
    }
}
