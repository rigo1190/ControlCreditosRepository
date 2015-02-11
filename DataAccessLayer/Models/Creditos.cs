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
        public int FideicomisoId { get; set; }

        public int? MunicipioId { get; set; }

        

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NumeroDeContrato { get; set; }
        public DateTime FechaDelContrato { get; set; }
        public decimal ImporteContratado { get; set; }

        public double TasaNormal { get; set; }
        public double TasaMoratoria { get; set; }

        public int PeriodoDeAmortizacionId { get; set; }
        public int NPeriodos { get; set; }

        public decimal ImporteAmortizacion { get; set; }
        public string DestinoDelCredito { get; set; }



        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }


        public string PlazoDeInversion { get; set; }
        public string PeriodoDeGracia { get; set; }


        public string Observaciones { get; set; }

        public virtual Fideicomisos Fideicomiso { get;set;}

        public virtual Municipio Municipio { get; set; }

        

        public virtual PeriodosDeAmortizacion PeriodoDeAmortizacion { get; set; }

        

    }
}
