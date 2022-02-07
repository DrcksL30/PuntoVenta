using PuntoVenta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
        Task<Usuario> ObtenerUsuario(int id);

        Task<Login> ValidarUsuario(Login usuario);





        Task<ObtenerUsuario> VerUsuario(int id);
        Task<Roles> VerRolesUsuario(int id);

        Task<GetRoles> VerRoles(int id);

        Task<GetPuestos> VerPuestos(int id);

        Task<Puestos> VerPuestosUsuario(int id);
        Task<InformacionPersonal> VerInformacionPersonalUsuario(int id);









        Task<int> InsertUsuario(CrearUsuario usuario);

        Task<int> InsertRol(Roles usuario);

        Task<int> InsertPuesto(Puestos usuario);

        Task<int> InsertInformacionPersonal(InformacionPersonal usuario);

    }
}
