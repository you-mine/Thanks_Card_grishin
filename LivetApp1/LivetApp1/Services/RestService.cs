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
    class RestService : IRestService
    {
        private HttpClient Client;
        private string BaseUrl;

        public RestService()
        {
            this.Client = new HttpClient();
            this.BaseUrl = "https://localhost:5001";
        }

        #region ログイン処理
        public async Task<User> LogonAsync(User user)
        {
            var jObject = JsonConvert.SerializeObject(user);

            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            User responseUser = null;
            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/Logon", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseUser = JsonConvert.DeserializeObject<User>(responseContent);
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return responseUser;
        }
        #endregion

        #region カード作成
        public async Task<string> CreateCardAsync(ThanksCard thanksCard)
        {
            var jObject = JsonConvert.SerializeObject(thanksCard);

            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/ThanksCard", content);

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

        #region カード一覧取得
        public async Task<List<ThanksCard>> GetCardsAsync()
        {

            List<ThanksCard> responseCards = null;
            try
            {
                var response = await Client.GetAsync(this.BaseUrl + "/api/ThanksCard");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseCards = JsonConvert.DeserializeObject<List<ThanksCard>>(responseContent);
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }

            return responseCards;
        }
        #endregion

        #region カード更新
        public async Task<ThanksCard> PutCard(ThanksCard thanksCard)
        {
            User To = thanksCard.To;
            User From = thanksCard.From;
            thanksCard.ToId = thanksCard.To.Id;
            thanksCard.FromId = thanksCard.From.Id;
            thanksCard.To = null;
            thanksCard.From = null;

            var jObject = JsonConvert.SerializeObject(thanksCard);

            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            ThanksCard responseThanksCard = null;
            try
            {
                var response = await Client.PutAsync(this.BaseUrl + "/api/ThanksCard/" + thanksCard.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseThanksCard = JsonConvert.DeserializeObject<ThanksCard>(responseContent);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.PutUserAsync: " + e);
            }
            thanksCard.To = To;
            thanksCard.From = From;
            return responseThanksCard;
        }
        #endregion

        #region 部署一覧取得
        public async Task<List<Department>> GetDepartmentsAsync()
        {
            List<Department> responseDepartments = null;
            try
            {
                var response = await Client.GetAsync(this.BaseUrl + "/api/Departments");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseDepartments = JsonConvert.DeserializeObject<List<Department>>(responseContent);
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return responseDepartments;
        }
        #endregion

        #region 部署削除
        public async Task<string> DeleteDepartment(Department department)
        {
            try
            {
                var response = await Client.DeleteAsync(this.BaseUrl + "/api/Departments/" + department.Id);

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

        #region 報告書用代表事例取得
        public async Task<List<ThanksCard>> GetCardsForReport(Term term)
        {
            var jObject = JsonConvert.SerializeObject(term);

            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            List<ThanksCard> responseCards = null;
            try
            {
                var response = await Client.PostAsync(this.BaseUrl + "/api/Report" , content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseCards = JsonConvert.DeserializeObject<List<ThanksCard>>(responseContent);
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return responseCards;
        }

        #endregion

        #region ユーザー削除
        public async Task<string> DeleteUserAsync(User user)
        {
            try
            {
                var response = await Client.DeleteAsync(this.BaseUrl + "/api/Users/" + user.Id);

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

        #region ユーザー編集
        public async Task<String> PutUserAsync(User user)
        {
            var jObject = JsonConvert.SerializeObject(user);

            //Make Json object into content type
            var content = new StringContent(jObject);
            //Adding header of the contenttype
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await Client.PutAsync(this.BaseUrl + "/api/Users/" + user.Id, content);

                if (response.IsSuccessStatusCode)
                {
                    return "Success";
                }
            }
            catch (Exception e)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }
            return "failed";
        }
        #endregion

        #region ランキング取得
        public async Task<List<Ranking>> GetRankings()
        {
            List<Ranking> responseRankings = null;
            try { 
                    var response = await Client.GetAsync(this.BaseUrl + "/api/Ranking");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        responseRankings = JsonConvert.DeserializeObject<List<Ranking>>(responseContent);
                            //降順並んでいるだけでランク付けされていないため、ここでランク付けする。
                            int Index = 1;
                            foreach (Ranking a in responseRankings)
                            {
                                a.Rank = Index++; 
                            }
                    }
                }

            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RestService.LogonAsync: " + e);
            }

            return responseRankings;
        }
        #endregion



    }
}
