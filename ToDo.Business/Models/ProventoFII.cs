using System.Data.SqlTypes;

namespace RadarFII.Business.Models
{
    public class ProventoFII : Entity
    {
        public string NomeFundo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataAnuncio { get; set; }
        public decimal DividenYieldNoMomentoDaColeta { get; set; }
    }
}