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
    class AnalyzeService : IAnalyzeService
    {
        private HttpClient Client;
        private string BaseUrl;

        public AnalyzeService()
        {
            this.Client = new HttpClient();
            this.BaseUrl = "https://localhost:5001";
        }

        public async Task<List<AnalyzeUtoU>> GetAnalyzeUtoUs(Term term)
        {
            var jObject = JsonConvert.SerializeObject(term);

            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            List<AnalyzeUtoU> responseList = null;
            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/AnalyzeUtoU", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseList = JsonConvert.DeserializeObject<List<AnalyzeUtoU>>(responseContent);
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return responseList;
        }

        public async Task<List<AnalyzeDtoD>> GetAnalyzeDtoDs(Term term)
        {
            var jObject = JsonConvert.SerializeObject(term);

            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            List<AnalyzeDtoD> responseList = null;
            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/AnalyzeDtoD", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseList = JsonConvert.DeserializeObject<List<AnalyzeDtoD>>(responseContent);
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return responseList;
        }
    }
}
