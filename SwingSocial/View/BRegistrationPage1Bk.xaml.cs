using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage1Bk : ContentPage
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
        public BRegistrationPage1Bk()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            PhoneNumberEntry.Focus();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }


        private async void TapGestureRecognizer_TappedConfirmBox(object sender, EventArgs e)
        {
            var secondPage = new BRegistrationPage2("");
            await Navigation.PushAsync(secondPage);
        }

        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpConfirm.IsVisible = false;
            MainScroll.Opacity = 1;
        }

        private async void OpenConfirmation_Tapped(object sender, EventArgs e)
        {
            if (PhoneNumberEntry.Text!=null && PhoneNumberEntry.Text!=string.Empty)
            {
                PopUpConfirm.IsVisible = true;
                MainScroll.Opacity = 0.5;
            }
            else
            {
                PhoneNumberEntry.Focus();
                await DisplayAlert("Registration Status", "Write a valid Phone number.", "ok");
            }
        }
    }
}