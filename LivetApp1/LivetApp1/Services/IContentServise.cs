using LivetApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetApp1.Services
{
    public interface IContentServise
    {                                        //ここ！ここ！ここ！
        Task<List<Content>> Get();
        Task<String> Put(Content content);
        Task<String> Post(Content content);
        Task<String> Delete(Content content);
        String GetContentType();
    }
}