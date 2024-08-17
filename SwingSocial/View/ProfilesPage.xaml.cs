using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;


namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesPage : ContentPage
    {
        public static Location location;
        string currentPineapple= "pineapple.gif";
        public static ProfileEntity currentSwipeProfile;
        public ProfilesPage(RSVP rsvp)
        {
            try
            {
                InitializeComponent();
                BindingContext = new ProfilesPageViewModel(rsvp,Navigation);

                pineapple.Source = currentPineapple;
                var pineappleTimer = new System.Timers.Timer(300000);
                pineappleTimer.Elapsed += new ElapsedEventHandler(SwitchPineapples);
                pineappleTimer.Enabled = true;
                MySwipeCardView.Dragging += OnDragging;
                SetupLocation();
                var pineappleSaveTimer = new System.Timers.Timer(300000);
                pineappleSaveTimer.Elapsed += new ElapsedEventHandler(SaveMyPosition);
                pineappleSaveTimer.Enabled = true;
                SaveMyPosition(null, null);
                
                //GetCurrentLocation();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private async void SetupLocation()
        {
            SwingerSocialPage.location = await Geolocation.GetLastKnownLocationAsync();
            //MapPage.myLocation = new Position(SwingerSocialPage.location.Latitude, SwingerSocialPage.location.Longitude);
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

        private async void SaveMyPosition(object source, ElapsedEventArgs e)
        {
            UsersMock usersMock = new UsersMock();

            usersMock.SaveMyPosition(SwingerSocialPage.location);
        }

        private async void OnChatClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var currentSwipeProfile = view.FindByName<SwipeCardView>("MySwipeCardView");
            ProfileEntity profile = currentSwipeProfile.TopItem as ProfileEntity;
            //string myname = currentSwipeProfile.Text;
            //var navigationParams = new Dictionary<string, object>
            //{
            //};
            //await Shell.Current.GoToAsync("ChatPage", true);
            //Event currentEvent = e.Item as Event;
            //string result = currentEvent.Name;
            var secondPage = new MyChats();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

        private void OnDragging(object sender, DraggingCardEventArgs e)
        {
            MLToolkit.Forms.SwipeCardView.SwipeCardView.CurrentIdString = null;
        }
        //HideSwipeImage
        private async void HideSwipeImage(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            
            //var topStackLayout = view.FindByName<StackLayout>("TopStackLayout");
            //MySwipeCardView.SupportedSwipeDirections = SwipeCardDirection.None;
            //MySwipeCardView.SupportedDraggingDirections = SwipeCardDirection.None;

            var swipeSectionFrame = view.FindByName<Frame>("SwipeSection");
            var profileSectionFrame = view.FindByName<StackLayout>("ProfileSection");
            var ProfileLocation = view.FindByName<Button>("ProfileLocation");

            swipeSectionFrame.IsVisible = false;
            profileSectionFrame.IsVisible = true;
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
            var partnerTableHideShow = view.FindByName<ImageButton>("PartnerTableHideShow");

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
    }
}