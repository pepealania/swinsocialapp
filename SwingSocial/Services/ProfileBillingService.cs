using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Helper;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SwingSocial.Sample.Services
{
    internal class ProfileBillingService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public ProfileBilling ProfileBillingInfo { get; set; }


        public ProfileBillingService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<ProfileBilling> GetProfileBilling() {
            ProfileBillingInfo = new ProfileBilling();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetProfileBilling?profileId=" + SwipeCardView.UsrId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var temp =
                        JsonSerializer.Deserialize<ProfileBilling>(content, serializerOptions);
                    ProfileBillingInfo = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ProfileBillingInfo;
        }

    }

}
