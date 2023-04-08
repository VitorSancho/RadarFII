using RadarFII.Business.Intefaces;
using RadarFII.Business.Models;

namespace RadarFII.Business.Services
{
    public class ColetaProventosFIIBusiness :
         IColetaProventosFIIBusiness
    {
        public Task Adicionar(ProventoFII entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<ProventoFII> ObterPorId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProventoFII>> ObterRadarFIIs()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}