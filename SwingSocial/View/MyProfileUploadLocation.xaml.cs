using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class MyProfileUploadLocation : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public MyProfileUploadLocation(ProfileEntity p)
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            EntryLocation.Text = p.Location;
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }


        private async void Saved_Tapped(object sender, EventArgs e)
        {
            newProfile.Location = EntryLocation.Text;

            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.EditProfileLocation(newProfile);
            await DisplayAlert("Update Status", "Location successfully updated.", "ok");
            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);
        }

        private void EntryLocation_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            MyProfileService myProfileService = new MyProfileService();
            mylocationsList.ItemsSource = await myProfileService.EditProfileCitySearch(EntryLocation.Text);

        }

        private void mylocationsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileLocation pl = e.Item as ProfileLocation;
            EntryLocation.Text = pl.City + ", " + pl.State;
        }

        private async void Send_Tapped(object sender, EventArgs e)
        {
            newProfile.Location = EntryLocation.Text;

            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.EditProfileLocation(newProfile);
            await DisplayAlert("Update Status", "Location successfully updated.", "ok");
            ListOfLocations.IsVisible = false;
            FileActivityIndicator.IsVisible = true;
            FileActivityIndicator.IsRunning = true;

            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);

        }
    }
}