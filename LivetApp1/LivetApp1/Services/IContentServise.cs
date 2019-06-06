using LivetApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetApp1.Services
{
    interface IContentServise
    {                                        //ここ！ここ！ここ！
        Task<String> Help1ContentAsync(Help1Content help1Content);
        Task<String> Help2ContentAsync(Help2Content help2Content);
        Task<String> PlaceContentAsync(PlaceContent placeContent);
    }
}