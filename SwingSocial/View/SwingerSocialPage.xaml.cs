using Android.Views;
using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using Plugin.LocalNotifications;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;


namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwingerSocialPage : ContentPage
    {
        public static Location location;
        string currentPineapple= "pineapple.gif";
        public static ProfileEntity currentSwipeProfile;
        public static string emojiButtonValue=null;
        public static string notificationPage = null;
        public bool isChat=true;
        public bool isEmail =false;
        public bool isMatch = false;
        public static bool FromSwingerSocialPage;


        private static Frame mySwipeSectionFrame;
        private static StackLayout myProfileSectionFrame;


        private SwingerSocialPageViewModel SwingerSocialPageViewModel;
        private bool PermissionToLeave;
        public SwingerSocialPage()
        {
            try
            {
                InitializeComponent();
                SwingerSocialPageViewModel = new SwingerSocialPageViewModel(Navigation,this);
                BindingContext = SwingerSocialPageViewModel;

                pineapple.Source = currentPineapple;
                var pineappleTimer = new System.Timers.Timer(300000);
                pineappleTimer.Elapsed += new ElapsedEventHandler(SwitchPineapples);
                pineappleTimer.Enabled = true;
                MySwipeCardView.Dragging += OnDragging;
                //SetupLocation();
                var pineappleSaveTimer = new System.Timers.Timer(300000);
                pineappleSaveTimer.Elapsed += new ElapsedEventHandler(SaveMyPosition);
                pineappleSaveTimer.Enabled = true;
                if (Login.CommingFromLogin)
                {
                    modalStackLayout.IsVisible = true;
                    SwipeMessageStartup.IsVisible = true;
                    Login.CommingFromLogin = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void ansPicker_CheckedChanged(object sender, int e)
        {
            var view = (Xamarin.Forms.View)sender;

            Frame EventsAllFrame = view.FindByName<Frame>("EventsAllFrame");
            Frame EventsRsvpFrame = view.FindByName<Frame>("EventsRsvpFrame");

            var radio = sender as CustomRadioButton;

            if (radio == null || radio.Id == -1)
            {
                return;
            }
            if (radio.Text == "Rsvp")
            {
                EventsAllFrame.IsVisible = false;
                EventsRsvpFrame.IsVisible = true;
                ////listOfFeaturesPerAccountType.Source = "premium.jpg";
                //premiumOptions.IsVisible = true;
            }
            else
            {
                EventsAllFrame.IsVisible = true;
                EventsRsvpFrame.IsVisible = false;
                ////listOfFeaturesPerAccountType.Source = "free.jpg";
                //premiumOptions.IsVisible = false;
            }

            //DisplayAlert("Info", radio.Text, "OK");
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            BRegistrationPage1.TransactionAlive = false;
            if (PreferencesPage.commingFromPreferences)
            {
                PreferencesPage.commingFromPreferences = false;
                var nextPage = new MyProfilePage();
                await Navigation.PushAsync(nextPage);
            }
        }
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                if (!PermissionToLeave)
                {
                    //if (myProfileSectionFrame.IsVisible)
                    //{
                    //    mySwipeSectionFrame.IsVisible=true;
                    //    myProfileSectionFrame.IsVisible=false;
                    //}
                    //var nextPage = new SwingSocialGateway();
                    //await Navigation.PushAsync(nextPage);
                    SwingerSocialPage.FromSwingerSocialPage = true;

                }
                else
                {
                    SwingerSocialPage.FromSwingerSocialPage = true;
                    PermissionToLeave = false;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async void SetupLocation()
        {

            try
            {
                SwingerSocialPage.location = await Geolocation.GetLastKnownLocationAsync();

            }
            catch (Exception ex)
            {

                throw;
            }            //MapPage.myLocation = new Position(SwingerSocialPage.location.Latitude, SwingerSocialPage.location.Longitude);
        }

        private async void GetCurrentLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
            }
            catch (Exception wx)
            {

                throw;
            }

        }

        private void SwitchPineapples(object source,ElapsedEventArgs e)
        {
            //read the service GetProfilesAroundMe
            //if it has results > 0 then turn on pineapple

            //if (currentPineapple=="pineapple.gif")
            //{
            //    pineapple.Source = "FlashingPineapple.gif";
            //    currentPineapple = "FlashingPineapple.gif";
            //}
            //else
            //{
            //    pineapple.Source = "pineapple.gif";
            //    currentPineapple = "pineapple.gif";
            //}
        }

        private async void ReadNotificationChat(object source, ElapsedEventArgs e)
        {
            UsersMock usersMock = new UsersMock();
            List<Notification> notifications = new List<Notification>();
            if (isChat)
            {
                notifications = await usersMock.ReadNotification("chat");
                isChat = false;
                isEmail = true;
                isMatch = false;
            }
            else if(isEmail)
            {
                notifications = await usersMock.ReadNotification("email");
                isChat = false;
                isEmail = false;
                isMatch = true;
            }
            else
            {
                notifications = await usersMock.ReadNotification("match");
                isChat = true;
                isEmail = false;
                isMatch = false;
            }
            string notificationType = String.Empty;
            //            
            foreach (var item in notifications)
            {
                
                CrossLocalNotifications.Current.Show("SwingSocial", item.Message, 0, DateTime.Now.AddSeconds(1));
                
                notificationType = item.NotificationType;
            }
            if (notificationType=="chat")
            {
                
                notificationPage = "chat";
            }
            else
            {
                notificationPage = "email";
            }
        }

        private async void SaveMyPosition(object source, ElapsedEventArgs e)
        {
            UsersMock usersMock = new UsersMock();

            if (SwingerSocialPage.location!=null)
            {
                usersMock.SaveMyPosition(SwingerSocialPage.location); 
            }
            else
            {
                SwingerSocialPage.location = await Geolocation.GetLastKnownLocationAsync();
            }
        }

        private async void OnChatClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var currentSwipeProfile = view.FindByName<SwipeCardView>("MySwipeCardView");
            ProfileEntity profile = currentSwipeProfile.TopItem as ProfileEntity;
            PermissionToLeave = true;
            //string myname = currentSwipeProfile.Text;
            //var navigationParams = new Dictionary<string, object>
            //{
            //};
            //await Shell.Current.GoToAsync("ChatPage", true);
            //Event currentEvent = e.Item as Event;
            //string result = currentEvent.Name;
            var secondPage = new MessagesMenuTopPage();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

        private void OnSuperLikeClicked(object sender, EventArgs e)
        {
            MySwipeCardView.InvokeSwipe(SwipeCardDirection.Down);
        }

        private void OnLikeClicked(object sender, EventArgs e)
        {
            MySwipeCardView.InvokeSwipe(SwipeCardDirection.Right);
        }

        private void OnDragging(object sender, DraggingCardEventArgs e)
        {
            
            var view = (Xamarin.Forms.View)sender;
            var nopeFrame = view.FindByName<Frame>("NopeFrame");
            var likeFrame = view.FindByName<Frame>("LikeFrame");
            var superLikeFrame = view.FindByName<Frame>("SuperLikeFrame");
            //var matchesConfirmation = view.FindByName<StackLayout>("MatchesConfirmation");
            //var matchedProfile = view.FindByName<Span>("matchedProfile");
            var IdString = view.FindByName<Span>("IdString");
            MLToolkit.Forms.SwipeCardView.SwipeCardView.CurrentIdString = IdString.Text;
            var threshold = (BindingContext as SwingerSocialPageViewModel).Threshold;
            ProfileEntity p = e.Item as ProfileEntity;
            int TotalProfiles = SwingerSocialPageViewModel.Profiles.Count;
            int ProfileIndex = SwingerSocialPageViewModel.Profiles.IndexOf(p);
            
            var draggedXPercent = e.DistanceDraggedX / threshold;

            if (e.Direction==SwipeCardDirection.Down)
            {

            }

            var draggedYPercent = e.DistanceDraggedY / threshold;

            switch (e.Position)
            {
                case DraggingCardPosition.Start:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    superLikeFrame.Opacity = 0;
                    //nopeButton.Scale = 0.2;
                    //likeButton.Scale = 0.2;
                    //superLikeButton.Scale = 1;
                    break;

                case DraggingCardPosition.UnderThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        nopeFrame.Opacity = (-1) * draggedXPercent;
                        //nopeButton.Scale = 1 + draggedXPercent / 2;
                        superLikeFrame.Opacity = 0;
                        //superLikeButton.Scale = 0.2;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        likeFrame.Opacity = draggedXPercent;
                        //likeButton.Scale = 1 - draggedXPercent / 2;
                        superLikeFrame.Opacity = 0;
                        //superLikeButton.Scale = 0.2;
                    }
                    else if (e.Direction == SwipeCardDirection.Down)
                    {
                        nopeFrame.Opacity = 0;
                        likeFrame.Opacity = 0;
                        //nopeButton.Scale = 0.2;
                        //likeButton.Scale = 0.2;
                        superLikeFrame.Opacity = (-1) * draggedYPercent;
                        //superLikeButton.Scale = 1 + draggedYPercent / 2;
                    }
                    break;

                case DraggingCardPosition.OverThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        nopeFrame.Opacity = 1;
                        superLikeFrame.Opacity = 0;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        likeFrame.Opacity = 1;
                        superLikeFrame.Opacity = 0;
                    }
                    else if (e.Direction == SwipeCardDirection.Down)
                    {
                        nopeFrame.Opacity = 0;
                        likeFrame.Opacity = 0;
                        superLikeFrame.Opacity = 1;
                    }
                    break;

                case DraggingCardPosition.FinishedUnderThreshold:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    superLikeFrame.Opacity = 0;
                    //nopeButton.Scale = 0.2;
                    //likeButton.Scale = 0.2;
                    //superLikeButton.Scale = 0.2;
                    break;

                case DraggingCardPosition.FinishedOverThreshold:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    superLikeFrame.Opacity = 0;
                    if (p.TargetLikes==1)
                    {
                        modalStackLayout.IsVisible = true;
                        matchedProfile.Text = p.UserName;
                        matchedMessageAvatar.Source = p.Avatar;
                        MySwipeCardView.Opacity = 0.3;
                        MatchesConfirmation.IsVisible = true;
                    }
                    if (ProfileIndex == TotalProfiles-1)
                    {
                        //LastProfilePopUp.IsVisible = true;
                        //LastProfileMessage.Text = p.Tagline;
                        //MySwipeCardView.Opacity = 0.3;
                        //modalStackLayout.IsVisible = true;
                        SwingerSocialPageViewModel = new SwingerSocialPageViewModel(Navigation,this);
                        BindingContext = SwingerSocialPageViewModel;

                    }
                    //nopeButton.Scale = 0.2;
                    //likeButton.Scale = 0.2;
                    //superLikeButton.Scale = 0.2;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        //HideSwipeImage
        private async void OpenProfileInfo(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            
            //var topStackLayout = view.FindByName<StackLayout>("TopStackLayout");
            //MySwipeCardView.SupportedSwipeDirections = SwipeCardDirection.None;
            //MySwipeCardView.SupportedDraggingDirections = SwipeCardDirection.None;

            var swipeSectionFrame = view.FindByName<Frame>("SwipeSection");
            var profileSectionFrame = view.FindByName<StackLayout>("ProfileSection");

            mySwipeSectionFrame = swipeSectionFrame;
            myProfileSectionFrame = profileSectionFrame;


            var ProfileLocation = view.FindByName<Button>("ProfileLocation");
            BindableRadioGroup ansPicker = view.FindByName<BindableRadioGroup>("ansPicker");
            ansPicker.ItemsSource = new[]
            {
                "Rsvp",
                "All Events"
            };
            ansPicker.SelectedIndex = 1;
            ansPicker.CheckedChanged += ansPicker_CheckedChanged;
            //premiumOptions.IsVisible = false;
            ListView EventsListView = view.FindByName<ListView>("EventsListView");
            swipeSectionFrame.IsVisible = false;
            profileSectionFrame.IsVisible = true;
            EventsListView.ItemTapped += EventsListView_ItemTappedAsync;
            ListView EventsAllListView = view.FindByName<ListView>("EventsAllListView");
            EventsAllListView.ItemsSource = SwingerSocialPageViewModel.EventsAll;
            EventsAllListView.ItemTapped += EventsListView_ItemTappedAsync;

        }

        private async void EventsListView_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            Event currentEvent = e.Item as Event;
            string result = currentEvent.Name;
            var secondPage = new EventView(currentEvent);
            await Navigation.PushAsync(secondPage);
        }

        private async void ShowSwipeImage(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var swipeSectionFrame = view.FindByName<Frame>("SwipeSection");
            var profileSectionFrame = view.FindByName<StackLayout>("ProfileSection");

            swipeSectionFrame.IsVisible = true;
            profileSectionFrame.IsVisible = false;
        }

        private void HideShowPartnerTable(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var partnerTableLabel = view.FindByName<Label>("PartnerTableLabel");
            var partnerTable = view.FindByName<WebView>("PartnerTable");
            var partnerTableHideShow = view.FindByName<Xamarin.Forms.ImageButton>("PartnerTableHideShow");

            if (partnerTable.IsVisible)
            {
                partnerTableHideShow.Source = "circleplus";
                partnerTableHideShow.HeightRequest = 50;
                partnerTable.IsVisible = false;
            }
            else
            {
                partnerTableHideShow.Source = "circleminus";
                partnerTable.IsVisible = true;
                partnerTableHideShow.HeightRequest = 50;

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }
        
        private async void HeartChat_Tapped(object sender, EventArgs e)
        {
            Label b = (Label)sender;
            emojiButtonValue = b.Text;
            ProfileEntity p = ((TappedEventArgs)e).Parameter as ProfileEntity;
            ChatService cs = new ChatService();
            var chat =await cs.ChatCreateUserChat(p.Id);
            PermissionToLeave = true;
            var nextPage = new ChatPage(chat);
            await Navigation.PushAsync(nextPage);
        }
        
        private async void WineGlass_Tapped(object sender, EventArgs e)
        {
            Label b = (Label)sender;
            emojiButtonValue = b.Text;
            ProfileEntity p = ((TappedEventArgs)e).Parameter as ProfileEntity;
            ChatService cs = new ChatService();
            var chat = await cs.ChatCreateUserChat(p.Id);
            PermissionToLeave = true;
            var nextPage = new ChatPage(chat);
            await Navigation.PushAsync(nextPage);
        }
        
        private async void Bed_Tapped(object sender, EventArgs e)
        {
            Label b = (Label)sender;
            emojiButtonValue = b.Text;
            ProfileEntity p = ((TappedEventArgs)e).Parameter as ProfileEntity;
            ChatService cs = new ChatService();
            var chat = await cs.ChatCreateUserChat(p.Id);
            PermissionToLeave = true;
            var nextPage = new ChatPage(chat);
            await Navigation.PushAsync(nextPage);
        }

        private async void ButtonStartChat_Clicked(object sender, EventArgs e)
        {

            Chat c = new Chat();
            Button b = sender as Button;

            ProfileEntity p = b.CommandParameter as ProfileEntity;
            ChatService cs = new ChatService();
            c = await cs.ChatCreateUserChat(p.Id);
            PermissionToLeave = true;
            var nextPage = new ChatPage(c);
            await Navigation.PushAsync(nextPage);
        }

        private void MatchesConfirmationClose_Clicked(object sender, EventArgs e)
        {
            MatchesConfirmation.IsVisible = false;
            MySwipeCardView.Opacity = 1;
            MatchesConfirmation.IsVisible = false;
            modalStackLayout.IsVisible = false;

        }
        //LastProfilePopUp_Clicked
        private void LastProfilePopUpClose_Clicked(object sender, EventArgs e)
        {
            LastProfilePopUp.IsVisible = false;
            MySwipeCardView.Opacity = 1;
            MatchesConfirmation.IsVisible = false;
            modalStackLayout.IsVisible = false;

        }
        private async void LastProfileGoToBilling_Clicked(object sender, EventArgs e)
        {
            var nextPage = new BRegistrationPage8();
            await Navigation.PushAsync(nextPage);
        }

        private void matchedMessageAvatar_Clicked(object sender, EventArgs e)
        {

            try
            {
                string profileToSearch = matchedProfile.Text;
                SwingerSocialPageViewModel.Profiles = new ObservableCollection<ProfileEntity>(SwingerSocialPageViewModel.Profiles.OrderByDescending(x => x.UserName == profileToSearch).ThenBy(x => x.UserName));

                modalStackLayout.IsVisible = false;
                MySwipeCardView.Opacity = 1;
            }
            catch (Exception ex)
            {

                throw;
            }        
        }
        private void HideShowEvents(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var eventsLabel = view.FindByName<Label>("EventsLabel");
            var eventsRsvpFrame = view.FindByName<Frame>("EventsRsvpFrame");
            var eventsAllFrame = view.FindByName<Frame>("EventsAllFrame");            
            var eventsHideShow = view.FindByName<Xamarin.Forms.ImageButton>("EventsHideShow");

            var ansPicker = view.FindByName<BindableRadioGroup>("ansPicker");

            CustomRadioButton radio = ansPicker.Items[ansPicker.SelectedIndex];



            if (eventsRsvpFrame.IsVisible || eventsAllFrame.IsVisible)
            {
                eventsHideShow.Source = "circleplus";
                eventsHideShow.HeightRequest = 50;
                eventsRsvpFrame.IsVisible = false;
                eventsAllFrame.IsVisible = false;
            }
            else
            {
                eventsHideShow.Source = "circleminus";
                eventsHideShow.HeightRequest = 50;
                if (radio == null || radio.Id == -1)
                {
                    return;
                }
                if (radio.Text == "Rsvp")
                {
                    eventsAllFrame.IsVisible = false;
                    eventsRsvpFrame.IsVisible = true;
                    ////listOfFeaturesPerAccountType.Source = "premium.jpg";
                    //premiumOptions.IsVisible = true;
                }
                else
                {
                    eventsAllFrame.IsVisible = true;
                    eventsRsvpFrame.IsVisible = false;
                    ////listOfFeaturesPerAccountType.Source = "free.jpg";
                    //premiumOptions.IsVisible = false;
                }

            }
        }

        private void HomeMenu_Clicked(object sender, EventArgs e)
        {

        }

        private async void CommunityMenu_Clicked(object sender, EventArgs e)
        {
            PermissionToLeave = true;
            var nextPage = new Community();
            await Navigation.PushAsync(nextPage);
        }

        private async void Pineapple_Clicked(object sender, EventArgs e)
        {
            PermissionToLeave = true;
            var nextPage = new MapPage();
            await Navigation.PushAsync(nextPage);
        }

        private async void MatchesMenu_Clicked(object sender, EventArgs e)
        {
            PermissionToLeave = true;
            var nextPage = new Matches();
            await Navigation.PushAsync(nextPage);   
        }

        private async void MyProfile_Clicked(object sender, EventArgs e)
        {
            PermissionToLeave = true;
            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void GoToEmailPage_Clicked(object sender, EventArgs e)
        {
            PermissionToLeave = true;
            //var nextPage = new MailPage();
            //await Navigation.PushAsync(nextPage);

            SwingSocial.Sample.Model.Email email = new SwingSocial.Sample.Model.Email();
            Button b = sender as Button;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            //Profile p = e.Item as Profile;
            email.Avatar = p.Avatar;
            email.ProfileIdFrom = SwipeCardView.UsrId.ToString();
            email.ProfileFromUsername = SwipeCardView.UsrName;
            email.ProfileTo = p.Id.ToString();
            email.ProfileToUsername = p.UserName;
            var nextPage = new EmailCreatePage(email);
            await Navigation.PushAsync(nextPage);



        }

        private async void SendFriendlyMail_Clicked(object sender, EventArgs e)
        {
            SwingSocial.Sample.Model.Email em = new SwingSocial.Sample.Model.Email();
            em.ProfileIdFrom = SwipeCardView.UsrId.ToString();
            Button b = sender as Button;
            ProfileEntity p = b.CommandParameter as ProfileEntity;
            em.ProfileTo = p.Id.ToString();
            em.Subject = "Friend Request";
            em.Body = "Hello, can we be friends??";
            EmailService es = new EmailService();
            var result = await es.InsertMailMessage(em);
            DisplayAlert("Friend Request Status","Friend Request successfully sent","ok");
        }

        private void PublicImage_Clicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            var PublicPhotosSection = view.FindByName<StackLayout>("PublicPhotosSection");
            var PrivatePhotosSection = view.FindByName<StackLayout>("PrivatePhotosSection");
            PublicPhotosSection.IsVisible = true;
            PrivatePhotosSection.IsVisible = false;

        }

        private async void PrivateImage_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            ProfileEntity p = b.CommandParameter as ProfileEntity;
            int privateImage = p.PrivateImage;
            string userName = p.UserName;
            if (privateImage==0)
            {
                DisplayAlert("Permission Status","Click ok to send a request to " + userName + " for access to their private images.", "ok");
                SwingSocial.Sample.Model.Email em = new SwingSocial.Sample.Model.Email();
                em.ProfileIdFrom = SwipeCardView.UsrId.ToString();
                em.ProfileTo = p.Id.ToString();
                em.Subject = "Access to Private Images Requested";
                em.Body = "Hello, The user " +userName+ " has requested permission to your private Photos.";
                EmailService es = new EmailService();
                var result = await es.InsertMailMessage(em);
            }
            else 
            {
                var view = (Xamarin.Forms.View)sender;

                var PublicPhotosSection = view.FindByName<StackLayout>("PublicPhotosSection");
                var PrivatePhotosSection = view.FindByName<StackLayout>("PrivatePhotosSection");
                PublicPhotosSection.IsVisible = false;
                PrivatePhotosSection.IsVisible = true;
            }
        }

        private async void GrantPermission_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            ProfileEntity p = b.CommandParameter as ProfileEntity;
            int privateImage = p.PrivateImage;
            string userName = p.UserName;
            if (privateImage == 0)
            {
                DisplayAlert("Grant Permission Status", "Tap ok to grant permission to " + userName + " to your private pics.", "ok");
                ProfilePrivate pp = new ProfilePrivate();
                pp.LoginIdProfileId = SwipeCardView.UsrId.ToString();
                pp.TargetProfileId = p.Id.ToString();
                UsersMock um = new UsersMock();
                var result = um.ImageGrantPrivateAccess(pp);
                SwingSocial.Sample.Model.Email em = new SwingSocial.Sample.Model.Email();
                em.ProfileIdFrom = SwipeCardView.UsrId.ToString();
                em.ProfileTo = p.Id.ToString();
                em.Subject = "Access to Private Images Granted";
                em.Body = "Hello, The user " + userName + " has granted you permission to his private Photos.";
                EmailService es = new EmailService();
                var resultEmailSent= await es.InsertMailMessage(em);
                var view = (Xamarin.Forms.View)sender;

                var swipeSectionFrame = view.FindByName<Frame>("SwipeSection");
                var profileSectionFrame = view.FindByName<StackLayout>("ProfileSection");

                swipeSectionFrame.IsVisible = true;
                profileSectionFrame.IsVisible = false;
                SwingerSocialPageViewModel = new SwingerSocialPageViewModel(Navigation,this);
                BindingContext = SwingerSocialPageViewModel;


            }
            else 
            {
                DisplayAlert("Revoke Permission Status", "Tap ok to revoke permission to " + userName + " to your private pics.", "ok");
                SwingSocial.Sample.Model.Email em = new SwingSocial.Sample.Model.Email();
                ProfilePrivate pp = new ProfilePrivate();
                pp.LoginIdProfileId = SwipeCardView.UsrId.ToString();
                pp.TargetProfileId = p.Id.ToString();
                UsersMock um = new UsersMock();
                var result = await um.ImageRevokePrivateAccess(pp);
                SwingerSocialPageViewModel = new SwingerSocialPageViewModel(Navigation,this);
                BindingContext = SwingerSocialPageViewModel;

            }
        }
        private void SwipeMessageStartup_Clicked(object sender, EventArgs e)
        {
            modalStackLayout.IsVisible = false;
            SwipeMessageStartup.IsVisible = false;
        }
    }
}