using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class MapPage : ContentPage
    {
        public static Position myLocation;
        PineapplePageViewModel pineapplePageViewModel;
        string currentPineapple = "pineapple.gif";

        public MapPage()
        {
            InitializeComponent();
            BindingContext = pineapplePageViewModel = new PineapplePageViewModel(Navigation);
            pineapple.Source = currentPineapple;
            GeneratePins();
            //map.InfoWindowClicked += Map_InfoWindowClicked;
            //map.PinClicked += Map_PinClicked;
        }

        void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            var popUp = view.FindByName<Frame>("PopUp");

            if (!popUp.IsVisible)
            {
                popUp.IsVisible = true;
            }
            else
            {
                popUp.IsVisible = false;

            }
        }

        private async void GeneratePins()
        {


            SwingerSocialPage.location = await Geolocation.GetLastKnownLocationAsync();
            MapPage.myLocation = new Position(SwingerSocialPage.location.Latitude, SwingerSocialPage.location.Longitude);
            Position location = new Position(MapPage.myLocation.Latitude, MapPage.myLocation.Longitude);

            CameraPosition cameraPosition = new CameraPosition(location, 18);

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            //map.InitialCameraUpdate = cameraUpdate;

            UsersMock mock = new UsersMock();
            List<PineApple> pins = await mock.LoadPineapples();
            foreach (var item in pins)
            {
                Pin pin = new Pin();
                pin.Position = new Position(Convert.ToDouble(item.Lattitude), Convert.ToDouble(item.Longitude));
                pin.Label = item.Label;
                pin.Icon = BitmapDescriptorFactory.FromBundle("pineapple.png");
                //map.Pins.Add(pin);

            }

        }


        private async void Map_InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
        {
            var navigationParams = new Dictionary<string, object>
            {
            };
            await Shell.Current.GoToAsync("SwingerSocialPage");

        }

        private void ShowPopUp(object sender, MapClickedEventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            var popUp = view.FindByName<Frame>("PopUp");
            if (!popUp.IsVisible)
            {
                popUp.IsVisible = true;
            }
            else
            {
                popUp.IsVisible = false;

            }
        }

        private void PopUp_Clicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            var popUp = view.FindByName<Frame>("PopUp");
            popUp.IsVisible = false;
        }

        private async void ProfilesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileEntity profile = (ProfileEntity)e.Item;
            string profileid = profile.Id.ToString();
            var secondPage = new ProfilesPagePineapple(profile);
            await Navigation.PushAsync(secondPage);
        }


        private async void OnChatClicked(object sender, EventArgs e)
        {
            var secondPage = new MessagesMenuTopPage();
            await Navigation.PushAsync(secondPage);
        }
    }
}