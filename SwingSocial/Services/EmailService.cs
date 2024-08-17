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
using System.Web;
using Xamarin.Essentials;
using static Android.Provider.ContactsContract;

namespace SwingSocial.Sample.Services
{
    internal class EmailService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public List<ChatComment> ChatComments { get; set; }
        public List<SwingSocial.Sample.Model.Email> Emails { get; set; }

        public EmailService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        
        internal async Task<List<SwingSocial.Sample.Model.Email>> LoadSentboxEmails()
        {
            Emails = new List<SwingSocial.Sample.Model.Email>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetMailSentBox?Id=" + SwipeCardView.UsrId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<SwingSocial.Sample.Model.Email> temp =
                        JsonSerializer.Deserialize<List<SwingSocial.Sample.Model.Email>>(content, serializerOptions);
                    Emails = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Emails.AddExtraInfo();
        }
        internal async Task<List<SwingSocial.Sample.Model.Email>> LoadImboxEmails()
        {
            Emails = new List<SwingSocial.Sample.Model.Email>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/Get_MailInbox?Id=" + SwipeCardView.UsrId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<SwingSocial.Sample.Model.Email> temp =
                        JsonSerializer.Deserialize<List<SwingSocial.Sample.Model.Email>>(content, serializerOptions);
                    Emails = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Emails.AddExtraInfo();
        }

        internal async Task<string> InsertMailMessage(SwingSocial.Sample.Model.Email email)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertMailMessage", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(email), Encoding.UTF8, "application/json");

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
        
        internal async Task<InsertNewEmailResults> DeleteMailMessage(SwingSocial.Sample.Model.Email email)
        {
            InsertNewEmailResults result = new InsertNewEmailResults();

            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/MailDeleteMessage?emailId=" + email.MessageId  , string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    InsertNewEmailResults temp =
                        JsonSerializer.Deserialize<InsertNewEmailResults>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<string> InsertFriend(SwingSocial.Sample.Model.Email email)
        {
            FriendData fd = new FriendData();
            fd.LogggedUserID = email.ProfileIdFrom;
            fd.DestinationProfileID = email.ProfileTo;
            var response = String.Empty;
            var Profiles = new List<SwingSocial.Sample.Model.ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertFriend", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(fd), Encoding.UTF8, "application/json");

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

        //internal async Task<string> ImageGrantPrivateAccess(SwingSocial.Sample.Model.Email email)
        //{
        //    FriendData fd = new FriendData();
        //    fd.LogggedUserID = email.ProfileIdFrom;
        //    fd.DestinationProfileID = email.ProfileTo;
        //    var response = String.Empty;
        //    var Profiles = new List<Profile>();
        //    Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertFriend", string.Empty));
        //    HttpContent content = new StringContent(JsonSerializer.Serialize(fd), Encoding.UTF8, "application/json");

        //    try
        //    {
        //        HttpResponseMessage result = await client.PostAsync(uri, content);
        //        if (result.IsSuccessStatusCode)
        //        {
        //            response = await result.Content.ReadAsStringAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    return response;
        //}

        //internal async Task<string> ImageRevokePrivateAccess(SwingSocial.Sample.Model.Email email)
        //{
        //    FriendData fd = new FriendData();
        //    fd.LogggedUserID = email.ProfileIdFrom;
        //    fd.DestinationProfileID = email.ProfileTo;
        //    var response = String.Empty;
        //    var Profiles = new List<Profile>();
        //    Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertFriend", string.Empty));
        //    HttpContent content = new StringContent(JsonSerializer.Serialize(fd), Encoding.UTF8, "application/json");

        //    try
        //    {
        //        HttpResponseMessage result = await client.PostAsync(uri, content);
        //        if (result.IsSuccessStatusCode)
        //        {
        //            response = await result.Content.ReadAsStringAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    return response;
        //}

    }

}
