using Coravel.Invocable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFIIWorker
{
    public class ColetaProventosFIIJob : IInvocable
    {
        public Task Invoke()
        {
            Console.WriteLine("This is my first invocable!");
            return Task.CompletedTask;
        }
    }
}
