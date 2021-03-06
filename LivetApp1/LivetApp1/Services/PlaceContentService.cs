﻿using System;
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
    class PlaceContentService :IContentServise
    {
        private HttpClient Client;
        private string BaseUrl;

        public PlaceContentService()
        {
            this.Client = new HttpClient();
            this.BaseUrl = "https://localhost:5001";
        }

        #region Delete

        public async Task<string> Delete(Content placeContent)
        {
            try
            {
                var response = await Client.DeleteAsync(this.BaseUrl + "/api/PlaceContents/" + placeContent.Id);

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
            List<Content> responsePlaceContent = null;
        
            try
            {

                var response = await Client.GetAsync(this.BaseUrl + "/api/PlaceContents");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responsePlaceContent = JsonConvert.DeserializeObject<List<Content>>(responseContent);
                }

            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return responsePlaceContent;
        }


        #endregion

        #region PlacePost

        public async Task<string> Post(Content placeContent)
        {
            var jObject = JsonConvert.SerializeObject(placeContent);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/PlaceContents", content);

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


        #endregion

        #region Put

        public async Task<string> Put(Content placeContent)
        {

            var jObject = JsonConvert.SerializeObject(placeContent);
            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                var response = await Client.PutAsync(this.BaseUrl + "/api/PlaceContents/" + placeContent.Id, content);
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

        public string GetContentType()
        {
            return "場所";
        }

    }
}
