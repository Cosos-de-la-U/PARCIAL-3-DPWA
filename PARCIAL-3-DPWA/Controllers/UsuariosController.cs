using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL_3_DPWA.Models;
using PARCIAL_3_DPWA.Models.ViewModel;

namespace PARCIAL_3_DPWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly railwayContext _context;

        public UsuariosController(railwayContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/uname
        [HttpGet("{uname}")]
        public async Task<ActionResult<Usuario>> GetUsuario(String uname)
        {
          if (_context.Usuarios == null)
          {
              return NotFound($"El usuario {uname} no existe 😓");
          }
            //Obteniendo id de usuario
            var idUsuario = (from u in _context.Usuarios
                            where u.U_name == uname
                            select u.Id_usuario).First();

            //Obteniendo datos del usuario
            var usuario = from u in _context.Usuarios
                          join redU in _context.RedByUsers on u.Id_usuario equals redU.Id_usuario
                          join gradoU in _context.GradoAcademicoByUsuarios on u.Id_usuario equals gradoU.Id_usuario
                          join expU in _context.ExperienciaByUsuarios on u.Id_usuario equals expU.Id_usuario
                          join certU in _context.CertificacionByUsuarios on u.Id_usuario equals certU.Id_usuario
                          join cert in _context.Certificacions on cert.
                          select new UsuarioModel{

                          };

            if (usuario == null)
            {
                return NotFound($"El usuario {uname} no existe 😓");
            }

            return await usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id_usuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'railwayContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { uname = usuario.U_name }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id_usuario == id)).GetValueOrDefault();
        }
    }
}
