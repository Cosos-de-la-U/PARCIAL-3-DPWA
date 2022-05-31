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
    public class PortafolioController : ControllerBase
    {
        private readonly railwayContext _context;

        public PortafolioController(railwayContext context)
        {
            _context = context;
        }

        // GET: api/Portafolio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portafolio>>> GetPortafolios()
        {
          if (_context.Portafolios == null)
          {
              return NotFound();
          }
            return await _context.Portafolios.ToListAsync();
        }

        // GET: api/Portafolio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Portafolio>> GetPortafolio(int id)
        {
          if (_context.Portafolios == null)
          {
              return NotFound();
          }
            var portafolio = await _context.Portafolios.FindAsync(id);

            if (portafolio == null)
            {
                return NotFound();
            }

            return portafolio;
        }

        // PUT: api/Portafolio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortafolio(int id, Portafolio portafolio)
        {
            if (id != portafolio.Id)
            {
                return BadRequest();
            }

            _context.Entry(portafolio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortafolioExists(id))
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

        // POST: api/Portafolio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Portafolio>> PostPortafolio(Portafolio portafolio)
        {
          if (_context.Portafolios == null)
          {
              return Problem("Entity set 'railwayContext.Portafolios'  is null.");
          }
            _context.Portafolios.Add(portafolio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPortafolio", new { id = portafolio.Id }, portafolio);
        }

        // DELETE: api/Portafolio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortafolio(int id)
        {
            if (_context.Portafolios == null)
            {
                return NotFound();
            }
            var portafolio = await _context.Portafolios.FindAsync(id);
            if (portafolio == null)
            {
                return NotFound();
            }

            _context.Portafolios.Remove(portafolio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortafolioExists(int id)
        {
            return (_context.Portafolios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
