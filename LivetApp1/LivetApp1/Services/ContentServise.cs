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
    class ContentServise : IContentServise
    {
        private HttpClient Client;
        private string BaseUrl;

        public ContentServise()
        {
            this.Client = new HttpClient();
            this.BaseUrl = "https://localhost:5001";
        }
        #region Help1

        #region Help1Get
        public async Task<List<string>>GetHelp1ContentAsync()
        {
            List<Help1Content> responseHelp1Content = null;
            List<string> StringList = new List<string>();

            try
            {

                var response = await Client.GetAsync(this.BaseUrl + "/api/Help1Content");
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseHelp1Content = JsonConvert.DeserializeObject<List<Help1Content>>(responseContent);

                    foreach (Help1Content a in responseHelp1Content)
                    {
                        StringList.Add(a.Help1);
                    }
                }

            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return StringList;
        }
        #endregion

        #region Help1Post

        public async Task<string> PostHelp1ContentAsync(Help1Content help1Content)
        {
            var jObject = JsonConvert.SerializeObject(help1Content);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Help1Content responseHelp1Content = null;
            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/Help1Content", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseHelp1Content = JsonConvert.DeserializeObject<Help1Content>(responseContent);
                    return "succcess";
                }
               
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return "fail";
        }

        #endregion

        #region Help1Put

        public async Task<string> PutHelp1ContentAsync(Help1Content help1Content)
        {
            
          var jObject = JsonConvert.SerializeObject(help1Content);
        //Make Json object into content type
        var content = new StringContent(jObject);
        //Adding header of the contenttype
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        Help1Content responseHelp1Content = null;
            try
            {
                var response = await Client.PutAsync(this.BaseUrl + "/api/Help1Content/", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseHelp1Content = JsonConvert.DeserializeObject<Help1Content>(responseContent);
                    return "succcess";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.PutUserAsync: " + e);
            }
            return "fail";
        }

        #endregion

        #region Help1Delete

        public async Task<string> DeleteHelp1ContentAsync(Help1Content help1Content)
        {
            try
            {
                var response = await Client.DeleteAsync(this.BaseUrl + "/api/Help1Content/" + help1Content.Id);

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

        #endregion


        #region Help2

        #region Help2Get
        public async Task<List<string>> GetHelp2ContentAsync()
        {
            List<Help2Content> responseHelp2Content = null;
            List<string> StringList = new List<string>();

            try
            {

                var response = await Client.GetAsync(this.BaseUrl + "/api/Help2Content");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseHelp2Content = JsonConvert.DeserializeObject<List<Help2Content>>(responseContent);

                    foreach (Help2Content a in responseHelp2Content)
                    {
                        StringList.Add(a.Help2);
                    }
                }

            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return StringList;
        }
        #endregion

        #region Help2Post

        public async Task<string> PostHelp2ContentAsync(Help2Content help2Content)
        {
            var jObject = JsonConvert.SerializeObject(help2Content);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Help2Content responseHelp2Content = null;
            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/Help1Content", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseHelp2Content = JsonConvert.DeserializeObject<Help2Content>(responseContent);
                    return "succcess";
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return "fail";
        }

        #endregion

        #region Help2Put

        public async Task<string> PutHelp2ContentAsync(Help2Content help2Content)
        {

            var jObject = JsonConvert.SerializeObject(help2Content);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Help2Content responseHelp2Content = null;
            try
            {
                var response = await Client.PutAsync(this.BaseUrl + "/api/Help2Content/", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseHelp2Content = JsonConvert.DeserializeObject<Help2Content>(responseContent);
                    return "succcess";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.PutUserAsync: " + e);
            }
            return "fail";
        }

        #endregion

        #region Help2Delete

        public async Task<string> DeleteHelp2ContentAsync(Help2Content help2Content)
        {
            try
            {
                var response = await Client.DeleteAsync(this.BaseUrl + "/api/Help2Content/" + help2Content.Id);

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

        #endregion


        #region Place

        #region PlaceGet
        public async Task<List<string>> GetPlaceContentAsync()
        {
            List<PlaceContent> responsePlaceContent = null;
            List<string> StringList = new List<string>();

            try
            {

                var response = await Client.GetAsync(this.BaseUrl + "/api/PlaceContent");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responsePlaceContent = JsonConvert.DeserializeObject<List<PlaceContent>>(responseContent);

                    foreach (PlaceContent a in responsePlaceContent)
                    {
                        StringList.Add(a.Place);
                    }
                }

            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return StringList;
        }
        #endregion

        #region PlacePost

        public async Task<string> PostPlaceContentAsync(PlaceContent placeContent)
        {
            var jObject = JsonConvert.SerializeObject(placeContent);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            PlaceContent responsePlaceContent = null;
            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/PlaceContent", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responsePlaceContent = JsonConvert.DeserializeObject<PlaceContent>(responseContent);
                    return "succcess";
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return "fail";
        }

        #endregion

        #region PlacePut

        public async Task<string> PutPlaceContentAsync(PlaceContent PlaceContent)
        {

            var jObject = JsonConvert.SerializeObject(PlaceContent);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            PlaceContent responsePlaceContent = null;
            try
            {
                var response = await Client.PutAsync(this.BaseUrl + "/api/PlaceContent/", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responsePlaceContent = JsonConvert.DeserializeObject<PlaceContent>(responseContent);
                    return "succcess";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.PutUserAsync: " + e);
            }
            return "fail";
        }

        #endregion

        #region PlaceDelete

        public async Task<string> DeletePlaceContentAsync(PlaceContent placeContent)
        {
            try
            {
                var response = await Client.DeleteAsync(this.BaseUrl + "/api/PlaceContent/" + placeContent.Id);

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

        public Task<string> Help1ContentAsync(Help1Content help1Content)
        {
            throw new NotImplementedException();
        }

        public Task<string> Help2ContentAsync(Help2Content help2Content)
        {
            throw new NotImplementedException();
        }

        public Task<string> PlaceContentAsync(PlaceContent placeContent)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

    }
}
