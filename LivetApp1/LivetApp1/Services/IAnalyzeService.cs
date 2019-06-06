using LivetApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetApp1.Services
{
    interface IAnalyzeService
    {
        Task<List<AnalyzeUtoU>> GetAnalyzeUtoUs(Term term);
        Task<List<AnalyzeDtoD>> GetAnalyzeDtoDs(Term term);
    }
}
