using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

#nullable disable

namespace PLN.API.Models
{
    public partial class Padron
    {
        public int Cedula { get; set; }
        public int Codelec { get; set; }
        public string Relleno { get; set; }
        public int Fechacaduc { get; set; }
        public string Junta { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        [NotMapped]
        public string NombreCompleto { 
            get
            {
                // Creates a TextInfo based on the "en-US" culture.
                TextInfo myTI = new CultureInfo("es-CR", false).TextInfo;
                return $"{myTI.ToTitleCase(Nombre.Trim().ToLower())} {myTI.ToTitleCase(Apellido1.Trim().ToLower())} {myTI.ToTitleCase(Apellido2.Trim().ToLower())} ";
            }
        }
        [NotMapped]
        public string NombreCase
        {
            get
            {
                // Creates a TextInfo based on the "en-US" culture.
                TextInfo myTI = new CultureInfo("es-CR", false).TextInfo;
                return $"{myTI.ToTitleCase(Nombre.Trim().ToLower())}";
            }
        }
        public virtual Distelec CodelecNavigation { get; set; }
    }
}
