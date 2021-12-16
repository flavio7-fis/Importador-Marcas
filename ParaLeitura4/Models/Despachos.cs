using System;
using System.Collections.Generic;
using System.Text;

namespace ParaLeitura4.Models
{
    class Despachos
    {
        public Guid Id { get; set; }
        public string Rpi { get; set; }
        public DateTime DataRpi { get; set; }
        public string DespachoRpi { get; set; }
        public string TextoComplementar { get; set; }
    }
}
