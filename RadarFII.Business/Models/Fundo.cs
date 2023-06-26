using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFII.Business.Models
{
    public class Fundo
    {
        public int FundoId { get; set; }
        public string NomeFundo { get; set; }
        public string Ticket { get; set;}
        public decimal LiquidezDiaria { get; set; }
    }
}
