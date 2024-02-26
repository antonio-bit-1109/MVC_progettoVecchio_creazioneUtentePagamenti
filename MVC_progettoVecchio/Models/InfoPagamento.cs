using System;

namespace MVC_progettoVecchio.Models
{
    public class InfoPagamento
    {
        public int IdPagamento { get; set; }
        public DateTime PeriodoDelPagamento { get; set; } = DateTime.Now;
        public string TipoDiPagamento { get; set; }

        public decimal Importo { get; set; }

        public int idLavoratore { get; set; }

        public InfoPagamento()
        {
        }
        public InfoPagamento(int idPagamento, DateTime periodoDelPagamento, string tipoDiPagamento, decimal importo, int idLavoratore)
        {
            this.IdPagamento = idPagamento;
            this.PeriodoDelPagamento = periodoDelPagamento;
            this.TipoDiPagamento = tipoDiPagamento;
            this.Importo = importo;
            this.idLavoratore = idLavoratore;
        }
    }
}