using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta.Model
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Rol{ get; set; }

    }
}
