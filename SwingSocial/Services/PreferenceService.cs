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
    internal class PreferenceService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public Preference Preference { get; set; }
        public PreferenceService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<Preference> GetPreferences() {

            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetPreferences?ProfileId=" + SwipeCardView.UsrId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                     Preference =
                        JsonSerializer.Deserialize<Preference>(content, serializerOptions);                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Preference;
        }

        internal async Task<string> InsertUpdatePreference(Preference preference)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertUpdatePreference", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(preference), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage result = await client.PostAsync(uri, content);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }

    }

}
