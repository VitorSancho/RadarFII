using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RadarFII.Data.Models;
using RestSharp;

namespace RadarFII.Service
{
    public class ColetaProventosFIIService : IColetaProventosFIIService
    {
        private readonly IConfiguration configuration;
        private readonly RestClient restClient;
        private readonly string UrlListaAnuncios;

        public ColetaProventosFIIService(IConfiguration _configuration)
        {
            configuration = _configuration;
            UrlListaAnuncios = _configuration["BmfFundos.Net:Url"];

            var restClient = new RestClient();
        }

        public async Task<IEnumerable<ProventoFII>> BuscarProventosAnunciadosEm(DateOnly dataBusca)
        {
            var ultimos50AnunciosPublicados = await BuscarUltimos50Anuncios();

        }

        private async Task<IEnumerable<ProventoFII>> BuscarUltimos50Anuncios()
        {
            var request = new RestRequest(UrlListaAnuncios);
            request.AddParameter("d", "0");
            request.AddParameter("s", "0");
            request.AddParameter("l", "30");
            request.AddParameter("o[0][dataEntrega]", "desc");
            request.AddParameter("idCategoriaDocumento", "14");
            request.AddParameter("idTipoDocumento", "41");
            request.AddParameter("idEspecieDocumento", "0");
            request.AddParameter("tipoFundo", "1");
            var response = await restClient.GetAsync(request).Result;
            return JsonConvert.SerializeObject<ProventoFII>(response);
        }
    }
}