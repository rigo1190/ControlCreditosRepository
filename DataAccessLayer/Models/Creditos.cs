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

        public Creditos()
        {
            this.detalleAmortizaciones = new HashSet<Amortizaciones>();
            this.detalleCalendarioPagos = new HashSet<CalendarioPagos>();
        }
        public int FideicomisoId { get; set; }
        public int FuenteDeFinanciamientoId { get; set; }
        public int DestinoDeFinanciamientoId { get; set; }        

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NumeroDeContrato { get; set; }
        public DateTime FechaDelContrato { get; set; }   
        public int TipoDeMonedaId { get; set; }
        public decimal CantidadContratada { get; set; }
        public decimal ValorTipoMoneda { get; set; }
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
        public virtual FuentesDeFinanciamientos FuenteDeFinanciamiento { get; set; }
        public virtual DestinosDeFinanciamientos DestinoDeFinanciamiento { get; set; }
        public virtual TiposDeMonedas TipoDeMoneda { get; set; }
        public virtual PeriodosDeAmortizacion PeriodoDeAmortizacion { get; set; }
        public virtual ICollection<Amortizaciones> detalleAmortizaciones { get; set; }
        public virtual ICollection<CalendarioPagos> detalleCalendarioPagos { get; set; }

    }
}
