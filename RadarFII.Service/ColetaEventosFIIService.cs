using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RadarFII.Data.Interfaces;
using RadarFII.Data.Models;
using RestSharp;
using System.Net;
using System.Xml;

namespace RadarFII.Service
{
    public class ColetaEventosFIIService : IColetaEventosFIIService
    {
        private readonly IConfiguration configuration;
        private readonly RestClient restClient;
        private readonly string UrlListaAnuncios;
        private readonly string UrlAnuncio;

        public ColetaEventosFIIService(IConfiguration _configuration)
        {
            configuration = _configuration;
            UrlListaAnuncios = _configuration["BmfFundosNet:UrlListaAnuncios"];

            UrlAnuncio = _configuration["BmfFundosNet:UrlAnuncio"];

            restClient = new RestClient();
        }

        public async Task<IEnumerable<AnuncioFII>> BuscarEventosFIIAnunciadosEm(DateOnly dataBusca)
        {
            Console.WriteLine("Buscando anuncios...");
            var ultimos50AnunciosPublicados = BuscarUltimos50Anuncios().Result;
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
            WebClient client = new WebClient();
            var request = client.DownloadString(UrlAnuncio.Replace("@idevento", idAnuncio)).ToString();
            return request;
        }

        private async Task<ProventoFII> ExtrairProvento(string htmlAnuncio, string idAnuncio)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlAnuncio);

            var dadosSobreProventos = doc.DocumentNode.SelectNodes("//span[@class='dado-valores']")//this xpath selects all span tag having its class as hidden first                              
                              .ToList();

            var objProvento = ParserParaProventos(dadosSobreProventos);

            return null;
        }

        private ProventoFII ParserParaProventos(List<HtmlNode> listaDeDados)
        {
            var provento = new ProventoFII();
            provento.DataAnuncio = new DateTime(2010, 10, 10);

            return provento;
        }
    }
}