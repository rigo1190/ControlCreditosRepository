using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class FuentesDeFinanciamientos:Generica
    {

        public FuentesDeFinanciamientos()
        {
            this.detalleCreditos = new HashSet<Creditos>();
        }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }

        [StringLength(255, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Nombre { get; set; }

        public int Status { get; set; }

        public virtual ICollection<Creditos> detalleCreditos { get; set; }

    }
}
