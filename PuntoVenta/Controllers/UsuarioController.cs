using Microsoft.AspNetCore.Mvc;
using PuntoVenta.Data.Repositories;
using PuntoVenta.Model;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace PuntoVenta.Controllers
{
    [Route("api")]
    [ApiController]
    
    public class UsuarioController : Controller
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration configurations;

        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration _configurations)
        {
            _usuarioRepository = usuarioRepository;
            configurations = _configurations;
        }
        
        [Route("ObtenerUsuarios")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            return Ok(await _usuarioRepository.ObtenerUsuarios());
        }

        
        [HttpGet]
        [Route("ObtenerUsuario{id}")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            return Ok(await _usuarioRepository.ObtenerUsuario(id));
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> ValidarUsuario([FromBody] Login usuario)
        {

            var test = await _usuarioRepository.ValidarUsuario(usuario);
 
           
                if (!object.ReferenceEquals(null, test))
                {
                    if (usuario.Clave == test.Clave)
                    {
                        var secretKey = configurations.GetValue<string>("SecretKey");
                        var key = Encoding.ASCII.GetBytes(secretKey);
                        var claims = new ClaimsIdentity();
                        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, test.Correo));

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = claims,
                            Expires = DateTime.UtcNow.AddHours(4),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                        string bearer_token = tokenHandler.WriteToken(createdToken);
                        return Ok(bearer_token);
                        //return Ok("las contraseñas coinciden");
                    }
                    else
                    {
                        return BadRequest("Contraseña incorrecta");
                    }
                }else
                {
                    return BadRequest("correo no existe");
                }            
           
        }

        
        [Route("NuevoUsuario")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertUsuario([FromBody] CrearUsuario usuario)
        {
            var test = await _usuarioRepository.InsertUsuario(usuario);
            usuario.Id = test;
            var roles = usuario.Roles;

            roles[0].Id_Usuario = test;
            var test2 = await _usuarioRepository.InsertRol(roles[0]);
            
            var puestos = usuario.Puesto;
            puestos[0].Id_Usuario = test;
            var test3 = await _usuarioRepository.InsertPuesto(puestos[0]);

            var informacionPersonal = usuario.InformacionPersonal;
            informacionPersonal.Id_Usuario = test;
            var test4 = await _usuarioRepository.InsertInformacionPersonal(informacionPersonal);

            return Created("Created", usuario);
            /*
           
            var roles = usuario.Roles;
            var test2 = await _usuarioRepository.NuevoUsuarioInformacion(usuario);
            return Ok(test2);
            */

        }




        [HttpGet]
        [Route("VerUsuario/{id}")]
        //[Authorize]
        public async Task<IActionResult> VerUsuario( int id)
        {
            
            var test = await _usuarioRepository.VerUsuario(id);

            var test2 = await _usuarioRepository.VerRolesUsuario(id);

            var test5 = await _usuarioRepository.VerRoles(test2.Id_Rol);

            var test3 = await _usuarioRepository.VerPuestosUsuario(id);

            var test6 = await _usuarioRepository.VerPuestos(test3.Id_Puesto);

            var test4 = await _usuarioRepository.VerInformacionPersonalUsuario(id);

            List<Roles> roles = new List<Roles>();
            List<GetRoles> rolesx = new List<GetRoles>();
            List<Puestos> puestos = new List<Puestos>();
            List<GetPuestos> puestosx = new List<GetPuestos>();

            ObtenerUsuario usuario = new ObtenerUsuario() { Id = id, Correo = test.Correo, Clave = test.Clave, Activo = test.Activo};

            roles.Add(test2);
            rolesx.Add(test5);
            usuario.Roles = rolesx;

            puestos.Add(test3);
            puestosx.Add(test6);
            usuario.Puesto = puestosx;

            usuario.InformacionPersonal = test4;

            return Ok(usuario);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return Ok(r == null ? "" : r.Value);
        }



    }
}
