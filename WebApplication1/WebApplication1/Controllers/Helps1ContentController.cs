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
    public class Helps1ContentController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public Helps1ContentController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Helps1Content
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Helps1Content>>> GetHelps1()
        {
            return await _context.Helps1.ToListAsync();
        }

        // GET: api/Helps1Content/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Helps1Content>> GetHelps1Content(int id)
        {
            var helps1Content = await _context.Helps1.FindAsync(id);

            if (helps1Content == null)
            {
                return NotFound();
            }

            return helps1Content;
        }

        // PUT: api/Helps1Content/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelps1Content(int id, Helps1Content helps1Content)
        {
            if (id != helps1Content.Id)
            {
                return BadRequest();
            }

            _context.Entry(helps1Content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Helps1ContentExists(id))
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

        // POST: api/Helps1Content
        [HttpPost]
        public async Task<ActionResult<Helps1Content>> PostHelps1Content(Helps1Content helps1Content)
        {
            _context.Helps1.Add(helps1Content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelps1Content", new { id = helps1Content.Id }, helps1Content);
        }

        // DELETE: api/Helps1Content/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Helps1Content>> DeleteHelps1Content(int id)
        {
            var helps1Content = await _context.Helps1.FindAsync(id);
            if (helps1Content == null)
            {
                return NotFound();
            }

            _context.Helps1.Remove(helps1Content);
            await _context.SaveChangesAsync();

            return helps1Content;
        }

        private bool Helps1ContentExists(int id)
        {
            return _context.Helps1.Any(e => e.Id == id);
        }
    }
}
