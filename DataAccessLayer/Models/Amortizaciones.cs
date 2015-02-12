using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Models
{
    public class Amortizaciones:Generica
    {

        public Amortizaciones()
        {
            this.detalleConceptos = new HashSet<AmortizacionesConceptos>();
        }

        public int CreditoId { get; set; }
        public int NumeroDeAmortizacion { get; set; }

        public DateTime FechaDeElaboracion { get; set; }
        public DateTime? FechaDeProcesado { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NoTramite { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string noFolio { get; set; }


        public int UnidadPresupuestalId { get; set; }
        public int? DepartamentoId { get; set; }

        public string Instruccion { get; set; }

 
        public string DescripcionDelPago { get; set; }

        public decimal Cantidad { get; set; }
        public decimal Valor { get; set; }
        public decimal Importe { get; set; }



        public virtual Creditos Credito { get; set; }
        public virtual UnidadPresupuestal UnidadPresupuestal { get; set; }
        public virtual Departamentos Departamento { get; set; }

        public virtual ICollection<AmortizacionesConceptos> detalleConceptos { get; set; }



    }
}
