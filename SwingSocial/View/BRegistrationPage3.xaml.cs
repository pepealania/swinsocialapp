using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage3 : ContentPage
    {
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
        public BRegistrationPage3()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            //listOfFeaturesPerAccountType.Source = "http://www.expatcallers.com/swingsocial/hedonism.jpg";
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            UsernameEntry.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void GoToBRegistrationPage4_Tapped(object sender, EventArgs e)
        {
            UsersMock u = new UsersMock();
            var results = await u.InsertProfileCheckUsername(UsernameEntry.Text);
            if (results.Results.Equals("error duplicate"))
            {
                await DisplayAlert("Input Validation", "Username already exists", "yes");
            }
            else
            {
                BRegistrationPage2.newProfile.UserName = UsernameEntry.Text;
                var secondPage = new BRegistrationPage5();
                await Navigation.PushAsync(secondPage);
            }

        }


    }
}