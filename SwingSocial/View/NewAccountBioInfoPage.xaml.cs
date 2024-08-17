using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class NewAccountBioInfoPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public NewAccountBioInfoPage()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            userTypesPicker.ItemsSource = new[]
            {
                "Man",
                "Woman",
                "Couple",
                "Throuple"
            };
            lookingForPicker.ItemsSource = new[]
            {
                "Man",
                "Woman",
                "Couple",
                "Throuple"
            };
            swingStylePicker.ItemsSource = new[]
            {
                "Full Swap",
                "Soft Swap",
                "Voyeur",
                "Exploring/Unsure"
            };
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            NewAccountPage.newProfile.AccountType = userTypesPicker.SelectedItem.ToString();
            NewAccountPage.newProfile.LookingForTags = new string[] { lookingForPicker.SelectedItem.ToString() };
            NewAccountPage.newProfile.Location = homeCityEntry.Text;
            NewAccountPage.newProfile.Tagline = taglineEditor.Text;
            NewAccountPage.newProfile.About = aboutMeEditor.Text;
            NewAccountPage.newProfile.SwingStyleTags = new string[] { swingStylePicker.SelectedItem.ToString() };
            UsersMock u = new UsersMock();
            var result = await u.EditProfilePage1(NewAccountPage.newProfile);
            //NewAccountPage.newProfile.UserType
            var nextPage = new NewAccountYourPhysicalPage();
            await Navigation.PushAsync(nextPage);
        }
    }
}