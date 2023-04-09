

using RadarFII.Data.Interfaces;
using RadarFII.Data.Models;

namespace RadarFII.Data.Repository
{
    public class ProventoFIIRepository : Interfaces.IProventoFIIRepository
    {
        public async Task SalvaListaDeProventosFII(IEnumerable<ProventoFII> listaDeProventos)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProventoFII>> SelectProventosAnunciadosEm(DateOnly dataBusca)
        {
            await null;
        }
    }
}