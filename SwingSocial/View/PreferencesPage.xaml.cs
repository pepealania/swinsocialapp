using DurianCode.PlacesSearchBar;
using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class PreferencesPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static PreferencesViewModel PreferencesViewModel { get; set; }
        string GooglePlacesApiKey = "AIzaSyAbs5Umnu4RhdgslS73_TKDSV5wkWZnwi0";
        private string lattitude;
        private string longitude;
        public static bool commingFromPreferences;
        static public Preference MyCurrentPreferences;
        bool firstLoading;
        public PreferencesPage()
        {
            InitializeComponent();
            PreferencesViewModel = new PreferencesViewModel(Navigation);
            BindingContext = PreferencesViewModel;
            search_bar.ApiKey = GooglePlacesApiKey;
            search_bar.Type = PlaceType.All;
            search_bar.Components = new Components("country:us"); // Restrict results to Australia and New Zealand
            search_bar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
            search_bar.TextChanged += Search_Bar_TextChanged;
            search_bar.MinimumSearchText = 2;
            results_list.ItemSelected += Results_List_ItemSelected;
            VenueListStackLayout.IsVisible = false;
            results_list.IsVisible = false;
        }
        void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            if (firstLoading)
            {
                firstLoading = false;
                return;
            }
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
            {
                VenueListStackLayout.IsVisible = true;
                results_list.IsVisible = true;
            }

        }

        void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                VenueListStackLayout.IsVisible = false;
                results_list.IsVisible = false;
                spinner.IsVisible = true;
                spinner.IsRunning = true;
            }
            else
            {
                //VenueListStackLayout.IsVisible = true;
                //results_list.IsVisible = true;
                //spinner.IsRunning = false;
                //spinner.IsVisible = false;
            }
        }

        async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var prediction = (AutoCompletePrediction)e.SelectedItem;
            search_bar.Text = prediction.Description;
            results_list.SelectedItem = null;

            var place = await Places.GetPlace(prediction.Place_ID, GooglePlacesApiKey);

            if (place != null)
            {
                lattitude = place.Latitude.ToString();
                longitude = place.Longitude.ToString();
                await DisplayAlert(
                    place.Name, string.Format("Lat: {0}\nLon: {1}", place.Latitude, place.Longitude), "OK");
                VenueListStackLayout.IsVisible = false;
                results_list.IsVisible = false;

            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            firstLoading = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private async void SavePreferencesButton_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Preference p = b.CommandParameter as Preference;
            p.ProfileId=SwipeCardView.UsrId.ToString();
            PreferenceService ps = new PreferenceService();
            MyCurrentPreferences = p;
            var result = ps.InsertUpdatePreference(p);
            //commingFromPreferences = true;
            var nextPage = new SwingerSocialPage();
            await Navigation.PushAsync(nextPage);
        }


    }
}