using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta.Model
{
    public class ObtenerUsuario
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }

        public IList<GetRoles> Roles { get; set; }
        public IList<GetPuestos> Puesto { get; set; }
        
        public InformacionPersonal InformacionPersonal { get; set; }
    }
}
