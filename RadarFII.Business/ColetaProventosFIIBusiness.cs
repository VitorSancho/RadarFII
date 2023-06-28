using RadarFII.Business.Interfaces.Business;
using RadarFII.Business.Interfaces.Data;
using RadarFII.Business.Interfaces.Service;
using RadarFII.Business.Models;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RadarFII.Business
{
    public class ColetaProventosFIIBusiness : IColetaProventosFIIBusiness
    {
        private readonly IColetaEventosFIIService _coletaProventosFIIService;
        private readonly IProventoFIIRepository _proventoFIIRepository;
        private DateOnly dataHoje;

        public ColetaProventosFIIBusiness(IColetaEventosFIIService coletaProventosFIIService,
                                            IProventoFIIRepository proventoFIIRepository)
        {
            _coletaProventosFIIService = coletaProventosFIIService;
            _proventoFIIRepository = proventoFIIRepository;
        }

        public async Task Coleta()
        {
            //dataHoje = DateOnly.FromDateTime(DateTime.Now);
            dataHoje = new DateOnly(2023, 06, 23);
            var AnunciosRealizadosHoje = await BuscarProventosAnunciadosHojeNaoColetados();

            await _coletaProventosFIIService.EnviarParaFila(AnunciosRealizadosHoje);
            //await SalvarNoBancoDeDados(AnunciosRealizadosHoje);
        }

        private async Task<IEnumerable<ProventoFII>> BuscarProventosAnunciadosHojeNaoColetados()
        {
            Console.WriteLine("Buscando...");
            var proventosAnunciadosHoje = await _coletaProventosFIIService.BuscarEventosFIIAnunciadosEm(dataHoje);

            var anunciosNaoColetados = await removeAnunciosProventosJaColetados(proventosAnunciadosHoje);

            var listaDeProventosAnunciadosHoje = await ExtraiProventosDeListaDeAnuncios(anunciosNaoColetados);

            return listaDeProventosAnunciadosHoje;
        }

        public async Task<IEnumerable<EventoFII>> removeAnunciosProventosJaColetados(IEnumerable<EventoFII> listaDeProventosDeFIIcoletadosAgora)
        {
            //busca fundos já coletados hoje
            var IdEventosJaColetadosHoje = await _proventoFIIRepository.SelectIdAnunciosDivulgadosEm(dataHoje);

            if (JaHouveColetaHoje(IdEventosJaColetadosHoje))
            {
                var novosLancamentosDeProventos = await removeProventosJaColetados(IdEventosJaColetadosHoje, listaDeProventosDeFIIcoletadosAgora);
                return novosLancamentosDeProventos;
            }
            return listaDeProventosDeFIIcoletadosAgora;

        }

        private bool JaHouveColetaHoje(IEnumerable<string> proventosJaColetadosHoje)
        {
            return proventosJaColetadosHoje.Count() > 0;
        }

        private async Task<IEnumerable<EventoFII>> removeProventosJaColetados(IEnumerable<string> IdEventosJaColetadosEmExecucaoAnterior,
                                                                            IEnumerable<EventoFII> anunciosColetadosNaAtualExecucao)
        {
            return anunciosColetadosNaAtualExecucao.Where(anuncio => !IdEventosJaColetadosEmExecucaoAnterior.ToList().Contains(anuncio.id));
        }

        private async Task<IEnumerable<ProventoFII>> ExtraiProventosDeListaDeAnuncios(IEnumerable<EventoFII> listaDeAnuncios)
        {
            var listaProventos = new List<ProventoFII>();
            foreach (var anuncioFII in listaDeAnuncios)
            {
                var proventos = await _coletaProventosFIIService.RequisitarEExtrairProvento(anuncioFII);
                listaProventos.Add(proventos);
            }

            return listaProventos;
        }
    }
}