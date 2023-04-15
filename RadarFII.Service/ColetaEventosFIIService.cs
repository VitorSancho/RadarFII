using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RadarFII.Data.Models;
using RestSharp;

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
            UrlListaAnuncios = _configuration["BmfFundos.Net:UrlListaAnuncios"];

            UrlAnuncio = _configuration["BmfFundos.Net:UrlAnuncio"];

            var restClient = new RestClient();
        }

        public async Task<IEnumerable<AnuncioFII>> BuscarEventosFIIAnunciadosEm(DateOnly dataBusca)
        {
            var ultimos50AnunciosPublicados = await BuscarUltimos50Anuncios();
            return ultimos50AnunciosPublicados;
        }

        public async Task<IEnumerable<ProventoFII>> ExtraiProventosDeListaDeAnuncios(IEnumerable<AnuncioFII> listaDeAnuncios)
        {
            var listaProventos = new List<ProventoFII>();
           foreach(var anuncioFII in listaDeAnuncios)
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
            request.AddParameter("l", "0");
            request.AddParameter("o[0][dataEntrega]", "desc");
            request.AddParameter("idCategoriaDocumento", "14");
            request.AddParameter("idTipoDocumento", "41");
            request.AddParameter("idEspecieDocumento", "0");
            request.AddParameter("tipoFundo", "1");
            var response = restClient.GetAsync(request).Result.Content;
            return JsonConvert.DeserializeObject<IEnumerable<AnuncioFII>>(response);
        }

        private async Task<ProventoFII> RequisitarEExtrairProvento(AnuncioFII anuncioFII)
        {
            var htmlAnuncio = await RequisitarAnuncio(anuncioFII.id);

            return await ExtrairProvento(htmlAnuncio);
        }

        private async Task<string> RequisitarAnuncio(string idAnuncio)
        {
            var request = new RestRequest(UrlAnuncio.Replace("@idevento", idAnuncio));
            return await restClient.GetAsync<string>(request);
        }

        private async Task<ProventoFII> ExtrairProvento(string htmlAnuncio)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(htmlAnuncio);

            var dadosSobreProventos = doc.DocumentNode.SelectNodes("//span[@class='dado-valores']")//this xpath selects all span tag having its class as hidden first                              
                              .ToList();

            var objProvento = ParserParaProventos(dadosSobreProventos);

            return null;
        }

        private ProventoFII ParserParaProventos(List<HtmlNode> listaDeDados)
        {
            var provento = new ProventoFII();
            provento.DataAnuncio = new DateTime(2010,10,10);

            return provento;
        }
    }
}