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
    public class PlaceContentsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PlaceContentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/PlaceContents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceContent>>> GetplaceContent()
        {
            return await _context.placeContent.ToListAsync();
        }

        // GET: api/PlaceContents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceContent>> GetPlaceContent(int id)
        {
            var placeContent = await _context.placeContent.FindAsync(id);

            if (placeContent == null)
            {
                return NotFound();
            }

            return placeContent;
        }

        // PUT: api/PlaceContents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceContent(int id, PlaceContent placeContent)
        {
            if (id != placeContent.Id)
            {
                return BadRequest();
            }

            _context.Entry(placeContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceContentExists(id))
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

        // POST: api/PlaceContents
        [HttpPost]
        public async Task<ActionResult<PlaceContent>> PostPlaceContent(PlaceContent placeContent)
        {
            _context.placeContent.Add(placeContent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaceContent", new { id = placeContent.Id }, placeContent);
        }

        // DELETE: api/PlaceContents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaceContent>> DeletePlaceContent(int id)
        {
            var placeContent = await _context.placeContent.FindAsync(id);
            if (placeContent == null)
            {
                return NotFound();
            }

            _context.placeContent.Remove(placeContent);
            await _context.SaveChangesAsync();

            return placeContent;
        }

        private bool PlaceContentExists(int id)
        {
            return _context.placeContent.Any(e => e.Id == id);
        }
    }
}
