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
    public class ReportController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ReportController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Report
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ThanksCard>>> Post([FromBody] Term Terms)
        {

            if (Terms.Term1 > Terms.Term2)
            {
                DateTime a = Terms.Term2;
                Terms.Term2 = Terms.Term1;
                Terms.Term1 = a;
            }

            

            return await _context.ThanksCards.Include(ThanksCard => ThanksCard.To)
                                             .Include(ThanksCard => ThanksCard.To.Department)
                                             .Include(ThanksCard => ThanksCard.From)
                                             .Include(ThanksCard => ThanksCard.From.Department)
                                             .Where(x => 
                                                        x.PostDate >= Terms.Term1 
                                                        && 
                                                        x.PostDate <= Terms.Term2 
                                                        && 
                                                        x.IsRepresentative)
                                                                            .ToListAsync();
        }
    }
}
