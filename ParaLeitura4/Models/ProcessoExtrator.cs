using System;
using System.Collections.Generic;
using System.Text;

namespace ParaLeitura4.Models
{
    class ProcessoExtrator
    {
        public Guid Id { get; set; }
        public string NumeroProcesso { get; set; }
        public DateTime DataDeposito { get; set; }
        public DateTime DataConcessao { get; set; }
        public DateTime DataVigencia { get; set; }
        public string NomeProcurador { get; set; }
        public string Apostila { get; set; }
        public string Situacao { get; set; }
        public string ApresentacaoMarca { get; set; }
        public string NaturezaMarca { get; set; }
        public string NomeMarca { get; set; }
        public string TraducaoMarca { get; set; }
        public List<ClassesVienna> ClassesViennas { get; set; }
        public List<Despachos> Despachos { get; set; }
        public List<Titulares> Titulares { get; set; }
        public List<ClasseNacional> ClassesNacionais { get; set; }
        public List<Peticoes> Peticoes { get; set; }
    }
}
