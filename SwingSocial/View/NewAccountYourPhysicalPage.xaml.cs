using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class NewAccountYourPhysicalPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public NewAccountYourPhysicalPage()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            genderPicker.ItemsSource = new[]
            {
                "Male",
                "Female",
                "Non-Binary"
            };
            sexualOrientationPicker.ItemsSource = new[]
            {
                "Straight",
                "Bi-sexual",
                "Bi-curious",
                "Gay"
            };
            bodyTypePicker.ItemsSource = new[]
            {
                "Average",
                "Athletic",
                "Ample",
                "Slim/Petite",
                "A little extra padding",
                "BBW/BBM"
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
            NewAccountPage.newProfile.Gender = genderPicker.SelectedItem.ToString();
            NewAccountPage.newProfile.SexualOrientation = sexualOrientationPicker.SelectedItem.ToString();
            NewAccountPage.newProfile.DateOfBirth= BirthdayDatePicker.Date;
            NewAccountPage.newProfile.BodyType = bodyTypePicker.SelectedItem.ToString();
            UsersMock u = new UsersMock();
            var result = await u.EditProfilePage2(NewAccountPage.newProfile);

            var nextPage = new NewAccountUploadPage();
            await Navigation.PushAsync(nextPage);   
        }
    }
}