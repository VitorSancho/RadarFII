using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using RadarFII.Business.Models;
using RadarFII.Service;
using Xunit.Abstractions;

namespace RadarFII.tests
{
    public class ServiceTests
    {
        private readonly ColetaEventosFIIService _coletaEventosFIIService;
        private readonly IConfiguration _configurations;
        private readonly EventoFII _eventoFiiMock;
        private readonly ProventoFII _proventoFiiMock;
        private readonly ITestOutputHelper _testeHelper;

        public ServiceTests(ITestOutputHelper testeHelper)
        {
            _configurations = TestsConfiguration.GetIConfigurationRoot();
            _testeHelper = testeHelper;

            _coletaEventosFIIService = new ColetaEventosFIIService(_configurations);

            _eventoFiiMock = new EventoFII()
            {
                id = "473286"
            };

            _proventoFiiMock = new ProventoFII()
            {
                TickerFundo = "HGIC11",
                Valor = "0,91",
                DataPagamento = "13/06/2023",
                DataAnuncio = "02/06/2023",
                NomeFundo = "HGI CR&Eacute;DITOS IMOBILI&Aacute;RIOS FUNDO DE INVESTIMENTO IMOBILI&Aacute;RIO",
                CNPJFundo = "38.456.508/0001-71",
                NomeAdm = "BTG PACTUAL SERVI&Ccedil;OS FINANCEIROS S/A DTVM",
                CNPJAdm = "59.281.253/0001-23"
            };
        }

        [Fact]
        public void Requisicao_para_bmf_esta_funcionando()
        {
            var testResult = _coletaEventosFIIService.RequisitarEExtrairProvento(_eventoFiiMock).GetAwaiter().GetResult();
            
            Assert.Equal(_proventoFiiMock.TickerFundo, testResult.TickerFundo);
            _testeHelper.WriteLine("Ticket do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.Valor, testResult.Valor);
            _testeHelper.WriteLine("Valor do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.DataPagamento, testResult.DataPagamento);
            _testeHelper.WriteLine("DataPagamento do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.DataAnuncio, testResult.DataAnuncio);
            _testeHelper.WriteLine("DataAnuncio do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.NomeFundo, testResult.NomeFundo);
            _testeHelper.WriteLine("NomeFundo do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.CNPJFundo, testResult.CNPJFundo);
            _testeHelper.WriteLine("CNPJFundo do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.NomeAdm, testResult.NomeAdm);
            _testeHelper.WriteLine("NomeAdm do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.CNPJAdm, testResult.CNPJAdm);
            _testeHelper.WriteLine("CNPJAdm do fundo est� correto!");
        }

        [Fact]
        public void parseia_html_em_proventoFII()
        {
            var Proventohtml = TestRepository.HtmlAnuncioProventos();

            var proventoFIIobject = _coletaEventosFIIService.ExtrairProvento(Proventohtml).Result;

            Assert.Equal(_proventoFiiMock.TickerFundo, proventoFIIobject.TickerFundo);
            _testeHelper.WriteLine("Ticket do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.Valor, proventoFIIobject.Valor);
            _testeHelper.WriteLine("Valor do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.DataPagamento, proventoFIIobject.DataPagamento);
            _testeHelper.WriteLine("DataPagamento do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.DataAnuncio, proventoFIIobject.DataAnuncio);
            _testeHelper.WriteLine("DataAnuncio do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.NomeFundo, proventoFIIobject.NomeFundo);
            _testeHelper.WriteLine("NomeFundo do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.CNPJFundo, proventoFIIobject.CNPJFundo);
            _testeHelper.WriteLine("CNPJFundo do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.NomeAdm, proventoFIIobject.NomeAdm);
            _testeHelper.WriteLine("NomeAdm do fundo est� correto!");
            Assert.Equal(_proventoFiiMock.CNPJAdm, proventoFIIobject.CNPJAdm);
            _testeHelper.WriteLine("CNPJAdm do fundo est� correto!");
        }


        [Fact]
        public void requisita_anuncio_proventoFII()
        {
            var proventoFIIobject = _coletaEventosFIIService.RequisitarAnuncio(_eventoFiiMock.id).Result;

            Assert.Contains("Valor do provento", proventoFIIobject);
        }

        [Fact]
        public void busca_anuncios_site_bfm()
        {
            var proventoFIIobject = _coletaEventosFIIService.BuscarUltimos50Anuncios().Result;

            Assert.True(proventoFIIobject.Count() == 50);
        }
    }
}