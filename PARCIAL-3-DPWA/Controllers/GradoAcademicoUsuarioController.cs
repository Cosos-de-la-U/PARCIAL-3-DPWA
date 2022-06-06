using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL_3_DPWA.Models;
using PARCIAL_3_DPWA.Models.ViewModel.GradoAcademico;

namespace PARCIAL_3_DPWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradoAcademicoUsuarioController : ControllerBase
    {
        private readonly railwayContext _context;

        public GradoAcademicoUsuarioController(railwayContext context)
        {
            _context = context;
        }

        // GET: api/GradoAcademicoUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradoAcademicoByUsuario>>> GetGradoAcademicoByUsuarios()
        {
            if (_context.GradoAcademicoByUsuarios == null)
            {
                return NotFound();
            }
            // Sacando datos ordenados
            var GradoAcademicoByUsuario = await (from redU in _context.GradoAcademicoByUsuarios
                                      orderby redU.Id_usuario ascending
                                      select redU).ToListAsync();

            List<GradoAcademicoModel> ListaGradoAcademicoModel = new List<GradoAcademicoModel>();
            foreach (GradoAcademicoByUsuario gradoAcademico in GradoAcademicoByUsuario)
            {
                //Obteniendo usuario id
                var usuarioU_name = ObtenerU_nameUsuario(gradoAcademico.Id_usuario ?? default(int)).Result;

                // Verificando que hayan dados en la lista
                if (ListaGradoAcademicoModel.Count != 0)
                {
                    // Verificando que no se repitan
                    if (ListaGradoAcademicoModel.LastOrDefault().U_name.Equals(usuarioU_name))
                    {
                        continue;
                    };
                }

                //Uniendo
                GradoAcademicoModel GradoAcademicoModel = new GradoAcademicoModel
                {
                    U_name = usuarioU_name,
                    Profesion = gradoAcademico.Profesion,
                    Universidad = gradoAcademico.Universidad,
                    Objetivos = gradoAcademico.Objetivos

                };

                ListaGradoAcademicoModel.Add(GradoAcademicoModel);

            }

            return Ok(ListaGradoAcademicoModel);
        }

        // GET: api/GradoAcademicoUsuario/5
        [HttpGet("{u_name}")]
        public async Task<ActionResult<GradoAcademicoByUsuario>> GetGradoAcademicoByUsuario(String u_name)
        {
            //Obteniendo usuario id
            var usuarioId = ObtenerIdUsuario(u_name).Result;

            var gradoAcademicoUsuario = ObtenerObjetoGradoAcademicoByUsuarios(usuarioId).Result;

            //Uniendo
            GradoAcademicoModel GradoAcademicoModel = new GradoAcademicoModel
            {
                U_name = u_name,
                Profesion = gradoAcademicoUsuario.Profesion,
                Universidad = gradoAcademicoUsuario.Universidad,
                Objetivos = gradoAcademicoUsuario.Objetivos

            };

            return Ok(GradoAcademicoModel);
    }

        // PUT: api/GradoAcademicoUsuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{u_name}")]
        public async Task<IActionResult> PutGradoAcademicoByUsuario(int id, GradoAcademicoByUsuario gradoAcademicoByUsuario)
        {
            if (id != gradoAcademicoByUsuario.Id_grado_academico_by_usuario)
            {
                return BadRequest();
            }

            _context.Entry(gradoAcademicoByUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradoAcademicoByUsuarioExists(id))
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

        // POST: api/GradoAcademicoUsuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GradoAcademicoByUsuario>> PostGradoAcademicoByUsuario(GradoAcademicoByUsuario gradoAcademicoByUsuario)
        {
          if (_context.GradoAcademicoByUsuarios == null)
          {
              return Problem("Entity set 'railwayContext.GradoAcademicoByUsuarios'  is null.");
          }
            _context.GradoAcademicoByUsuarios.Add(gradoAcademicoByUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGradoAcademicoByUsuario", new { id = gradoAcademicoByUsuario.Id_grado_academico_by_usuario }, gradoAcademicoByUsuario);
        }

        // DELETE: api/GradoAcademicoUsuario/5
        [HttpDelete("{u_name}")]
        public async Task<IActionResult> DeleteGradoAcademicoByUsuario(int id)
        {
            if (_context.GradoAcademicoByUsuarios == null)
            {
                return NotFound();
            }
            var gradoAcademicoByUsuario = await _context.GradoAcademicoByUsuarios.FindAsync(id);
            if (gradoAcademicoByUsuario == null)
            {
                return NotFound();
            }

            _context.GradoAcademicoByUsuarios.Remove(gradoAcademicoByUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradoAcademicoByUsuarioExists(int id)
        {
            return (_context.GradoAcademicoByUsuarios?.Any(e => e.Id_grado_academico_by_usuario == id)).GetValueOrDefault();
        }
        private async Task<int> ObtenerIdUsuario(String u_name)
        {
            //Obteniendo usuario id
            return await (from u in _context.Usuarios
                          where u.U_name == u_name
                          select u.Id_usuario).FirstOrDefaultAsync();
        }
        private async Task<String> ObtenerU_nameUsuario(int idUsuario)
        {
            //Obteniendo usuario id
            return await (from u in _context.Usuarios
                          where u.Id_usuario == idUsuario
                          select u.U_name).FirstOrDefaultAsync();
        }
        private async Task<int> ObtenerIdRed(String nombreRed)
        {
            //Obteniendo red id
            return await (from r in _context.Reds
                          where r.Nombre == nombreRed
                          select r.Id_red).FirstOrDefaultAsync();
        }

        private async Task<int> ObtenerRedByUsuarios(int idUsuario, int idRed)
        {
            //Obteniendo red id
            return await (from r in _context.RedByUsers
                          where r.Id_usuario == idUsuario && r.Id_red == idRed
                          select r.Id_red_by_user).FirstOrDefaultAsync();
        }

        private async Task<GradoAcademicoByUsuario> ObtenerObjetoGradoAcademicoByUsuarios(int idUsuario)
        {
            //Obteniendo RedByUsuario
            return await (from gaU in _context.GradoAcademicoByUsuarios
                          where gaU.Id_usuario == idUsuario
                          select gaU).FirstOrDefaultAsync();
        }
    }
}
