using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BillingScreenPage : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static ProfileBillingInfoViewModel ProfileBillingInfoViewModel { get; set; }
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
        public BillingScreenPage()
        {
            InitializeComponent();
            ProfileBillingInfoViewModel = new ProfileBillingInfoViewModel(Navigation);
            BindingContext = ProfileBillingInfoViewModel;

        }


        private async void TapGestureRecognizer_TappedConfirmBox(object sender, EventArgs e)
        {
            var secondPage = new Gateway();
            await Navigation.PushAsync(secondPage);
        }

        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpConfirm.IsVisible = false;
            MainScroll.Opacity = 1;
        }
    }
}