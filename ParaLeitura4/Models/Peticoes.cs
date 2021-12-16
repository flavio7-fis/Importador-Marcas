using System;
using System.Collections.Generic;
using System.Text;

namespace ParaLeitura4.Models
{
    class Peticoes
    {
        public Guid Id { get; set; }
        public string PeticaoProtocolo { get; set; }
        public DateTime PeticaoData { get; set; }
        public string PeticaoServico { get; set; }
        public string PeticaoNome { get; set; }
    }
}
