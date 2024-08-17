using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class RegistrationPage2 : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public RegistrationPage2()
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
            NewAccountPage.newProfile.Age = Convert.ToInt32(ageEntry.Text);
            NewAccountPage.newProfile.Email = emailEntry.Text;
            //UsersMock u = new UsersMock();
            //var result = await u.EditProfilePage1(NewAccountPage.newProfile);
            //NewAccountPage.newProfile.UserType
            var nextPage = new RegistrationPage3();
            await Navigation.PushAsync(nextPage);
        }
    }
}