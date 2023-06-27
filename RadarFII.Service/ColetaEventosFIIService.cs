using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using RadarFII.Business.Interfaces.Service;
using RadarFII.Service.Models;
using RadarFII.Business.Models;

namespace RadarFII.Service
{
    public class ColetaEventosFIIService : IColetaEventosFIIService
    {
        private readonly RestClient restClient;
        private readonly string UrlListaAnuncios;
        private readonly string UrlAnuncio;

        public ColetaEventosFIIService(IConfiguration _configuration)
        {
            UrlListaAnuncios = _configuration["BmfFundosNet:UrlListaAnuncios"];

            UrlAnuncio = _configuration["BmfFundosNet:UrlAnuncio"];

            restClient = new RestClient();
            
        }

        public async Task<IEnumerable<EventoFII>> BuscarEventosFIIAnunciadosEm(DateOnly dataBusca)
        {
            Console.WriteLine("Buscando anuncios...");
            var ultimos50AnunciosPublicados = await BuscarUltimos50Anuncios();
            return (IEnumerable<EventoFII>)ultimos50AnunciosPublicados
                            .Where(anuncio => anuncio.dataEntrega.Substring(0, 10) == dataBusca.ToString("dd/MM/yyyy")).ToList();
        }



        public async Task<IEnumerable<EventoFII>> BuscarUltimos50Anuncios()
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

        public async Task<ProventoFII> RequisitarEExtrairProvento(EventoFII eventoFII)
        {
            var htmlAnuncio = await RequisitarAnuncio(eventoFII.id);

            return await ExtrairProvento(htmlAnuncio);
        }

        public async Task<string> RequisitarAnuncio(string idAnuncio)
        {
            var request = new RestRequest(UrlAnuncio.Replace("@idevento", idAnuncio));
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");

            var response = restClient.Get(request).Content;
            return response;
        }

        public async Task<ProventoFII> ExtrairProvento(string htmlAnuncio)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlAnuncio);

            var dadosSobreProventos = ExtraiDadosProventos(doc);
            var dadosSobreFundo = ExtraiDadosFundos(doc);

            var objProvento = ParserParaProventos(dadosSobreFundo, dadosSobreProventos);

            return objProvento;
        }

        private List<HtmlNode> ExtraiDadosFundos(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes("//span[@class='dado-cabecalho']")//this xpath selects all span tag having its class as hidden first                              
                              .ToList();
        }

        private List<HtmlNode> ExtraiDadosProventos(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes("//span[@class='dado-valores']")//this xpath selects all span tag having its class as hidden first                              
                              .ToList();
        }

        private ProventoFII ParserParaProventos(List<HtmlNode> listaDeDados, List<HtmlNode> dadosSobreFundo)
        {
            var provento = new ProventoFII();
            provento.DataAnuncio = dadosSobreFundo[3].InnerText;
            provento.DataPagamento = dadosSobreFundo[5].InnerText;
            provento.Valor = dadosSobreFundo[4].InnerText;
            provento.TickerFundo = listaDeDados[8].InnerText;
            provento.NomeFundo = listaDeDados[0].InnerText;
            provento.CNPJFundo = listaDeDados[1].InnerText;
            provento.NomeAdm = listaDeDados[2].InnerText;
            provento.CNPJAdm = listaDeDados[3].InnerText;

            return provento;
        }

        public Task EnviarParaFila(IEnumerable<ProventoFII> listaDeProventosFII)
        {
            foreach(var provento in listaDeProventosFII)
            {
                Console.WriteLine(provento.ToString());
            }
            return null;
        }
    }
}