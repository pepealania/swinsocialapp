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
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace SwingSocial.Sample.Services
{
    class UsersMock
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public List<ProfileEntity> Profiles { get; set; }
        public List<ProfileEntity> HomeProfiles { get; set; }
        public List<ProfileEntity> ProfileId { get; set; }
        public List<Notification> Notifications { get; set; }

        public EmailRegistrationResult Result { get; set; }

        public EmailToValidateResult ResultEmailToValidate { get; set; }

        public UsersMock()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<PineApple>> LoadPineapples()
        {
            //List<Pin> Pin = new List<Pin>
            //{
            //    new Pin { Type = PinType.Place, BindingContext=new Image{ Source="pineapple.png" }, Label = "DMVExplorer", Address = "24M", Position = new Position(-12.022019380798634, -76.89215339910008) },
            //    new Pin { Type = PinType.Place, BindingContext=new Image{ Source="pineapple.png" }, Label = "Double Tada", Address = "30M|30F", Position = new Position(-12.008653316660547, -76.90224268525343) },
            //    new Pin { Type = PinType.Place, BindingContext=new Image{ Source="pineapple.png" }, Label = "Thickntatted", Address = "50M|41F", Position = new Position(-12.013522589961894, -76.90644816496346) },
            //    new Pin { Type = PinType.Place, BindingContext=new Image{ Source="pineapple.png" }, Label = "TinkBadAss", Address = "26F|28M", Position = new Position(39.0863577118741, -77.17640156160469) },
            //    new Pin { Type = PinType.Place, BindingContext=new Image{ Source="pineapple.png" }, Label = "TutLeslie", Address = "40F|43M", Position = new Position(39.0853577118741, -77.17340156160469) },
            //};

            List<PineApple> pineApples = new List<PineApple>();
                        Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetPineapplesAroundMeWithProfiles", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<PineApple> temp = JsonSerializer.Deserialize<List<PineApple>>(content, serializerOptions);
                    pineApples = temp;
                }
                foreach (var item in pineApples)
                {
                    item.Pos = new Position(Convert.ToDouble(item.Lattitude), Convert.ToDouble(item.Longitude));
                }
            }
            catch (OperationCanceledException opex)
            {
                Login.LoginPage.DisplayAlert("Network Error", "Network Error, please email to technical support.", "ok");
            }
            catch (HttpRequestException httpex)
            {
                Login.LoginPage.DisplayAlert("Network Error", "Network Error, please email to technical support.", "ok");

            }
            catch (System.Net.Sockets.SocketException socketex)
            {
                Login.LoginPage.DisplayAlert("Network Error", "Network Error, please email to technical support.", "ok");
            }
            catch (Exception ex)
            {
                Login.LoginPage.DisplayAlert("Network Error", "Network Error, please email to technical support.", "ok");

            }

            return pineApples;
        }
        
        public async Task<List<ProfileEntity>> LoadPineappleProfiles()
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetPineAppleList?Id=" + SwipeCardView.UsrId.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadPineappleProfiles(ProfileEntity p)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetPineAppleListTarget?Id=" + SwipeCardView.UsrId.ToString()+ "&targetId="+p.Id.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadSearchedUserProfiles(string searchText)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/SearchByUsername?SearchString=" +  searchText + "&loggedinProfileId=" + SwipeCardView.UsrId.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadUserProfiles()
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipeScreenHome?profileId=" + SwipeCardView.UsrId.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    HomeProfiles = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return HomeProfiles.AddExtraInfo();
            //return HomeProfiles;

        }

        public async Task<List<ProfileEntity>> LoadHomeUserProfiles(Page page)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipeScreenHome?profileId=" + SwipeCardView.UsrId.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    HomeProfiles = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return HomeProfiles.AddHomeProfileExtraInfo(page);
            //return HomeProfiles;

        }


        public async Task<List<ProfileEntity>> LoadUserProfiles(RSVP rsvp)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipescreenRsvp?LoginProfileId=" + SwipeCardView.UsrId.ToString()+ "&RsvpProfileId="+rsvp.ProfileId+ "&EventId="+EventView.EventId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();
        }


        public async Task<List<ProfileEntity>> LoadLikedUserProfiles()
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetRelationshipsJoinProfilesLiked?Id="+ SwipeCardView.UsrId.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Profiles.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadBlockedUserProfiles()
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetRelationshipsJoinProfilesBlocked?LoggedInProfileId=" + SwipeCardView.UsrId.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Profiles.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadMaybeUserProfiles()
        {
            ProfileId = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetRelationshipsJoinProfilesMaybe?Id=" + SwipeCardView.UsrId.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    ProfileId = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ProfileId.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadNopedUserProfiles()
        {
            ProfileId = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetRelationshipsJoinProfilesDenied?Id=" + SwipeCardView.UsrId.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    ProfileId = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ProfileId.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadFriendsUserProfiles()
        {
            ProfileId = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetFriendsProfiles", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    ProfileId = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ProfileId;
        }

        public async void InsertSwipeOption()
        {
            var response = String.Empty;
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"https://localhost:7238/api/User/InsertRelationship", string.Empty));
            var payload = "{\"loginProfileId\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\"targetProfileId\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\"friends\":\"No\",\"category\":\"Nope\"}";
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

        internal async void SaveMyPosition(Location location)
        {
            var response = String.Empty;
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertGeolocation", string.Empty));
            var payload = "{\"ProfileId\":\""+ SwipeCardView.UsrId.ToString()+"\",\"Lattitude\":\"" + location.Latitude.ToString()+"\",\"Longitude\":\""+location.Longitude.ToString()+ "\",\"Region\":\"1\",\"lastupdate\":\""+ DateTime.UtcNow.ToString("s") + "Z\"}";
            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage result = await client.PostAsync(uri, c);
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

        internal async Task<SelectResults> InsertProfileCheckUsername(string Username)
        {
            SelectResults SelectResults = new SelectResults();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertProfileCheckUsername?UserName=" + Username, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    SelectResults temp = JsonSerializer.Deserialize<SelectResults>(content, serializerOptions);
                    SelectResults = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return SelectResults;
        }


        internal async Task<ProfileEntity> LoadUserProfileIdFromUserName(string Username)
        {
            ProfileEntity Profile = new ProfileEntity();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EmailReturnEmail?UserName=" + Username, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ProfileEntity temp = new ProfileEntity();
                    if (content.Equals(String.Empty))
                    {

                    }
                    else
                    {
                        temp = JsonSerializer.Deserialize<ProfileEntity>(content, serializerOptions);
                    }
                    Profile = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Profile;
        }

        internal async Task<List<RSVP>> LoadRSVPs()
        {
            List<RSVP> RSVPs = new List<RSVP>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetEventRsvpByEventId?id=" + EventView.EventId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<RSVP> temp = JsonSerializer.Deserialize<List<RSVP>>(content, serializerOptions);
                    RSVPs = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return RSVPs.AddExtraInfo();
        }

        internal async Task<List<TicketPackage>> Load_TicketPackages()
        {
            List<TicketPackage> TicketPackages = new List<TicketPackage>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetEventTicketPackages?id=" + EventView.EventId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<TicketPackage> temp = JsonSerializer.Deserialize<List<TicketPackage>>(content, serializerOptions);
                    TicketPackages = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return TicketPackages.AddExtraInfo();
        }

        internal async Task<List<Attendee>> LoadAttendees()
        {
            List<Attendee> Attendees = new List<Attendee>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetEventAttendees?id="+EventView.EventId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Attendee> temp = JsonSerializer.Deserialize<List<Attendee>>(content, serializerOptions);
                    Attendees = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Attendees.AddExtraInfo();
        }

        internal async void InsertEvenRsvp()
        {
            var response = String.Empty;
            //Profiles = new List<Profile>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertEvenRsvp", string.Empty));
            var payload = "{\"eventId\":\""+EventView.EventId.ToString()+"\",\"profileId\":\"" + SwipeCardView.UsrId.ToString() + "\"}";
            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage result = await client.PostAsync(uri, c);
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
        //
        internal async Task<InsertNewProfileResult> InsertNewUserPage1(ProfileEntity p)
        {
            InsertNewProfileResult result = new InsertNewProfileResult();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertNewUserPage1?username=" + p.UserName, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    InsertNewProfileResult temp =
                        JsonSerializer.Deserialize<InsertNewProfileResult>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }
        internal async Task<InsertNewProfileResult> InsertProfilePage1(ProfileEntity p)
        {
            InsertNewProfileResult result = new InsertNewProfileResult();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertProfilePage1?q_firstname=" + p.FirstName + "&q_lastname=" + p.LastName + "&q_email=" + p.Email + "&q_password=" + p.Password + "&q_phone=" + p.Phone + "&q_username=" + p.UserName + "&q_gender=" + p.Gender + "&q_type=" + p.Type, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    InsertNewProfileResult temp =
                        JsonSerializer.Deserialize<InsertNewProfileResult>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<UpdateNewProfileResults> EditProfilePage1(ProfileEntity p)
        {
            UpdateNewProfileResults result = new UpdateNewProfileResults();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfilePage1?profileId=" + p.ProfileId + "&accountType=" + p.AccountType+ "&lookingfor=''" + p.LookingForTags[0] + "''&homecity=" + p.Location + "&tagline=" + p.Tagline + "&aboutme=" + p.About + "&swingStyles=''" + p.SwingStyleTags[0]+"''", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateNewProfileResults temp =
                        JsonSerializer.Deserialize<UpdateNewProfileResults>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<UpdateNewProfileResults> EditProfilePage2(ProfileEntity p)
        {
            UpdateNewProfileResults result = new UpdateNewProfileResults();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfilePage2?profileId=" + p.ProfileId + "&gender=" + p.Gender+ "&orientation=" + p.SexualOrientation + "&birthday=" + p.DateOfBirth.Value.ToShortDateString() + "&bodytype=" + p.BodyType, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateNewProfileResults temp =
                        JsonSerializer.Deserialize<UpdateNewProfileResults>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<UpdateNewProfileResults> EditProfilePage4(ProfileEntity p)
        {
            UpdateNewProfileResults result = new UpdateNewProfileResults();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfilePage4?profileId=" + p.ProfileId + "&avatar=" + p.Avatar + "&banner=" + p.ProfileBanner, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateNewProfileResults temp =
                        JsonSerializer.Deserialize<UpdateNewProfileResults>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }
        internal async Task<UpdateNewProfileResults> EditProfilePage5(ProfileEntity p)
        {
            UpdateNewProfileResults result = new UpdateNewProfileResults();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EditProfilePage5?profileId=" + p.ProfileId + "&desires=''" + p.Desires[0] + "''&fantasies=" + p.Fantasies, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UpdateNewProfileResults temp =
                        JsonSerializer.Deserialize<UpdateNewProfileResults>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<List<ProfileEntity>> LoadSearchedProfiles(ProfileEntity p)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/SearchProfilesAll?loginProfileId=" +SwipeCardView.UsrId+ "&qUsername=" + p.Username + "&qCoupleType=" + p.AccountTypeInteger + "&qCityState=" + p.Location + "&qOnlyWithPhotos=" + p.OnlyWithPhotos + "&qHisAgeMin=" + p.HisAgeFrom + "&qHisAgeMax=" + p.HisAgeTo + "&qHerAgeMin=" + p.HerAgeFrom + "&qHerAgeMax=" + p.HerAgeTo + "&herOrientation=" + p.HerOrientation + "&hisOrientation=" + p.HisOrientation, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Profiles.AddExtraInfo();
        }

        internal async Task<List<ProfileEntity>> GetOnlineList()
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetOnlineList?profileId=" + SwipeCardView.UsrId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Profiles.AddExtraInfo();
        }

        internal async Task<List<ProfileEntity>> LoadSearchProfiles(ProfileEntity p)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipescreenWithFilters?LoginProfileId=" + SwipeCardView.UsrId.ToString()+ "&selectedAccountType="+p.AccountTypeInteger, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();

        }
        public async Task<List<ProfileEntity>> LoadOnlineProfiles(ProfileEntity p)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipeScreenLastOnline?LoginProfileId=" + SwipeCardView.UsrId.ToString()+ "&TopId="+p.Id.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadLikedProfiles(ProfileEntity p)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipescreenWithCategory?LoginProfileId=" + SwipeCardView.UsrId.ToString()+ "&Category=Liked&TappedId="+p.Id.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadDeniedProfiles(ProfileEntity p)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipescreenWithCategory?LoginProfileId=" + SwipeCardView.UsrId.ToString() + "&Category=Denied&TappedId=" + p.Id.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();
        }

        public async Task<List<ProfileEntity>> LoadMaybeProfiles(ProfileEntity p)
        {
            Profiles = new List<ProfileEntity>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetSwipescreenWithCategory?LoginProfileId=" + SwipeCardView.UsrId.ToString() + "&Category=Maybe&TappedId=" + p.Id.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ProfileEntity> temp = JsonSerializer.Deserialize<List<ProfileEntity>>(content, serializerOptions);
                    Profiles = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Profiles.AddExtraInfo();
        }

        internal async Task<List<Notification>> ReadNotification(string notificationType)
        {
            Notifications = new List<Notification>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/ReadNotification?profileId=" + SwipeCardView.UsrId.ToString()+ "&notificationType="+ notificationType, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Notification> temp = JsonSerializer.Deserialize<List<Notification>>(content, serializerOptions);
                    Notifications = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Notifications;
        }

        internal async Task<EmailRegistrationResult> SendEmail(string email)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EmailInsertEmailCode?email=" + email, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EmailRegistrationResult temp =
                        JsonSerializer.Deserialize<EmailRegistrationResult>(content, serializerOptions);
                    Result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Result;
        }

        internal async Task<EmailToValidateResult> EmailReturnEmailcode(string email)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EmailReturnEmailcode?email=" + HttpUtility.UrlEncode(email), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EmailToValidateResult temp =
                        JsonSerializer.Deserialize<EmailToValidateResult>(content, serializerOptions);
                    ResultEmailToValidate = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ResultEmailToValidate;
        }
        /*        public string IdPlainString { get; internal set; }
        public int AccountTypeNumber { get; internal set; }
        public int HisAgeTo { get; internal set; }
        public int HisAgeFrom { get; internal set; }
        public int HerAgeTo { get; internal set; }
        public int HerAgeFrom { get; internal set; }
        public int OnlyWithPhotos { get; internal set; }
        public string HisOrientation { get; internal set; }
        public string HerOrientation { get; internal set; }
        public string InterestedIn { get; internal set; }
        public string SwingStyleString { get; internal set; }*/


        internal async Task<InsertNewProfileResult> InsertRegistrationProfile(ProfileEntity newProfile)
        {
            InsertNewProfileResult result = new InsertNewProfileResult();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertRegistrationProfile?q_email=" + newProfile.Email+ "&username="+newProfile.UserName + "&iam=" + newProfile.AccountType + "&lookingFor=" + newProfile.InterestedIn + "&qswingStyles=" + newProfile.SwingStyleString + "&qavatar=" + HttpUtility.HtmlEncode(newProfile.Avatar) + "&banner=" + HttpUtility.UrlEncode(newProfile.ProfileBanner) + "&tagline=" + newProfile.Tagline + "&aboutUs=" + newProfile.About + "&dateOfBirth="+ Convert.ToDateTime(newProfile.DateOfBirth).ToString("MM/dd/yyyy") + "&partnerDateOfBirth=01/01/1001", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    InsertNewProfileResult temp =
                        JsonSerializer.Deserialize<InsertNewProfileResult>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<string> ImageGrantPrivateAccess(ProfilePrivate profile)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/ImageGrantPrivateAccess", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(profile), Encoding.UTF8, "application/json");

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
        internal async Task<string> ImageRevokePrivateAccess(ProfilePrivate profile)
        {
            var response = String.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/ImageRevokePrivateAccess", string.Empty));
            HttpContent content = new StringContent(JsonSerializer.Serialize(profile), Encoding.UTF8, "application/json");

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

        #region registration
        internal async Task<EmailRegistrationResult> SendWelcomeEmail(string email)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/SendWelcomeEmail?email=" + email, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EmailRegistrationResult temp =
                        JsonSerializer.Deserialize<EmailRegistrationResult>(content, serializerOptions);
                    Result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Result;
        }

        #endregion

    }
}
