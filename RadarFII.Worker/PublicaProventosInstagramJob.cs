using Coravel.Invocable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFIIWorker
{
    internal class PublicaProventosInstagramJob : IInvocable
    {
        public Task Invoke()
        {
            Console.WriteLine("This is my first invocable!");
            return Task.CompletedTask;
        }
    }
}
