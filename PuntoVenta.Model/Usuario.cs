using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta.Model
{
    public class Usuario
    {
        
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
        public IList<Puestos> Puesto { get; set; }
        public IList<GetRoles> Roles { get; set; }
        public InformacionPersonal InformacionPersonal { get; set; }

    }
}
