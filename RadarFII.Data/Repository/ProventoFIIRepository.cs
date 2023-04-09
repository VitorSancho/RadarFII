

using RadarFII.Data.Interfaces;
using RadarFII.Data.Models;

namespace RadarFII.Data.Repository
{
    public class ProventoFIIRepository : IProventoFIIRepository
    {
        private readonly IDBRepository _dBRepository;
        public ProventoFIIRepository(IDBRepository _dBRepository)
        {
            _dBRepository = dBRepository;

        }

        public async Task SalvaListaDeProventosFII(IEnumerable<ProventoFII> listaDeProventos)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProventoFII>> SelectProventosAnunciadosEm(DateOnly dataBusca)
        {
            _dBRepository = "";

            await null;
        }
    }
}