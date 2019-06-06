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
    public class AnalyzeUtoUController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AnalyzeUtoUController(ApplicationContext context)
        {
            _context = context;
        }


        // POST: api/AnalyzeUtoU
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AnalyzeUtoU>>> Post([FromBody] Term Terms)
        {
            if (Terms.Term1 > Terms.Term2)
            {
                DateTime a = Terms.Term2;
                Terms.Term2 = Terms.Term1;
                Terms.Term1 = a;
            }

            return await _context.ThanksCards
                            .Include(ThanksCard => ThanksCard.To)
                            .Include(ThanksCard => ThanksCard.To.Department)
                            .Where(x => x.PostDate >= Terms.Term1 && x.PostDate <= Terms.Term2)
                            .GroupBy(x => x.ToId)
                            .Select(x => new AnalyzeUtoU
                            {
                                Name = x.Select(y => y.To.Name).ToList()[0]
                                , DepartmentName = x.Select(y => y.To.Department.DepartmentName).ToList()[0]
                                , ThanksCount = x.Select(y => y.ThanksCount).Sum()
                            }).ToListAsync();

        }

    }
}
