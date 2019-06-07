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
    class PutDepartmentService : IEditDepartment
    {
        private HttpClient Client;
        private string BaseUrl;

        public PutDepartmentService()
        {
            this.Client = new HttpClient();
            this.BaseUrl = "https://localhost:5001";
        }

        public async Task<string> EditDepartment(Department department)
        {
            var jObject = JsonConvert.SerializeObject(department);

            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                var response = await Client.PutAsync(this.BaseUrl + "/api/Departments/"+department.Id , content);

                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return "fail";
        }

    }
}
