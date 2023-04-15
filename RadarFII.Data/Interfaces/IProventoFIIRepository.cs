using RadarFII.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFII.Data.Interfaces
{
    public interface IProventoFIIRepository
    {
        Task SalvaListaDeProventosFII(IEnumerable<ProventoFII> listaDeProventos);
        Task<IEnumerable<string>> SelectIdAnunciosDivulgadosEm(DateOnly dataBusca);
    }
}
