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
    public class CertificacionController : ControllerBase
    {
        private readonly railwayContext _context;

        public CertificacionController(railwayContext context)
        {
            _context = context;
        }

        // GET: api/Certificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificacion>>> GetCertificacions()
        {
          if (_context.Certificacions == null)
          {
              return NotFound();
          }
            return await _context.Certificacions.ToListAsync();
        }

        // GET: api/Certificacion/5
        [HttpGet("{certificacion}")]
        public async Task<ActionResult<Certificacion>> GetCertificacion(int id)
        {
          if (_context.Certificacions == null)
          {
              return NotFound();
          }
            var certificacion = await _context.Certificacions.FindAsync(id);

            if (certificacion == null)
            {
                return NotFound();
            }

            return certificacion;
        }

        // PUT: api/Certificacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{certificacion}")]
        public async Task<IActionResult> PutCertificacion(int id, Certificacion certificacion)
        {
            if (id != certificacion.Id_certificacion)
            {
                return BadRequest();
            }

            _context.Entry(certificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificacionExists(id))
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

        // POST: api/Certificacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Certificacion>> PostCertificacion(Certificacion certificacion)
        {
          if (_context.Certificacions == null)
          {
              return Problem("Entity set 'railwayContext.Certificacions'  is null.");
          }
            _context.Certificacions.Add(certificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificacion", new { id = certificacion.Id_certificacion }, certificacion);
        }

        // DELETE: api/Certificacion/5
        [HttpDelete("{certificacion}")]
        public async Task<IActionResult> DeleteCertificacion(int id)
        {
            if (_context.Certificacions == null)
            {
                return NotFound();
            }
            var certificacion = await _context.Certificacions.FindAsync(id);
            if (certificacion == null)
            {
                return NotFound();
            }

            _context.Certificacions.Remove(certificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertificacionExists(int id)
        {
            return (_context.Certificacions?.Any(e => e.Id_certificacion == id)).GetValueOrDefault();
        }
    }
}
