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
    public class Help1ContentController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public Help1ContentController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Help1Content
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Help1Content>>> Gethelp1Content()
        {
            return await _context.help1Content.ToListAsync();
        }

        // GET: api/Help1Content/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Help1Content>> GetHelp1Content(int id)
        {
            var help1Content = await _context.help1Content.FindAsync(id);

            if (help1Content == null)
            {
                return NotFound();
            }

            return help1Content;
        }

        // PUT: api/Help1Content/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelp1Content(int id, Help1Content help1Content)
        {
            List<Help1Content> Exists = _context.help1Content.Where(x => x.Id != id && help1Content.CD == x.CD).ToList();
            if(Exists.Count > 0)
            {
                return BadRequest();
            }

            if (id != help1Content.Id)
            {
                return BadRequest();
            }

            _context.Entry(help1Content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Help1ContentExists(id))
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

        // POST: api/Help1Content
        [HttpPost]
        public async Task<ActionResult<Help1Content>> PostHelp1Content(Help1Content help1Content)
        {
            List<Help1Content> Exists = _context.help1Content.Where(x => help1Content.CD == x.CD).ToList();
            if (Exists.Count > 0)
            {
                return BadRequest();
            }
            _context.help1Content.Add(help1Content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelp1Content", new { id = help1Content.Id }, help1Content);
        }

        // DELETE: api/Help1Content/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Help1Content>> DeleteHelp1Content(int id)
        {
            var help1Content = await _context.help1Content.FindAsync(id);
            if (help1Content == null)
            {
                return NotFound();
            }

            _context.help1Content.Remove(help1Content);
            await _context.SaveChangesAsync();

            return help1Content;
        }

        private bool Help1ContentExists(int id)
        {
            return _context.help1Content.Any(e => e.Id == id);
        }
    }
}
