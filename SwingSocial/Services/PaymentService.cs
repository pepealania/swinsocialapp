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
    internal class PaymentService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public List<ChatComment> ChatComments { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Emoji> Emojis { get; set; }
        public Chat Chat { get; set; }


        public PaymentService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<string> AuthorizeNetPay(PaymentData paymentData)
        {
            var response = String.Empty;
            var Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:6001/api/Payment/AuthorizeNetPayment", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(paymentData), Encoding.UTF8, "application/json");
            
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
