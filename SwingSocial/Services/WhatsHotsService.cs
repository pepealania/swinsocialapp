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
    internal class WhatsHotsService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public List<WhatsHot> WhatsHots { get; set; }
        public WhatsHotsService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<WhatsHot>> LoadWhatsHots() {
            WhatsHots = new List<WhatsHot>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetAllpostsByProfile?id="+ SwipeCardView.UsrId,string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<WhatsHot> temp =
                        JsonSerializer.Deserialize<List<WhatsHot>>(content, serializerOptions);
                    WhatsHots = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return WhatsHots.AddExtraInfo();
        }

        public async Task<Response> UpdateLikeInWhatsHotItem(WhatsHot selectedItem)
        {
            Response temp = new Response();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertLikesPost?PostId="+selectedItem.Id+ "&profileId=" + selectedItem.ProfileId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    temp = JsonSerializer.Deserialize<Response>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return temp;
        }

        internal async Task<string> GetLikesFromApi(WhatsHot selectedItem)
        {
            List<LikePost> likePosts = new List<LikePost>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetLikesWhatsHotPost?Id="+selectedItem.Id, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<LikePost> temp =
                        JsonSerializer.Deserialize<List<LikePost>>(content, serializerOptions);
                    likePosts = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return likePosts.Count==0?"0":likePosts[0].NumLikes.ToString();
        }

        internal async Task<string> GetCommentsFromApi(WhatsHot selectedItem)
        {
            CommentPost commentPost = new CommentPost();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetCommentsWhatsHotPostTotal?Id=" + selectedItem.Id, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    CommentPost temp =
                        JsonSerializer.Deserialize<CommentPost>(content, serializerOptions);
                    commentPost = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return commentPost == null ? "0" : commentPost.NumComments.ToString();
        }

        internal async Task<List<PostComment>> LoadPostComments()
        {
            List<PostComment> postComments = new List<PostComment>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetCommentsWhatsHotPost?Id="+CommentsPage.PostId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<PostComment> temp =
                        JsonSerializer.Deserialize<List<PostComment>>(content, serializerOptions);
                    postComments = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return postComments.AddExtraInfo();

        }

        internal async Task<bool> InsertComment(string commentText)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertCommentsPost?PostId=" + CommentsPage.PostId+"&profileId="+SwipeCardView.UsrId+"&comment="+commentText, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Response temp =
                        JsonSerializer.Deserialize<Response>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return true;
        }

        internal async void InsertPost(string captionText, string imageUrl)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertPostTableImage?ProfileId="+SwipeCardView.UsrId+"&PostUrl="+imageUrl+"&Caption="+captionText, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Response temp =
                        JsonSerializer.Deserialize<Response>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        internal async void InsertVideoPost(string captionText, string imageUrl)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertPostTableVideo?ProfileId=" + SwipeCardView.UsrId + "&PostUrl=" + imageUrl + "&Caption=" + captionText, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Response temp =
                        JsonSerializer.Deserialize<Response>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        internal async Task<string> DeletePost(WhatsHot wh)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/DeletePost/{wh.Id}", string.Empty));

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //HttpContent content = new StringContent(JsonSerializer.Serialize(profile), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage result = await client.SendAsync(request);
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

        internal async Task<bool> UploadPostCroppedImage(WhatsHotStream whatsHotStream)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/UploadPostCroppedImage", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(whatsHotStream), Encoding.UTF8, "application/json");

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
            return Convert.ToBoolean(response);
        }
    }
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

}
