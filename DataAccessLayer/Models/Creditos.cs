using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Creditos:Generica
    {

        public int MunicipioId { get; set; }

        public int? FinancieraId { get; set; }

        public DateTime FechaDelContrato { get; set; }
        public decimal ImporteContratado { get; set; }

        public double TasaDeInteres { get; set; }
        public double TasaDeInteresMoratorio { get; set; }

        public int PeriodoDeAmortizacionId { get; set; }
        public int NPeriodos { get; set; }

        public string DestinoDelCredito { get; set; }

        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }


        public virtual Municipio Municipio { get; set; }

        public virtual Financieras Financiera { get; set; }

        public virtual PeriodosDeAmortizacion PeriodoDeAmortizacion { get; set; }



    }
}
