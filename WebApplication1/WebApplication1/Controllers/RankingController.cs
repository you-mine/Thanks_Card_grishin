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
    public class RankingController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public RankingController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ranking>>> GetRanking()
        {

            return await _context.ThanksCards
                             .Include(ThanksCard => ThanksCard.To)
                             .Include(ThanksCard => ThanksCard.To.Department)
                             .GroupBy(x => x.To)
                             .Select(
                                         x => new Ranking
                                         {
                                             Name = x.Select(y => y.To.Name).ToList()[0]
                                                 ,
                                             DepartmentName = x.Select(y => y.To.Department.DepartmentName).ToList()[0]
                                                 ,
                                             ThanksCount = x.Select(y => y.ThanksCount).Sum()
                                         }

                                     ).OrderByDescending(x => x.ThanksCount).ToListAsync();


        }
    }
}