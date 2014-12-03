using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class OpcionSistema:Generica
    {
        public OpcionSistema()
        {
            this.SubOpciones = new HashSet<OpcionSistema>();
        }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        [Index(IsUnique = true)]
        public string Clave { get; set; }
        public string Descripcion { get; set; }

        [Index("IX_Orden_ParentId", 1, IsUnique = true)]
        public int Orden { get; set; }
        public bool Activo { get; set; }

        [Index("IX_Orden_ParentId", 2)]
        public int? ParentId { get; set; }
        public virtual OpcionSistema Parent { get; set; }
        public virtual ICollection<OpcionSistema> SubOpciones { get; set; }
    }
}
