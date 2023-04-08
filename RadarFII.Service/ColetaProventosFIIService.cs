using RadarFII.Business.Intefaces;

namespace RadarFII.Service
{
    public class ColetaProventosFIIService
    {
        private readonly IColetaProventosFIIBusiness _coletaProventosFIBusiness;
        public ColetaProventosFIIService(IColetaProventosFIIBusiness coletaProventosFIIBusiness)
        {
            _coletaProventosFIBusiness = coletaProventosFIIBusiness;
        }
        public async Task ExecutaColetaProventosFII()
        {
            var listaProventos = _coletaProventosFIBusiness.BuscaProventosFIIService();

            _coletaProventosFIBusiness.SalvaProventosFIIService(listaProventos);
        }
    }
}