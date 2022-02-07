using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PuntoVenta.Model
{
    public class CrearUsuario
    {

        [Key]
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
        public IList<Roles> Roles { get; set; }
        public IList<Puestos> Puesto { get; set; }
        public InformacionPersonal InformacionPersonal { get; set; }




    }
}
