using Microsoft.Extensions.Configuration;
using Moq;
using RadarFII.Service;

namespace RadarFII.tests
{
    public class ServiceTests
    {

        //BuscarUltimos50Anuncios -> contar se voltaram 50 anuncios

        //RequisitarAnuncio
        //...

        //ExtrairProvento

        //RequisitarEExtrairProvento

        private readonly ColetaEventosFIIService _coletaEventosFIIService;
        private readonly Mock<IConfiguration> _configurations;

        public ServiceTests()
        {
            _configurations = new Mock<IConfiguration>();
            _coletaEventosFIIService = new ColetaEventosFIIService(_configurations.Object);
        }

        [Fact]
        public void Requisicao_para_bmf_esta_funcionando()
        {
            _coletaEventosFIIService.RequisitarEExtrairProvento("473286");


        }
    }
}