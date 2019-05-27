using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LivetApp1.Models;
using Newtonsoft.Json;

namespace LivetApp1.Services
{
    class ShowUserService : IShowUserService
    {
        private HttpClient Client;
        private string BaseUrl;

        public ShowUserService()
        {
            this.Client = new HttpClient();
            this.BaseUrl = "https://localhost:5001";
        }

        public async Task<List<User>> ShowUserAsync()
        {
            //POSTで送るもののJson変換と思われるため、触らない。
            //var jObject = JsonConvert.SerializeObject(user);

            //ResponseがListでこないかなーっていうクソみたいな希望
            List<User> responseUser = null;
            try
            {
                var response = await Client.GetAsync(this.BaseUrl + "/api/Users");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseUser = JsonConvert.DeserializeObject<List<User>>(responseContent);
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return responseUser;
        }
    }
}
