using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Helper;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SwingSocial.Sample.Services
{
    internal class SearchsService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public ProfileEntity Profile { get; set; }
        public SearchsService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<ProfileEntity> GetSwipescreenOneprofile(string profileId) {
            //Profile = new Profile();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipescreenOneprofile?LoginProfileId=" + SwipeCardView.UsrId+ "&TargetProfileId="+profileId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                     Profile =
                        JsonSerializer.Deserialize<ProfileEntity>(content, serializerOptions);                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Profile.AddExtraInfo();
        }

    }

}
