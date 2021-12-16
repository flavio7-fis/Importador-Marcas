using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ParaLeitura4.Models;

namespace ParaLeitura4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Criando a lista de processos
            List<ProcessoExtrator> processoExtrator = new List<ProcessoExtrator>();

            XmlDocument xml = new XmlDocument();
            string filePath = @"c:\\Xml\\2021-04-20-08-31-11.xml";
            XElement exemploDeDados = XElement.Load(filePath);
            

            ///var t = exemploDeDados.Descendants("processo").ToList()[0.Descendants("titulares").Descendants("titular").ToList().Select(x => (string)x.Attribute("nome-razao-social")).ToList()[0];
            for (int i = 0; i < exemploDeDados.Descendants("processo").ToList().Count; i++)
            {

                //instanciando as listas
                ProcessoExtrator processoExtrator1 = new ProcessoExtrator();
                processoExtrator1.ClassesViennas = new List<ClassesVienna>();
                processoExtrator1.Despachos = new List<Despachos>();
                processoExtrator1.Titulares = new List<Titulares>();
                processoExtrator1.ClassesNacionais = new List<ClasseNacional>();
                processoExtrator1.Peticoes = new List<Peticoes>();

                //instaciando Classes
                DadosMadri dadosMadri = new DadosMadri();


                Console.WriteLine("---------------------------------------------- processo " + (i + 1)  + " ----------------------------------------------");
                var obj = exemploDeDados.Descendants("processo").ToList()[i];

                if (obj.Attribute("numero") != null)
                {
                    string numero_ = obj.Attribute("numero").Value;
                    Console.WriteLine("NUMERO DO PROCESSO: " + numero_);

                    processoExtrator1.NumeroProcesso = numero_;
                }

                if (obj.Attribute("data-deposito") != null)
                {
                    string dataDep_ = obj.Attribute("data-deposito").Value;
                    Console.WriteLine("Data de depósito: " + dataDep_);

                    DateTime dataDep_DateTime = Convert.ToDateTime(dataDep_);
                    processoExtrator1.DataDeposito = dataDep_DateTime;
                }

                if(obj.Attribute("data-concessao") != null)
                {
                    string dataConce_ = obj.Attribute("data-concessao").Value;
                    Console.WriteLine("Data de concessão: " + dataConce_);

                    DateTime dataConce_DateTime = Convert.ToDateTime(dataConce_);
                    processoExtrator1.DataConcessao = dataConce_DateTime;
                }
                if(obj.Attribute("data-vigencia") != null)
                {
                    string dataVigen_ = obj.Attribute("data-vigencia").Value;
                    Console.WriteLine("Data de vingência: " + dataVigen_);

                    DateTime dataVigen_DateTime = Convert.ToDateTime(dataVigen_);
                    processoExtrator1.DataVigencia = dataVigen_DateTime;
                }

                if (obj.Descendants("procurador").ToList().Count > 0)
                {

                    var procurador_ = obj.Descendants("procurador").ToList()[0].Value;
                    Console.WriteLine("Procurador: " + procurador_);

                    processoExtrator1.NomeProcurador = procurador_;
                }
                if(obj.Descendants("apostila").ToList().Count > 0 )
                {
                    if (obj.Descendants("apostila").ToList()[0].Value != "")
                    {
                        var apostila_ = obj.Descendants("apostila").ToList()[0].Value;
                        Console.WriteLine("Apostila: " + apostila_);

                        processoExtrator1.Apostila = apostila_;
                    }
                    
                }

                if (obj.Descendants("situacao").ToList().Count > 0)
                {
                    if (obj.Descendants("situacao").ToList()[0].Value != "")
                    {
                        var situacao_ = obj.Descendants("situacao").ToList()[0].Value;
                        Console.WriteLine("Situação: " + situacao_);

                        processoExtrator1.Situacao = situacao_;
                    }
                }

                if (obj.Descendants("marca").ToList().Count > 0)
                {
                    var marca_ = obj.Descendants("marca").ToList()[0];
                    string apresentacao_ = marca_.Attribute("apresentacao").Value;
                    string natureza_ = marca_.Attribute("natureza").Value;
                    Console.WriteLine("Apresentação: " + apresentacao_);
                    Console.WriteLine("Natureza: " + natureza_);

                    processoExtrator1.ApresentacaoMarca = apresentacao_;
                    processoExtrator1.NaturezaMarca = natureza_;
                }
                if (obj.Descendants("marca").Descendants("nome").ToList().Count > 0)
                {
                    var nome_ = obj.Descendants("marca").Descendants("nome").ToList()[0].Value;
                    Console.WriteLine("Nome: " + nome_);

                    processoExtrator1.NomeMarca = nome_;
                }
                

                if (obj.Descendants("marca").Descendants("traducao").ToList().Count > 0)
                {

                    if (obj.Descendants("marca").Descendants("traducao").ToList()[0].Value != "")
                    {
                        var traducao_ = obj.Descendants("marca").Descendants("traducao").ToList()[0].Value;
                        Console.WriteLine("Tradução: " + traducao_);

                        processoExtrator1.TraducaoMarca = traducao_;
                    }
                }

                var titulares_ = obj.Descendants("titulares").Descendants("titular").ToList();
                foreach (var tit in titulares_)
                {
                    Titulares titulares = new Titulares();

                    string titular = tit.Attribute("nome-razao-social").Value;
                    Console.WriteLine("Nome - Razão social: " + titular);

                    titulares.NomeRazaoSocialTitular = titular;
                    if (tit.Attribute("pais").Value != "")
                    {
                        string titPais = tit.Attribute("pais").Value;
                        Console.WriteLine("País: " + titPais);

                        titulares.PaisTitular = titPais;
                    }
                    if (tit.Attribute("uf").Value != "")
                    {
                        string titUf = tit.Attribute("uf").Value;
                        Console.WriteLine("UF: " + titUf);

                        titulares.UfTitular = titUf;
                    }

                    processoExtrator1.Titulares.Add(titulares);
                }

                var dadosDeMadri_ = obj.Descendants("dados-de-madri").ToList(); //essa node não aparece nos dois processos, preciso arrumar uma maneira de tratar isso
                if (dadosDeMadri_.Count > 0) //so buscará se existir esse node
                {
                    var ddM = obj.Descendants("dados-de-madri").ToList()[0];
                    if (ddM.Attribute("inscricao_internacional") != null)
                    {
                        var inscInter_ = ddM.Attribute("inscricao_internacional").Value;
                        Console.WriteLine("Inscrição Internacional: " + inscInter_);

                        dadosMadri.InscricaoInternacional = inscInter_;
                    }
                    if (ddM.Attribute("data-recebimento-inpi") != null)
                    {
                        var dataRecebInpi_ = ddM.Attribute("data-recebimento-inpi").Value;
                        Console.WriteLine("Data de recebimento do Inpi: " + dataRecebInpi_);

                        DateTime dataRecebimentoDateTime = Convert.ToDateTime(dataRecebInpi_);
                        dadosMadri.DataRecebimentoInpi = dataRecebimentoDateTime;
                    }
                    

                }

                if (obj.Descendants("prioridade-unionista").Descendants("prioridade").ToList().Count > 0)
                {

                    var prioriUni_ = obj.Descendants("prioridade-unionista").Descendants("prioridade").ToList()[0];

                    var numeroPriori = prioriUni_.Attribute("numero").Value;
                    var dataPriori = prioriUni_.Attribute("data").Value;
                    var paisPriori = prioriUni_.Attribute("pais").Value;

                    Console.WriteLine("Número prioridade: " + numeroPriori);
                    Console.WriteLine("País: " + dataPriori);
                    Console.WriteLine("Data: " + paisPriori);
                }
                var classesViena = obj.Descendants("classes-vienna").Descendants("classe-vienna").ToList();
                foreach (var vie in classesViena)
                {
                    string edic = vie.Attribute("edicao").Value;
                    string cod = vie.Attribute("codigo").Value;
                    Console.WriteLine("Classe vienna");
                    Console.WriteLine("Edição: " + edic);
                    Console.WriteLine("Código: " + cod);
                }

                var classeNacional = obj.Descendants("classe-nacional").ToList();
                if (classeNacional.Count > 0)
                {
                    ClasseNacional classeNacional1 = new ClasseNacional();

                    var classeNacio = obj.Descendants("classe-nacional").ToList()[0];
                    string codNaci = classeNacio.Attribute("codigo").Value;
                    Console.WriteLine("Código: " + codNaci);

                    classeNacional1.CodigoClasseNacional = codNaci;

                    var subNacional = classeNacional.Descendants("sub-classes-nacional").ToList();
                    if (subNacional.Count > 0)
                    {
                        var subClasses = subNacional.Descendants("sub-classe-nacional").ToList();
                        foreach (var sbC in subClasses)
                        {
                            string subCl = sbC.Attribute("codigo").Value;
                            string subN = sbC.Value;

                            Console.Write("Código " + subCl + ": ");
                            Console.WriteLine(subN);

                            classeNacional1.CodigoSubClasseNacional = subCl;
                            classeNacional1.Especificacao = subN;
                        }
                    }
                    processoExtrator1.ClassesNacionais.Add(classeNacional1);
                }

                var listaNice = obj.Descendants("lista-classe-nice").Descendants("classe-nice").ToList();
                if (listaNice.Count > 0)
                {
                    foreach (var nic in listaNice)
                    {
                        string cod = nic.Attribute("codigo").Value;
                        Console.WriteLine("Lista de classe nice, código: " + cod);

                        if (listaNice.Descendants("edicao").ToList().Count > 0)
                        {
                            var edicao = listaNice.Descendants("edicao").ToList()[0].Value;
                            Console.WriteLine("Edição: " + edicao);
                        }
                        
                        var especi = listaNice.Descendants("especificacao").ToList()[0].Value;
                        Console.WriteLine("Especificação: " + especi);

                        if (obj.Descendants("lista-classe-nice").Descendants("classe-nice").Descendants("traducao-especificacao").ToList().Count > 0)
                        {
                            var especiTraducao = obj.Descendants("lista-classe-nice").Descendants("classe-nice").Descendants("traducao-especificacao").ToList()[0].Value;
                            Console.WriteLine("Tradução da Especificação: " + especiTraducao);
                        }
                        
                    }
                }

                var prorro = obj.Descendants("prorrogacao").ToList();
                if (prorro.Count > 0)
                {
                    var prorr = obj.Descendants("prorrogacao").ToList()[0];
                    var prazInicio = prorr.Attribute("prazo-ordinario-inicio").Value;
                    var prazFim = prorr.Attribute("prazo-ordinario-fim").Value;
                    var proPr = prorr.Descendants("prorrogacao").ToList()[0];
                    var dtIni = proPr.Attribute("prazo-extraordinario-inicio").Value;
                    var dtFim = proPr.Attribute("prazo-extraordinario-fim").Value;

                    Console.WriteLine("Prazo ordinário de início: " + prazInicio);
                    Console.WriteLine("Prazo ordinário de fim: " + prazFim);
                    Console.WriteLine("Prazo extraordinário de início: " + dtIni);
                    Console.WriteLine("Prazo extraordinário de fim: " + dtFim);

                }

                var peticao = obj.Descendants("peticoes").Descendants("peticao").ToList();
                if (peticao != null && peticao.Count > 0)
                {
                    foreach (var pet in peticao)
                    {
                        Peticoes peticoes = new Peticoes();

                        string numPet = pet.Attribute("numero").Value;
                        string dtPet = pet.Attribute("data").Value;
                        string codSerPet = pet.Attribute("codigoServico").Value;
                        var rq = pet.Descendants("requerente").ToList()[0];
                        string rqt = rq.Attribute("nome-razao-social").Value;

                        Console.WriteLine("Número da petição: " + numPet);
                        Console.WriteLine("Data da petição: " + dtPet);
                        Console.WriteLine("Código de serviço: " + codSerPet);
                        Console.WriteLine("Nome - Razão social do requerente: " + rqt);


                        peticoes.PeticaoProtocolo = numPet;

                    }
                }

                

                var despacho = obj.Descendants("despachos").Descendants("despacho").ToList();
                foreach(var desp in despacho)
                {
                    if(desp.Attribute("rpi") != null)
                    {

                        string rpi = desp.Attribute("rpi").Value;

                        Console.WriteLine("Rpi: " + rpi);
                    }
                    if (desp.Attribute("data-rpi") != null)
                    {

                        string dtRpi = desp.Attribute("data-rpi").Value;
                        Console.WriteLine("Data rpi: " + dtRpi);
                    }
                    if (desp.Attribute("despacho-rpi") != null)
                    {
                        string despRpi = desp.Attribute("despacho-rpi").Value;
                        Console.WriteLine("Despacho Rpi: " + despRpi);
                    }
                    if (despacho.Descendants("texto-complementar").ToList().Count > 0)
                    {
                        if (despacho.Descendants("texto-complementar").ToList()[0].Value != "")
                        {
                            var textComp = despacho.Descendants("texto-complementar").ToList()[0].Value;
                            Console.WriteLine("Texto complementar: " + textComp);
                        }
                        
                    }

                    // ------------- PEGAR O NÓ cessionario (<cessionario nome-razão-social="CREDBENS EMPRESA SIMPLES DE CRÉDITO LTDA"/>

                    var prot = despacho.Descendants("protocolo").ToList();
                    if (prot.Count > 0)
                    {
                        var proto = despacho.Descendants("protocolo").ToList()[0];
                        string numProto = proto.Attribute("numero").Value;
                        string dtProto = proto.Attribute("data").Value;
                        string codServico = proto.Attribute("codigoServico").Value;
                        Console.WriteLine("Número: " + numProto);
                        Console.WriteLine("Data: " + dtProto);
                        Console.WriteLine("Código do serviço: " + codServico);
                        if (prot.Descendants("requerente").Descendants("procurador").ToList().Count > 0)
                        {
                            if (prot.Descendants("requerente").Descendants("procurador").ToList()[0].Value != "")
                            {
                                var procProt = prot.Descendants("requerente").Descendants("procurador").ToList()[0].Value;
                                Console.WriteLine("Procurador: " + procProt);
                            }
                        }
                    }
                }
            }
        }   
    }
}
