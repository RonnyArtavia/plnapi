using System;
using System.Collections.Generic;

#nullable disable

namespace PLN.API.Models
{
    public partial class Distelec
    {
        public Distelec()
        {
            Padrons = new HashSet<Padron>();
        }

        public int Codele { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }

        public virtual ICollection<Padron> Padrons { get; set; }
    }
}
