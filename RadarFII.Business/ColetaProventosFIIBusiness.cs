using RadarFII.Business.Intefaces;
using RadarFII.Data.Interfaces;
using RadarFII.Data.Models;
using RadarFII.Service;
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
            Console.WriteLine("Something...");
            //dataHoje = DateOnly.FromDateTime(DateTime.Now);
            dataHoje = new DateOnly(2023, 06, 02);
            var AnunciosRealizadosHoje = await BuscarProventosAnunciadosHojeNaoColetados();

            await SalvarNoBancoDeDados(AnunciosRealizadosHoje);
        }

        private async Task<IEnumerable<ProventoFII>> BuscarProventosAnunciadosHojeNaoColetados()
        {
            Console.WriteLine("Buscando...");
            var proventosAnunciadosHoje = await _coletaProventosFIIService.BuscarEventosFIIAnunciadosEm(dataHoje);

            var anunciosNaoColetados = await removeAnunciosProventosJaColetados(proventosAnunciadosHoje);

            var listaDeProventosAnunciadosHoje = await _coletaProventosFIIService.ExtraiProventosDeListaDeAnuncios(anunciosNaoColetados);

            return listaDeProventosAnunciadosHoje;
        }

        private async Task SalvarNoBancoDeDados(IEnumerable<ProventoFII> listaDeProventosDeFII)
        {
            await _proventoFIIRepository.SalvaListaDeProventosFII(listaDeProventosDeFII);
        }

        private async Task<IEnumerable<AnuncioFII>> removeAnunciosProventosJaColetados(IEnumerable<AnuncioFII> listaDeProventosDeFIIcoletadosAgora)
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

        private async Task<IEnumerable<AnuncioFII>> removeProventosJaColetados(IEnumerable<string> IdEventosJaColetadosEmExecucaoAnterior,
                                                                            IEnumerable<AnuncioFII> anunciosColetadosNaAtualExecucao)
        {
            return anunciosColetadosNaAtualExecucao.Where(anuncio => !IdEventosJaColetadosEmExecucaoAnterior.ToList().Contains(anuncio.id));
        }
    }
}