using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Permiso:Generica
    {
        public int RolId { get; set; }
        public int OpcionSistemaId { get; set; }
        public enumOperaciones Operaciones { get; set; }       
        public virtual Rol Rol { get; set; }
        public virtual OpcionSistema OpcionSistema { get; set; }
    }

    [Flags]
    public enum enumOperaciones : short
    {
        Agregar = 0x1,
        Editar = 0x2,
        Borrar = 0x4,
        Detalle = 0x8
    }

}
