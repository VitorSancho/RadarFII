using System.Data.SqlTypes;

namespace RadarFII.Business.Models
{
    public class ProventoFII
    {
        public string TickerFundo { get; set; }
        public string Valor { get; set; }
        public string DataPagamento { get; set; }
        public string DataAnuncio { get; set; }
        public string NomeFundo { get; set; }
        public string CNPJFundo { get; set; }
        public string NomeAdm { get; set; }
        public string CNPJAdm { get; set; }
    }
}
