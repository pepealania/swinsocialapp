using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage4 : ContentPage
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
        public BRegistrationPage4()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            EmailEntry.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (EmailEntry.Text!=null && EmailEntry.Text!=String.Empty && IsValidEmail(EmailEntry.Text))
            {
                var secondPage = new BRegistrationPage5();
                await Navigation.PushAsync(secondPage);
            }
            else
            {
                await DisplayAlert("Input Validation", "Use email format: user@email.com", "yes");
            }
        }

    }
}