using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SwingSocial.Sample.Services
{
    class UsersMock
    {
        HttpClient client;
        private static string BASE_URL = "http://expatcallers.com/";
        public static Guid userId;
        public UsersMock()
        {
            client = new HttpClient();
        }
        public async void InsertSwipeOption(SwipeCardDirection direction,string currentStringId)
        {
            string swipeOption = string.Empty;
            switch (direction)
            {
                case SwipeCardDirection.Right:
                    swipeOption = "Liked";
                    break;
                case SwipeCardDirection.Left:
                    swipeOption = "Denied";
                    break;
                case SwipeCardDirection.Down:
                    swipeOption = "Maybe";
                    break;
                default:
                    break;
            }
            // clarks 3c1a3569 - c9b6 - 46b2 - af76 - c0a9752105a7
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/UpdateRelationship", string.Empty));
            var payload = "{\"loginProfileId\":\""+ SwipeCardView.UsrId.ToString()+"\",\"targetProfileId\":\"" + currentStringId + "\",\"friends\":\"No\",\"category\":\""+swipeOption+"\"}";
            HttpContent c = new StringContent(payload,Encoding.UTF8,"application/json");
            try
            {
                HttpResponseMessage result = await client.PostAsync(uri,c);
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
