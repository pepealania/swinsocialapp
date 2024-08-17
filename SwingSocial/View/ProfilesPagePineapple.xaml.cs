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
    public partial class ProfilesPagePineapple : ContentPage
    {
        public static Location location;
        string currentPineapple= "pineapple.gif";
        public static ProfileEntity currentSwipeProfile;
        public ProfilesPagePineapple(ProfileEntity p)
        {
            try
            {
                InitializeComponent();
                BindingContext = new ProfilesPagePineappleViewModel(Navigation,p);

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
            var IdString = view.FindByName<Span>("IdString");
            MLToolkit.Forms.SwipeCardView.SwipeCardView.CurrentIdString = IdString.Text;
            var threshold = (BindingContext as ProfilesPagePineappleViewModel).Threshold;

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
                    //nopeButton.Scale = 0.2;
                    //likeButton.Scale = 0.2;
                    //superLikeButton.Scale = 0.2;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
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