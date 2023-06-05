using System.Data.SqlTypes;

namespace RadarFII.Data.Models
{
    public class ProventoFII //: IEquatable<ProventoFII>
    {
        public int FundoId { get; set; }
        public string TicketFundo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataAnuncio { get; set; }
        public decimal DividenYieldNoMomentoDaColeta { get; set; }

        //public bool Equals(ProventoFII ProventoFII_2)
        //{
        //    return ((Valor == ProventoFII_2.Valor) &&
        //        (NomeFundo == ProventoFII_2.NomeFundo) &&
        //        (DataPagamento == ProventoFII_2.DataPagamento) &&
        //        (DividenYieldNoMomentoDaColeta == ProventoFII_2.DividenYieldNoMomentoDaColeta));
        //}
    }
}
