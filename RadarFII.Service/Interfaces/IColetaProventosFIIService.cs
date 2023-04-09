

using RadarFII.Data.Models;

namespace RadarFII.Service
{
    public interface IColetaProventosFIIService
    {
        Task<IEnumerable<ProventoFII>> BuscarProventosAnunciadosEm(DateOnly dataBusca);
    }
}