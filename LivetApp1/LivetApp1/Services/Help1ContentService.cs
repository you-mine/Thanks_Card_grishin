using LivetApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LivetApp1.Services
{
    class Help1ContentService : IContentServise
    {
        private HttpClient Client;
        private string BaseUrl;

        public Help1ContentService()
        {
            this.Client = new HttpClient();
            this.BaseUrl = "https://localhost:5001";
        }

        #region Delete

        public async Task<string> Delete(Content content)
        {
            try
            {
                var response = await Client.DeleteAsync(this.BaseUrl + "/api/Help1Content/" + content.Id);

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

        #endregion

        #region Get

        public async Task<List<Content>> Get()
        {
            List<Content> ContentList = null;

            try
            {

                var response = await Client.GetAsync(this.BaseUrl + "/api/Help1Content");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ContentList = JsonConvert.DeserializeObject<List<Content>>(responseContent);
                }
 
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return ContentList;
        }

        #endregion

        #region Post

        public async Task<string> Post(Content help1Content)
        {
            var jObject = JsonConvert.SerializeObject(help1Content);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/Help1Content", content);

                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return "fail";
        }

        public string GetContentType()
        {
            return "感謝事項1";
        }

        #endregion

        #region Put

        public async Task<string> Put(Content help1Content)
        {

            var jObject = JsonConvert.SerializeObject(help1Content);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                var response = await Client.PutAsync(this.BaseUrl + "/api/Help1Content/" + help1Content.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.PutUserAsync: " + e);
            }
            return "fail";
        }

        #endregion
    }
}
