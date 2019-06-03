using LivetApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetApp1.Services
{
    interface IRestService
    {
        Task<User> LogonAsync(User user);
        Task<String> PutUserAsync(User user);
        Task<ThanksCard> CreateAsync(ThanksCard thanksCard);
        Task<List<ThanksCard>> GetCardsAsync();
        Task<List<Department>> GetDepartmentsAsync();

        Task<ThanksCard> PutCard(ThanksCard thanksCard); //←ここに追加

    }
}