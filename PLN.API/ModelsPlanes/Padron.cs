using System;
using System.Collections.Generic;

#nullable disable

namespace PLN.API.ModelsPlanes
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

        public virtual Distelec CodelecNavigation { get; set; }
    }
}
