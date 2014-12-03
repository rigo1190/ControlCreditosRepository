﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UnidadPresupuestal:Generica
    {

        public UnidadPresupuestal()
        {
            this.detalleDepartamentos = new HashSet<Departamentos>();
            this.detalleAmortizaciones = new HashSet<Amortizaciones>();
        }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }

        [StringLength(255, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Nombre { get; set; }


        public virtual ICollection<Departamentos> detalleDepartamentos { get; set; }

        public virtual ICollection<Amortizaciones> detalleAmortizaciones { get; set; }

    }
}
