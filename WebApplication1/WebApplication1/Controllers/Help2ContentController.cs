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
    public class Help2ContentController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public Help2ContentController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Help2Content
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Help2Content>>> Gethelp2Content()
        {
            return await _context.help2Content.ToListAsync();
        }

        // GET: api/Help2Content/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Help2Content>> GetHelp2Content(int id)
        {
            var help2Content = await _context.help2Content.FindAsync(id);

            if (help2Content == null)
            {
                return NotFound();
            }

            return help2Content;
        }

        // PUT: api/Help2Content/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelp2Content(int id, Help2Content help2Content)
        {
            List<Help2Content> Exist = _context.help2Content.Where(x => x.Id != id && help2Content.CD == x.CD).ToList();
            if(Exist.Count > 0)
            {
                return BadRequest();
            }

            if (id != help2Content.Id)
            {
                return BadRequest();
            }

            _context.Entry(help2Content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Help2ContentExists(id))
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

        // POST: api/Help2Content
        [HttpPost]
        public async Task<ActionResult<Help2Content>> PostHelp2Content(Help2Content help2Content)
        {
            List<Help2Content> Exist = _context.help2Content.Where(x => help2Content.CD == x.CD).ToList();
            if (Exist.Count > 0)
            {
                return BadRequest();
            }
            _context.help2Content.Add(help2Content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelp2Content", new { id = help2Content.Id }, help2Content);
        }

        // DELETE: api/Help2Content/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Help2Content>> DeleteHelp2Content(int id)
        {
            var help2Content = await _context.help2Content.FindAsync(id);
            if (help2Content == null)
            {
                return NotFound();
            }

            _context.help2Content.Remove(help2Content);
            await _context.SaveChangesAsync();

            return help2Content;
        }

        private bool Help2ContentExists(int id)
        {
            return _context.help2Content.Any(e => e.Id == id);
        }
    }
}
