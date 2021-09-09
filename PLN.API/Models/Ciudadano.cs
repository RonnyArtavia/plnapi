using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLN.API.Models
{
    public class Ciudadano
    {
        public int Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public int Fechacaduc { get; set; }
        public string Junta { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }

    }
}
