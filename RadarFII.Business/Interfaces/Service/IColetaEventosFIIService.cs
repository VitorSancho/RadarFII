using RadarFII.Business.Models;

namespace RadarFII.Business.Interfaces.Service
{
    public interface IColetaEventosFIIService
    {
        Task<IEnumerable<EventoFII>> BuscarEventosFIIAnunciadosEm(DateOnly dataBusca);

        Task<ProventoFII> RequisitarEExtrairProvento(EventoFII anuncioFII);

        Task EnviarParaFila(IEnumerable<ProventoFII> listaDeProventosFII);
    }
}
