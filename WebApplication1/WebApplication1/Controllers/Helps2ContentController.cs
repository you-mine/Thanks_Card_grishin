using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Helps2ContentController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public Helps2ContentController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Helps2Content
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Helps2Content>>> GetHelps2()
        {
            return await _context.Helps2.ToListAsync();
        }

        // GET: api/Helps2Content/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Helps2Content>> GetHelps2Content(int id)
        {
            var helps2Content = await _context.Helps2.FindAsync(id);

            if (helps2Content == null)
            {
                return NotFound();
            }

            return helps2Content;
        }

        // PUT: api/Helps2Content/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelps2Content(int id, Helps2Content helps2Content)
        {
            if (id != helps2Content.Id)
            {
                return BadRequest();
            }

            _context.Entry(helps2Content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Helps2ContentExists(id))
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

        // POST: api/Helps2Content
        [HttpPost]
        public async Task<ActionResult<Helps2Content>> PostHelps2Content(Helps2Content helps2Content)
        {
            _context.Helps2.Add(helps2Content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelps2Content", new { id = helps2Content.Id }, helps2Content);
        }

        // DELETE: api/Helps2Content/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Helps2Content>> DeleteHelps2Content(int id)
        {
            var helps2Content = await _context.Helps2.FindAsync(id);
            if (helps2Content == null)
            {
                return NotFound();
            }

            _context.Helps2.Remove(helps2Content);
            await _context.SaveChangesAsync();

            return helps2Content;
        }

        private bool Helps2ContentExists(int id)
        {
            return _context.Helps2.Any(e => e.Id == id);
        }
    }
}
