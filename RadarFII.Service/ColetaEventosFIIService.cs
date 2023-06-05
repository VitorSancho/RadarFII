using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RadarFII.Data.Interfaces;
using RadarFII.Data.Models;
using RestSharp;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static System.Net.Mime.MediaTypeNames;
using System;
using ScrapySharp.Network;
using System.Globalization;

namespace RadarFII.Service
{
    public class ColetaEventosFIIService : IColetaEventosFIIService
    {
        private readonly RestClient restClient;
        private readonly string UrlListaAnuncios;
        private readonly string UrlAnuncio;
        private readonly IWebDriver driver;

        public ColetaEventosFIIService(IConfiguration _configuration)
        {
            UrlListaAnuncios = _configuration["BmfFundosNet:UrlListaAnuncios"];

            UrlAnuncio = _configuration["BmfFundosNet:UrlAnuncio"];

            restClient = new RestClient();
            
        }

        public async Task<IEnumerable<AnuncioFII>> BuscarEventosFIIAnunciadosEm(DateOnly dataBusca)
        {
            Console.WriteLine("Buscando anuncios...");
            var ultimos50AnunciosPublicados = await BuscarUltimos50Anuncios();
            return (IEnumerable<AnuncioFII>)ultimos50AnunciosPublicados
                            .Where(anuncio => anuncio.dataEntrega.Substring(0, 10) == dataBusca.ToString("dd/MM/yyyy")).ToList();
        }

        public async Task<IEnumerable<ProventoFII>> ExtraiProventosDeListaDeAnuncios(IEnumerable<AnuncioFII> listaDeAnuncios)
        {
            var listaProventos = new List<ProventoFII>();
            foreach (var anuncioFII in listaDeAnuncios)
            {
                var proventos = await RequisitarEExtrairProvento(anuncioFII);
                listaProventos.Add(proventos);
            }

            return listaProventos;

        }

        private async Task<IEnumerable<AnuncioFII>> BuscarUltimos50Anuncios()
        {
            var request = new RestRequest(UrlListaAnuncios);
            request.AddParameter("d", "50");
            request.AddParameter("s", "0");
            request.AddParameter("l", "50");
            request.AddParameter("o[0][dataEntrega]", "desc");
            request.AddParameter("idCategoriaDocumento", "14");
            request.AddParameter("idTipoDocumento", "41");
            request.AddParameter("idEspecieDocumento", "0");
            request.AddParameter("tipoFundo", "1");
            request.Timeout = 30000;
            var response = restClient.Get(request).Content;
            return JsonConvert.DeserializeObject<ResponseAnuncioFII>(response).data;
        }

        private async Task<ProventoFII> RequisitarEExtrairProvento(AnuncioFII anuncioFII)
        {
            var htmlAnuncio = await RequisitarAnuncio(anuncioFII.id);

            return await ExtrairProvento(htmlAnuncio,anuncioFII.id);
        }

        private async Task<string> RequisitarAnuncio(string idAnuncio)
        {
  //          WebClient client = new WebClient();
            
//            var request = client.DownloadString(UrlAnuncio.Replace("@idevento", idAnuncio)).ToString();

            var request = new RestRequest(UrlAnuncio.Replace("@idevento", idAnuncio));
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");

            //request.AddParameter("d", "50");
            //request.AddParameter("s", "0");
            //request.AddParameter("l", "50");
            //request.AddParameter("o[0][dataEntrega]", "desc");
            //request.AddParameter("idCategoriaDocumento", "14");
            //request.AddParameter("idTipoDocumento", "41");
            //request.AddParameter("idEspecieDocumento", "0");
            //request.AddParameter("tipoFundo", "1");
            //request.Timeout = 30000;
            var response = restClient.Get(request).Content;
            return response;
        }

        private async Task<ProventoFII> ExtrairProvento(string htmlAnuncio, string idAnuncio)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlAnuncio);

            var dadosSobreProventos = ExtraiDadosProventos(doc);
            var dadosSobreFundo = doc.DocumentNode.SelectNodes("//span[@class='dado-cabecalho']")//this xpath selects all span tag having its class as hidden first                              
                  .ToList();

            var objProvento = ParserParaProventos(dadosSobreProventos, dadosSobreFundo);

            return objProvento;
        }

        private List<HtmlNode> ExtraiDadosProventos(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes("//span[@class='dado-cabecalho']")//this xpath selects all span tag having its class as hidden first                              
                              .ToList();
        }

        private List<HtmlNode> ExtraiDadosFundos(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes("//span[@class='dado-valores']")//this xpath selects all span tag having its class as hidden first                              
                              .ToList();



        }

        private ProventoFII ParserParaProventos(List<HtmlNode> listaDeDados, List<HtmlNode> dadosSobreFundo)
        {
            var provento = new ProventoFII();
            provento.DataAnuncio = DateTime.Today;
            provento.DataPagamento = DateTime.ParseExact(listaDeDados[6].InnerText, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            provento.DividenYieldNoMomentoDaColeta = 0;
            provento.Valor = Decimal.Parse(listaDeDados[6].InnerText.Replace(',','.'));
            provento.TicketFundo = dadosSobreFundo[8].InnerText;


            return provento;
        }
    }
}