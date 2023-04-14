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

        public Task<IEnumerable<ProventoFII>> ExtraiProventosDeListaDeAnuncios(IEnumerable<AnuncioFII> listaDeAnuncios)
        {
            var listaProventos = new List<ProventoFII>();
           foreach(var anuncioFII in listaDeAnuncios)
            {
                var proventos =  ExtrairProvento(anuncioFII);
                listaProventos.Add(proventos);
            }

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

        private async Task<ProventoFII> ExtrairProvento(AnuncioFII anuncioFII)
        {
            var request = new RestRequest(UrlAnuncio.Replace("@idevento", anuncioFII.id));
            var response = await restClient.GetAsync(request);

            HtmlDocument doc = new HtmlDocument();
            doc.Load(yourStream);

            var itemList = doc.DocumentNode.SelectNodes("//span[@class='hidden first']")//this xpath selects all span tag having its class as hidden first
                              .Select(p => p.InnerText)
                              .ToList();
        }
    }
}