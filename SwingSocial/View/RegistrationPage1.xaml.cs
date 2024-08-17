using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class RegistrationPage1 : ContentPage
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
        public RegistrationPage1()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            listOfFeaturesPerAccountType.Source = "http://www.expatcallers.com/swingsocial/hedonism.jpg";
            UsernameEntry.Focus();
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
                        UsersMock u = new UsersMock();
                        var results = await u.InsertProfileCheckUsername(UsernameEntry.Text);
                        if (results.Results.Equals("error duplicate"))
                        {
                            await DisplayAlert("Input Validation", "Username already exists", "yes");
                            return;
                        }
                        else
                        {
                            newProfile.UserName = UsernameEntry.Text;
                        }
                
            
            PopUpConfirm.IsVisible = true;
            MainScroll.Opacity = 0.5;
        }

        private async void TapGestureRecognizer_TappedConfirmBox(object sender, EventArgs e)
        {
            UsersMock u = new UsersMock();
            var result = await u.InsertNewUserPage1(newProfile);
            newProfile.ProfileId = result.ProfileId;
            var secondPage = new RegistrationPage2();
            await Navigation.PushAsync(secondPage);
        }

        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpConfirm.IsVisible = false;
            MainScroll.Opacity = 1;
        }
    }
}