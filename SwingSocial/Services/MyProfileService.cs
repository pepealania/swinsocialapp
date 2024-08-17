using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Helper;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Models;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;

namespace SwingSocial.Sample.Services
{
    internal class MyProfileService
    {
        HttpClient client;
        public List<ProfileLocation> Locations { get; set; }
        public UpdateProfileLocationResult ResultUserLocation { get; set; }
        public UpdateProfileUserNameAgeResult ResultUserNameAge { get; set; }

        public UpdateProfileAboutResult ResultUserAbout { get; set; }
        
        public UpdateProfileTaglineResult ResultUserTagline { get; set; }

        public UpdateProfileSwingStyleResult ResultSwingStyle { get; set; }

        
        public UpdateProfileBannerResult ResultBanner { get; set; }
        public UpdateAvatarResult Result { get; set; }
        public ProfileEntity MyProfile { get; set; }

        JsonSerializerOptions serializerOptions;

        private static string BASE_URL = "http://expatcallers.com/";
        public MyProfileService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        
        internal async Task<ProfileEntity> EditProfileMyProfile()
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileMyProfile?LoginProfileId=" + SwipeCardView.UsrId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ProfileEntity temp =
                        JsonSerializer.Deserialize<ProfileEntity>(content, serializerOptions);
                    MyProfile = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return MyProfile.AddExtraInfo();
        }

        internal async Task<UpdateAvatarResult> UpdateAvatar(string avatar)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileAvatar?ProfileId=" + SwipeCardView.UsrId + "&avatar="+avatar, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateAvatarResult temp =
                        JsonSerializer.Deserialize<UpdateAvatarResult>(content, serializerOptions);
                    Result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Result;
        }

        internal async Task<UpdateProfileBannerResult> UpdateProfileBanner(string profileBanner)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileBanner?ProfileId=" + SwipeCardView.UsrId + "&profileBanner=" + profileBanner, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateProfileBannerResult temp =
                        JsonSerializer.Deserialize<UpdateProfileBannerResult>(content, serializerOptions);
                    ResultBanner = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ResultBanner;
        }

        internal async Task<UpdateProfileUserNameAgeResult> UpdateUserNameAge(ProfileEntity newProfile)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileUsernameAge?ProfileId=" + SwipeCardView.UsrId + "&userName=" + newProfile.UserName+ "&dateOfBirth="+Convert.ToDateTime(newProfile.DateOfBirth).ToString("MM/dd/yyyy"), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateProfileUserNameAgeResult temp =
                        JsonSerializer.Deserialize<UpdateProfileUserNameAgeResult>(content, serializerOptions);
                    ResultUserNameAge = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ResultUserNameAge;
        }

        internal async Task<UpdateProfileLocationResult> EditProfileLocation(ProfileEntity newProfile)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileLocation?ProfileId=" + SwipeCardView.UsrId + "&location=" + newProfile.Location, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateProfileLocationResult temp =
                        JsonSerializer.Deserialize<UpdateProfileLocationResult>(content, serializerOptions);
                    ResultUserLocation = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ResultUserLocation;
        }

        
        internal async Task<List<ProfileLocation>> EditProfileCitySearch(string cityPart)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileCitySearch?&cityPart=" + cityPart, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileLocation> temp =
                        JsonSerializer.Deserialize<List<ProfileLocation>>(content, serializerOptions);
                    Locations = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Locations;
        }
        internal async Task<List<SwingStyleTagsObject>> LoadSwingStyles()
        {
            List<SwingStyleTagsObject> _SwingerStyles = new List<SwingStyleTagsObject>() { new SwingStyleTagsObject { Description = "Exploring/Unsure" }, new SwingStyleTagsObject { Description = "Full Swap" }, new SwingStyleTagsObject { Description = "Swap" }, new SwingStyleTagsObject { Description = "Voyeur" } };

            return _SwingerStyles;
        }

        
        internal async Task<UpdateProfileSwingStyleResult> EditProfileSwingstyle(string swingStyle)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileSwingstyle?ProfileId=" + SwipeCardView.UsrId + "&swingStyles=" + HttpUtility.UrlEncode(swingStyle), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateProfileSwingStyleResult temp =
                        JsonSerializer.Deserialize<UpdateProfileSwingStyleResult>(content, serializerOptions);
                    ResultSwingStyle = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ResultSwingStyle;
        }

        internal async Task<UpdateProfileAboutResult> EditProfileAbout(ProfileEntity newProfile)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileAbout?ProfileId=" + SwipeCardView.UsrId + "&about=" + newProfile.About, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateProfileAboutResult temp =
                        JsonSerializer.Deserialize<UpdateProfileAboutResult>(content, serializerOptions);
                    ResultUserAbout = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ResultUserAbout;
        }

        internal async Task<UpdateProfileTaglineResult> EditProfileTagline(ProfileEntity newProfile)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileTagline?ProfileId=" + SwipeCardView.UsrId + "&tagline=" + newProfile.Tagline, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateProfileTaglineResult temp =
                        JsonSerializer.Deserialize<UpdateProfileTaglineResult>(content, serializerOptions);
                    ResultUserTagline = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ResultUserTagline;
        }

        internal async Task<string> EditProfileAccountType(ProfileEdit newProfile)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileAccountType", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(newProfile), Encoding.UTF8, "application/json");

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

        internal async Task<UpdateProfileAboutResult> EditProfileDetails1(ProfileEntity newProfile)
        {//profileId, string gender, string orientation, string birthday, string bodytype, string alcoholstatus, string marijuana
            IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-US");

            string DobString = newProfile.DateOfBirth.Value.ToString("MM/dd/yyyy", iFormatProvider);


            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileDetails1?profileId=" + SwipeCardView.UsrId + "&gender=" + newProfile.Gender + "&orientation=" + newProfile.SexualOrientation + "&birthday=" + DobString + "&bodytype=" + newProfile.BodyType + "&alcoholstatus=' '&marijuana=' '&hairColor="+newProfile.HairColor+"&eyeColor="+newProfile.EyeColor, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateProfileAboutResult temp =
                        JsonSerializer.Deserialize<UpdateProfileAboutResult>(content, serializerOptions);
                    ResultUserAbout = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ResultUserAbout;
        }

        internal async Task<string> EditPartnerProfileDetails2(ProfileDetails newProfile)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileDetails2", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(newProfile), Encoding.UTF8, "application/json");
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

        internal async Task<string> EditProfileAvailable(ProfileEdit newProfile)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfileAvailable", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(newProfile), Encoding.UTF8, "application/json");

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

        internal async Task<string> ImagesInsertPublicImage(Image image)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/ImagesInsertPublicImage", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(image), Encoding.UTF8, "application/json");

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

        internal async Task<string> ImagesReplacePublicImage(Image image)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/ImagesReplacePublicImage", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(image), Encoding.UTF8, "application/json");

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
        internal async Task<string> ImagesDeletePublicImage(Image image)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/ImagesDeletePublicImage", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(image), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage result = await client.PostAsync(uri,content);
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

        internal async Task<string> PrivateImagesInsertPublicImage(Image image)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/PrivateImagesInsertPublicImage", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(image), Encoding.UTF8, "application/json");

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

        internal async Task<string> PrivateImagesReplacePublicImage(Image image)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/PrivateImagesReplacePublicImage", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(image), Encoding.UTF8, "application/json");

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
        internal async Task<string> PrivateImagesDeletePublicImage(Image image)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/PrivateImagesInsertPublicImage", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(image), Encoding.UTF8, "application/json");

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
