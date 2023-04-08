using RadarFII.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFII.Business.Intefaces
{
    public interface IColetaProventosFIIBusiness
    {
        Task<IEnumerable<ProventoFII>> BuscaProventosFIIService();

        Task SalvaProventosFIIService(IEnumerable<ProventoFII> listaProventosFII);
    }
}
