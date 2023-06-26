using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFII.tests
{
    static class TestRepository
    {
        public static string HtmlAnuncioProventos()
        {
            var html = File.ReadAllText(".\\files\\ProventoHTML.txt");
            return html;
        }
    }
}
