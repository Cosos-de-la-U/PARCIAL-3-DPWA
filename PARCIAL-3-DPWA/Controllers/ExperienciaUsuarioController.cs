using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL_3_DPWA.Models;

namespace PARCIAL_3_DPWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienciaUsuarioController : ControllerBase
    {
        private readonly railwayContext _context;

        public ExperienciaUsuarioController(railwayContext context)
        {
            _context = context;
        }

        // GET: api/ExperienciaUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExperienciaByUsuario>>> GetExperienciaByUsuarios()
        {
          if (_context.ExperienciaByUsuarios == null)
          {
              return NotFound();
          }
            return await _context.ExperienciaByUsuarios.ToListAsync();
        }

        // GET: api/ExperienciaUsuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExperienciaByUsuario>> GetExperienciaByUsuario(int id)
        {
          if (_context.ExperienciaByUsuarios == null)
          {
              return NotFound();
          }
            var experienciaByUsuario = await _context.ExperienciaByUsuarios.FindAsync(id);

            if (experienciaByUsuario == null)
            {
                return NotFound();
            }

            return experienciaByUsuario;
        }

        // PUT: api/ExperienciaUsuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperienciaByUsuario(int id, ExperienciaByUsuario experienciaByUsuario)
        {
            if (id != experienciaByUsuario.IdExperienciaByUsuario)
            {
                return BadRequest();
            }

            _context.Entry(experienciaByUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienciaByUsuarioExists(id))
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

        // POST: api/ExperienciaUsuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExperienciaByUsuario>> PostExperienciaByUsuario(ExperienciaByUsuario experienciaByUsuario)
        {
          if (_context.ExperienciaByUsuarios == null)
          {
              return Problem("Entity set 'railwayContext.ExperienciaByUsuarios'  is null.");
          }
            _context.ExperienciaByUsuarios.Add(experienciaByUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExperienciaByUsuario", new { id = experienciaByUsuario.IdExperienciaByUsuario }, experienciaByUsuario);
        }

        // DELETE: api/ExperienciaUsuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperienciaByUsuario(int id)
        {
            if (_context.ExperienciaByUsuarios == null)
            {
                return NotFound();
            }
            var experienciaByUsuario = await _context.ExperienciaByUsuarios.FindAsync(id);
            if (experienciaByUsuario == null)
            {
                return NotFound();
            }

            _context.ExperienciaByUsuarios.Remove(experienciaByUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExperienciaByUsuarioExists(int id)
        {
            return (_context.ExperienciaByUsuarios?.Any(e => e.IdExperienciaByUsuario == id)).GetValueOrDefault();
        }
    }
}
