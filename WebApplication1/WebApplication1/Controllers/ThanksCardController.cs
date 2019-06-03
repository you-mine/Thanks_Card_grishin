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
    public class ThanksCardController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ThanksCardController(ApplicationContext context)
        {
            _context = context;
        }

        #region GetThanksCards
        // GET: api/ThanksCard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThanksCard>>> GetThanksCards()
        {
            // Include を指定することで From, To (Userモデル) を同時に取得する。
            return await _context.ThanksCards
                                    .Include(ThanksCard => ThanksCard.From)
                                    .Include(ThanksCard => ThanksCard.To)
                                    .ToListAsync();
        }
        #endregion

        // POST api/ThanksCard
        [HttpPost]
        public async Task<ActionResult<ThanksCard>> Post([FromBody] ThanksCard thanksCard)
        {
            // From, To には既に存在しているユーザが入るため、更新の対象から外す。

            //_context.Department.Attach(thanksCard.From.Department);
            //_context.Department.Attach(thanksCard.To.Department);
            //_context.Users.Attach(thanksCard.From);
            //_context.Users.Attach(thanksCard.To);


            _context.ThanksCards.Add(thanksCard);
            await _context.SaveChangesAsync();
            // TODO: Error Handling
            return thanksCard;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutThanksCard(long id, ThanksCard thanksCard)
        {
            if (id != thanksCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(thanksCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThanksCardExists(id))
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

        private bool ThanksCardExists(long id)
        {
            return _context.ThanksCards.Any(e => e.Id == id);
        }
    }
}