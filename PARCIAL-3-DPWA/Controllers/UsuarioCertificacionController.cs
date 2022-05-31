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
    public class UsuarioCertificacionController : ControllerBase
    {
        private readonly railwayContext _context;

        public UsuarioCertificacionController(railwayContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioCertificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioCertificacion>>> GetUsuarioCertificacions()
        {
          if (_context.UsuarioCertificacions == null)
          {
              return NotFound();
          }
            return await _context.UsuarioCertificacions.ToListAsync();
        }

        // GET: api/UsuarioCertificacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioCertificacion>> GetUsuarioCertificacion(int id)
        {
          if (_context.UsuarioCertificacions == null)
          {
              return NotFound();
          }
            var usuarioCertificacion = await _context.UsuarioCertificacions.FindAsync(id);

            if (usuarioCertificacion == null)
            {
                return NotFound();
            }

            return usuarioCertificacion;
        }

        // PUT: api/UsuarioCertificacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioCertificacion(int id, UsuarioCertificacion usuarioCertificacion)
        {
            if (id != usuarioCertificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuarioCertificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioCertificacionExists(id))
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

        // POST: api/UsuarioCertificacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioCertificacion>> PostUsuarioCertificacion(UsuarioCertificacion usuarioCertificacion)
        {
          if (_context.UsuarioCertificacions == null)
          {
              return Problem("Entity set 'railwayContext.UsuarioCertificacions'  is null.");
          }
            _context.UsuarioCertificacions.Add(usuarioCertificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioCertificacion", new { id = usuarioCertificacion.Id }, usuarioCertificacion);
        }

        // DELETE: api/UsuarioCertificacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioCertificacion(int id)
        {
            if (_context.UsuarioCertificacions == null)
            {
                return NotFound();
            }
            var usuarioCertificacion = await _context.UsuarioCertificacions.FindAsync(id);
            if (usuarioCertificacion == null)
            {
                return NotFound();
            }

            _context.UsuarioCertificacions.Remove(usuarioCertificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioCertificacionExists(int id)
        {
            return (_context.UsuarioCertificacions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
