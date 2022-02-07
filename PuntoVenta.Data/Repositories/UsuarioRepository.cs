using Dapper;
using Npgsql;
using PuntoVenta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private PostgresSQLConfiguration _connectionString;

        public UsuarioRepository(PostgresSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }





        public async Task<Usuario> ObtenerUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"

                        SELECT correo, clave
                        FROM public.usuarios
                        WHERE id = @Id
                        ";

            return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }




        public async Task<ObtenerUsuario> VerUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"

                        SELECT id, correo, clave, activo
                        FROM public.usuarios
                        WHERE id = @Id
                        ";

           var result = await db.QueryFirstOrDefaultAsync<ObtenerUsuario>(sql, new { Id = id });

            return result;



        }

        public async Task<Roles> VerRolesUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"

                        SELECT id, id_usuario, id_rol
                        FROM public.roles_usuario
                        WHERE id_usuario = @Id_Usuario
                        ";

            var result= await db.QueryFirstOrDefaultAsync<Roles>(sql, new { Id_Usuario = id });
            return result;
        }

        public async Task<Puestos> VerPuestosUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"

                        SELECT id_usuario, id_puesto
                        FROM public.puestos_usuario
                        WHERE id_usuario = @Id_Usuario
                        ";

            var result = await db.QueryFirstOrDefaultAsync<Puestos>(sql, new { Id_Usuario = id });
            return result;
        }

        public async Task<InformacionPersonal> VerInformacionPersonalUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"

                        SELECT *
                        FROM public.informacion_personal
                        WHERE id_usuario = @Id_Usuario
                        ";

            var result = await db.QueryFirstOrDefaultAsync<InformacionPersonal>(sql, new { Id_Usuario = id });
            return result;
        }










        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            var db = dbConnection();

            var sql = @"

                        SELECT id, correo, clave, activo
                        FROM public.usuarios

                        ";

            return await db.QueryAsync<Usuario>(sql, new { });
        }

        public async Task<Login> ValidarUsuario(Login usuario)
        {
            var db = dbConnection();

            var sql = @"

                        SELECT correo, clave
                        FROM public.usuarios
                        WHERE correo = @Correo
                        ";

            return await db.QueryFirstOrDefaultAsync<Login>(sql, new { Correo = usuario.Correo });
        }





        public async Task<int> InsertUsuario(CrearUsuario usuario)
        {
            var db = dbConnection();

            var sql = @"

                        INSERT INTO public.usuarios (correo, clave, activo)
                        VALUES(@Correo, @Clave, @Activo) RETURNING id;
                        ";

            var result = await db.QueryAsync<int>(sql, new { Correo = usuario.Correo, Clave = usuario.Clave, Activo = usuario.Activo });
            return result.Single();
        }

        public async Task<int> InsertRol(Roles roles)
        {
            var db = dbConnection();

            var sql = @"

                        INSERT INTO public.roles_usuario (id_usuario, id_rol)
                        VALUES(@Id_Usuario, @Id_rol) RETURNING id;
                        ";

            var result = await db.QueryAsync<int>(sql, new { Id_Usuario= roles.Id_Usuario, Id_Rol = roles.Id_Rol });

            return result.Single();
        }
        public async Task<int> InsertPuesto(Puestos puesto)
        {
            var db = dbConnection();

            var sql = @"

                        INSERT INTO public.puestos_usuario (id_usuario, id_puesto)
                        VALUES(@Id_Usuario, @Id_rol) RETURNING id;
                        ";

            var result = await db.QueryAsync<int>(sql, new { Id_Usuario = puesto.Id_Usuario, Id_Rol = puesto.Id_Puesto });

            return result.Single();
        }

        public async Task<int> InsertInformacionPersonal(InformacionPersonal informacionPersonal)
        {
            var db = dbConnection();

            var sql = @"

                        INSERT INTO public.informacion_personal (id_usuario, nombre, apellido_paterno, apellido_materno)
                        VALUES(@Id_Usuario, @Nombre, @Apellido_Paterno, @Apellido_Materno) RETURNING id_usuario;
                        ";

            var result = await db.QueryAsync<int>(sql, new { Id_Usuario = informacionPersonal.Id_Usuario, Nombre = informacionPersonal.Nombre, Apellido_Paterno = informacionPersonal.Apellido_Paterno, Apellido_Materno = informacionPersonal.Apellido_Materno });
            return informacionPersonal.Id_Usuario;
            //return result.Single();
        }

        public async Task<GetRoles> VerRoles(int id)
        {
            var db = dbConnection();
            var sql = @"

                        SELECT id, nombre
                        FROM public.roles
                        WHERE id = @Id
                        ";

            var result = await db.QueryFirstOrDefaultAsync<GetRoles>(sql, new { Id = id });
            return result;
        }

        public async Task<GetPuestos> VerPuestos(int id)
        {
            var db = dbConnection();
            var sql = @"

                        SELECT id, nombre
                        FROM public.puestos
                        WHERE id = @Id
                        ";

            var result = await db.QueryFirstOrDefaultAsync<GetPuestos>(sql, new { Id = id });
            return result;
        }
    }
}
