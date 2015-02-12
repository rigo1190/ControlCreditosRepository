using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class TablaAmortizaciones:Generica
    {
        [Index("IX_Numero_CreditoId", 1, IsUnique = true)]
        public int Numero { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal ImporteAmortizado { get; set; }
        public decimal SaldoInsoluto { get; set; }

        [Index("IX_Consecutivo_CreditoId", 2)]
        public int CreditoId { get; set; }
        public virtual Creditos Credito { get; set; }
    }
}
