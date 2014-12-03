using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UsuarioRol:Generica
    {
        [Index("IX_UsuarioId_RolId", 1, IsUnique = true)]
        public int UsuarioId { get; set; }

        [Index("IX_UsuarioId_RolId", 2)]
        public int RolId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
