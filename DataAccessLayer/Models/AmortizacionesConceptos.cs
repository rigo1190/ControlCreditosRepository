using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AmortizacionesConceptos:Generica
    {

        public int AmortizacionId { get; set; }

        public int Partida { get; set; }//numero, consecutivo, numero de concepto, orden, #,etc
        public string Concepto { get; set; }
        public Decimal Importe { get; set; }


        public virtual Amortizaciones Amortizacion { get; set; }

    }
}
