using System;
using System.Collections.Generic;
using System.Text;

namespace ParaLeitura4.Models
{
    class ClasseNacional
    {
        public Guid Id { get; set; }
        public string CodigoClasseNacional { get; set; }
        public string CodigoSubClasseNacional { get; set; }
        public string Especificacao { get; set; }
    }
}
