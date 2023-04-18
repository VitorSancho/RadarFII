using RadarFII.Data.Models;

namespace RadarFII.Service
{
    public interface IColetaEventosFIIService
    {
        Task<IEnumerable<AnuncioFII>> BuscarEventosFIIAnunciadosEm(DateOnly dataBusca);

        Task<IEnumerable<ProventoFII>> ExtraiProventosDeListaDeAnuncios(IEnumerable<AnuncioFII> listaDeAnuncios);
    }
}
