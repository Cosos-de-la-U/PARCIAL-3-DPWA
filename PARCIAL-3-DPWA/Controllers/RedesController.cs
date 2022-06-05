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
    public class RedesController : ControllerBase
    {
        private readonly railwayContext _context;

        public RedesController(railwayContext context)
        {
            _context = context;
        }

        // GET: api/Redes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Red>>> GetReds()
        {
          if (_context.Reds == null)
          {
              return NotFound();
          }
            return await _context.Reds.ToListAsync();
        }

        // GET: api/Redes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Red>> GetRed(int id)
        {
          if (_context.Reds == null)
          {
              return NotFound();
          }
            var red = await _context.Reds.FindAsync(id);

            if (red == null)
            {
                return NotFound();
            }

            return red;
        }

        // PUT: api/Redes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRed(int id, Red red)
        {
            if (id != red.Id_red)
            {
                return BadRequest();
            }

            _context.Entry(red).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RedExists(id))
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

        // POST: api/Redes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Red>> PostRed(Red red)
        {
          if (_context.Reds == null)
          {
              return Problem("Entity set 'railwayContext.Reds'  is null.");
          }
            _context.Reds.Add(red);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRed", new { id = red.Id_red }, red);
        }

        // DELETE: api/Redes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRed(int id)
        {
            if (_context.Reds == null)
            {
                return NotFound();
            }
            var red = await _context.Reds.FindAsync(id);
            if (red == null)
            {
                return NotFound();
            }

            _context.Reds.Remove(red);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RedExists(int id)
        {
            return (_context.Reds?.Any(e => e.Id_red == id)).GetValueOrDefault();
        }
    }
}
