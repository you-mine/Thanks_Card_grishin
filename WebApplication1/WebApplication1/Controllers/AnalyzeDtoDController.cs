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
    public class AnalyzeDtoDController : ControllerBase
    {

        private readonly ApplicationContext _context;

        public AnalyzeDtoDController(ApplicationContext context)
        {
            _context = context;
        }




        // POST: api/AnalyzeDtoD
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AnalyzeDtoD>>> Post([FromBody] Term Terms)
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
                                             .Include(ThanksCard => ThanksCard.From)
                                             .Include(ThanksCard => ThanksCard.From.Department)
                                             .Where(x => x.PostDate >= Terms.Term1 && x.PostDate <= Terms.Term2)
                                             .GroupBy(x => new {ToId = x.To.Department.DepartmentName , FromID = x.From.Department.DepartmentName})
                                             .Select(x => new AnalyzeDtoD
                                             {
                                                 ToDepartmentName = x.Select(y => y.To.Department.DepartmentName).ToList()[0]
                                                 ,FromDepartmentName =x.Select(y => y.From.Department.DepartmentName).ToList()[0]
                                                 ,ThanksCount = x.Select(y => y.ThanksCount).Sum()
                                             }
                                                 ).ToListAsync();
                                             
        }

    }
}
