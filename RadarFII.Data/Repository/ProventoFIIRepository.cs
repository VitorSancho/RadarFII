using RadarFII.Data.Interfaces;
using RadarFII.Data.Models;

namespace RadarFII.Data.Repository
{
    public class ProventoFIIRepository : IProventoFIIRepository
    {
        private readonly IDBRepository _dBRepository;
        public ProventoFIIRepository(IDBRepository dBRepository)
        {
            _dBRepository = dBRepository;
        }

        public async Task SalvaListaDeProventosFII(IEnumerable<ProventoFII> listaDeProventos)
        {
            foreach(var provento in listaDeProventos)
            {
                var expressarInsertProventos = $@"insert into anuncioProventos
                                (FundoId, DataAnuncio, DataPagamento, ValorProvento, Ticket)
                                values ({provento.FundoId},{provento.DataAnuncio},{provento.DataPagamento},
                                    {provento.Valor},{provento.TicketFundo})";
                _dBRepository.ExecutaInsert(expressarInsertProventos);
            }
        }

        public async Task<IEnumerable<string>> SelectIdAnunciosDivulgadosEm(DateOnly dataBusca)
        {
            var expressaoConsultaProventosAnunciadosHoje = $"select AnuncioId from AnuncioProventos where DataAnuncio='{dataBusca.ToString("yyyy-MM-dd")}'";
            return await _dBRepository.RealizaConsulta<string>(expressaoConsultaProventosAnunciadosHoje);
        }
    }
}