using Moq;
using OpenQA.Selenium;
using RadarFII.Business;
using RadarFII.Business.Interfaces.Data;
using RadarFII.Business.Interfaces.Service;
using RadarFII.Business.Models;
using RadarFII.Service;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace RadarFII.tests
{
    public class BusinessTests
    {
        private readonly ColetaProventosFIIBusiness _coletaEventosFIIBusiness;
        private readonly Mock<IColetaEventosFIIService> _coletaEventosFIIService;
        private readonly Mock<IProventoFIIRepository> _proventoFIIRepository;
        private readonly ITestOutputHelper _testeHelper;
        private IEnumerable<EventoFII> _eventosFii_coletados_Mock;
        private IEnumerable<string> _eventosFii_todos_ja_salvos_Mock;
        private IEnumerable<string> _eventosFii_nenhum_salvos_Mock;
        private IEnumerable<string> _eventosFii_metade_ja_salvos_Mock;

        public BusinessTests(ITestOutputHelper testeHelper)
        {
            _testeHelper = testeHelper;
            _proventoFIIRepository = new Mock<IProventoFIIRepository>();
            _coletaEventosFIIService = new Mock<IColetaEventosFIIService>();

            _coletaEventosFIIBusiness = new ColetaProventosFIIBusiness(_coletaEventosFIIService.Object, _proventoFIIRepository.Object);

            _eventosFii_coletados_Mock = new List<EventoFII>
            {new EventoFII(){id = "1"},
            new EventoFII(){id = "2"},
            new EventoFII(){id = "3"},
            new EventoFII(){id = "4"} };

            _eventosFii_todos_ja_salvos_Mock = new List<string>{"1","2","3","4"};
            _eventosFii_nenhum_salvos_Mock = new List<string> { "5","6","7","8" };
            _eventosFii_metade_ja_salvos_Mock = new List<string> { "1", "2" };

        }

        [Fact] 
        public void remove_todos_proventos_coletados()
        { 
            _proventoFIIRepository
                                .Setup(obj => obj.SelectIdAnunciosDivulgadosEm(It.IsAny<DateOnly>()))
                                .ReturnsAsync(_eventosFii_todos_ja_salvos_Mock);

            var proventos_que_devem_ser_salvos = _coletaEventosFIIBusiness
                                        .removeAnunciosProventosJaColetados(_eventosFii_coletados_Mock).Result;

            Assert.True(proventos_que_devem_ser_salvos.Count() == 0);
        }

        [Fact]
        public void remove_nenhum_provento_coletado()
        {
            _proventoFIIRepository
                                .Setup(obj => obj.SelectIdAnunciosDivulgadosEm(It.IsAny<DateOnly>()))
                                .ReturnsAsync(_eventosFii_nenhum_salvos_Mock);

            var proventos_que_devem_ser_salvos = _coletaEventosFIIBusiness
                                        .removeAnunciosProventosJaColetados(_eventosFii_coletados_Mock).Result;

            Assert.True(proventos_que_devem_ser_salvos.Count() == _eventosFii_coletados_Mock.Count());
        }

        [Fact]
        public void remove_metade_dos_proventos_coletados()
        {
            _proventoFIIRepository
                                .Setup(obj => obj.SelectIdAnunciosDivulgadosEm(It.IsAny<DateOnly>()))
                                .ReturnsAsync(_eventosFii_metade_ja_salvos_Mock);

            var proventos_que_devem_ser_salvos = _coletaEventosFIIBusiness
                                        .removeAnunciosProventosJaColetados(_eventosFii_coletados_Mock).Result;

            Assert.True(proventos_que_devem_ser_salvos.Count() == _eventosFii_coletados_Mock.Count()/2);
        }
    }
}