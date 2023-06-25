using Coravel.Invocable;
using RadarFII.Business.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFIIWorker
{
    public class ColetaProventosFIIJob : IInvocable
    {
        private IColetaProventosFIIBusiness _coletaProventosFIIBusiness;

        public ColetaProventosFIIJob(IColetaProventosFIIBusiness coletaProventosFIIBusiness)
        {
            _coletaProventosFIIBusiness = coletaProventosFIIBusiness;
        }

        public async Task Invoke()
        {
            await _coletaProventosFIIBusiness.Coleta();
        }
    }
}
