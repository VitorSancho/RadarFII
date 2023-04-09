using RadarFII.Business.Intefaces;
using RadarFII.Data.Interfaces;
using RadarFII.Data.Models;
using RadarFII.Service;
using System.Runtime.CompilerServices;

namespace RadarFII.Business
{
    public class ColetaProventosFIIBusiness : IColetaProventosFIIBusiness
    {
        private readonly IColetaProventosFIIService _coletaProventosFIIService;
        private readonly IProventoFIIRepository _proventoFIIRepository;
        private DateOnly dataHoje;

        public ColetaProventosFIIBusiness(IColetaProventosFIIService coletaProventosFIIService,
                                            IProventoFIIRepository proventoFIIRepository)
        {
            _coletaProventosFIIService = coletaProventosFIIService;
            _proventoFIIRepository = proventoFIIRepository;
        }

        public async Task Coleta()
        {
            dataHoje = DateOnly.FromDateTime(DateTime.Now);
            var ProventosAnunciadosHoje = await BuscarProventosAnunciadosHojeNaoColetados();

            await SalvarNoBancoDeDados(ProventosAnunciadosHoje);
        }

        private async Task<IEnumerable<ProventoFII>> BuscarProventosAnunciadosHojeNaoColetados()
        {
            var proventosAnunciadosHoje =  await _coletaProventosFIIService.BuscarProventosAnunciadosEm(dataHoje);

            var anunciosNaoColetados = removeAnunciosProventosJaColetados(proventosAnunciadosHoje);

            return anunciosNaoColetados;
        }

        private async Task SalvarNoBancoDeDados(IEnumerable<ProventoFII> listaDeProventosDeFII)
        {
            await _proventoFIIRepository.SalvaListaDeProventosFII(listaDeProventosDeFII);
        }

        private IEnumerable<ProventoFII> removeAnunciosProventosJaColetados(IEnumerable<ProventoFII> listaDeProventosDeFIIcoletadosAgora)
        {
            //busca fundos já coletados hoje
            var proventosJaColetadosHoje = await _proventoFIIRepository.SelectProventosAnunciadosEm(dataHoje);

            if (JaHouveColetaHoje(proventosJaColetadosHoje))
            {            
                var novosLancamentosDeProventos = await removeProventosJaColetados(proventosJaColetadosHoje, listaDeProventosDeFIIcoletadosAgora);
                return novosLancamentosDeProventos;
            }
            else return null;

        }

        private async Task<IEnumerable<ProventoFII>> removeProventosJaColetados(IEnumerable<ProventoFII> proventosJaColetadosEmExecucaoAnterior,
                                                                                IEnumerable<ProventoFII> proventosColetadosNaAtualExecucao)
        {
            return null;
        }

        private bool JaHouveColetaHoje(IEnumerable<ProventoFII> proventosJaColetadosHoje)
        {
            return proventosJaColetadosHoje.Count() > 0;
        }


    }
}