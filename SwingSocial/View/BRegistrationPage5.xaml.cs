using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage5 : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public BRegistrationPage5()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            userTypesPicker.ItemsSource = new[]
            {
                "Couple",
                "Man",
                "Woman",
                "Throuple"
            };
                CoupleMySexOrientationPicker.ItemsSource = new[]
    {
                    "Straight",
                    "Bi",
                    "Bi-curious",
                    "Open minded"
                };
            CouplePartSexOrientationPicker.ItemsSource = new[]
{
                "Straight",
                "Bi",
                "Bi-curious",
                "Open minded"
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

        private async void GotoBRegistrationPage6_Tapped(object sender, EventArgs e)
        {
            BRegistrationPage2.newProfile.AccountType=userTypesPicker.SelectedItem.ToString();
            BRegistrationPage2.newProfile.DateOfBirth = SingleAgeEntry.Text==string.Empty?DateTime.Now:GetDateOfBirthFromAge(Convert.ToInt32(SingleAgeEntry.Text));
            var nextPage = new BRegistrationPage6();
            await Navigation.PushAsync(nextPage);
        }

        private DateTime GetDateOfBirthFromAge(int age)
        {
            DateTime today = DateTime.Today;
            DateTime dateOfBirth = today.AddYears(-age);
            return dateOfBirth;
        }

        private void userTypesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userTypesPicker.SelectedItem == "Man" || userTypesPicker.SelectedItem == "Woman")
            {
                SingleAgeLabel.IsVisible = true;
                SingleAgeEntry.IsVisible = true;
                CoupleStackLayout.IsVisible = false;
            }
            else
            {
                CoupleStackLayout.IsVisible = true;
                SingleAgeLabel.IsVisible = false;
                SingleAgeEntry.IsVisible = false;

            }
        }
    }
}