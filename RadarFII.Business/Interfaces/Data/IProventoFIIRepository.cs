using RadarFII.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFII.Business.Interfaces.Data
{
    public interface IProventoFIIRepository
    {
        //Task SalvaListaDeProventosFII(IEnumerable<ProventoFII> listaDeProventos);
        Task<IEnumerable<string>> SelectIdAnunciosDivulgadosEm(DateOnly dataBusca);
    }
}
