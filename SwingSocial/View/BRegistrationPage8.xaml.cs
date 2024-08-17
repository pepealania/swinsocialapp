using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage8 : ContentPage
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
        public BRegistrationPage8()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            ansPicker.ItemsSource = new[]
            {
                "Free"//,
                //"Premium"
            };
            ansPicker.SelectedIndex = 0;
            ansPicker.CheckedChanged += ansPicker_CheckedChanged;
            premiumOptions.IsVisible = true;

        }

        private void ansPicker_CheckedChanged(object sender, int e)
        {
            var radio = sender as CustomRadioButton;

            if (radio == null || radio.Id == -1)
            {
                return;
            }
            if (radio.Text=="Premium")
            {
                primeFeatures.IsVisible = true;
                freeFeatures.IsVisible = false;
                //listOfFeaturesPerAccountType.Source = "premium.jpg";
                premiumOptions.IsVisible = true;
                SignUpPay.Text = "Pay with Card";
            }
            else
            {
                primeFeatures.IsVisible = false;
                freeFeatures.IsVisible = true;
                //listOfFeaturesPerAccountType.Source = "free.jpg";
                premiumOptions.IsVisible = false;
                SignUpPay.Text = "Sign Up";
            }

            //DisplayAlert("Info", radio.Text, "OK");
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

        private void MonthlyTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //listOfFeaturesPerAccountType.Source = "premiummonthly.png";
            PriceAmmount.Text = "$24.95"; 
            monthlyLabel.BackgroundColor = Color.FromHex("#ba0c90");
            quarterlyLabel.BackgroundColor = Color.Black;
            bianuallyLabel.BackgroundColor = Color.Black;
            anuallyLabel.BackgroundColor = Color.Black;
        }
        private void QuarterlyTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //listOfFeaturesPerAccountType.Source = "premiumquarterly.png";
            PriceAmmount.Text = "$59.95"; 
            monthlyLabel.BackgroundColor = Color.Black;
            quarterlyLabel.BackgroundColor = Color.FromHex("#ba0c90");
            bianuallyLabel.BackgroundColor = Color.Black;
            anuallyLabel.BackgroundColor = Color.Black;

        }
        private void BiAnuallyTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //listOfFeaturesPerAccountType.Source = "premiumbianually.png";
            PriceAmmount.Text = "$109.95";
            monthlyLabel.BackgroundColor = Color.Black;
            quarterlyLabel.BackgroundColor = Color.Black;
            bianuallyLabel.BackgroundColor = Color.FromHex("#ba0c90");
            anuallyLabel.BackgroundColor = Color.Black;

        }
        private void AnuallyTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //listOfFeaturesPerAccountType.Source = "premiumanually.png";
            PriceAmmount.Text = "$199.95";
            monthlyLabel.BackgroundColor = Color.Black;
            quarterlyLabel.BackgroundColor = Color.Black;
            bianuallyLabel.BackgroundColor = Color.Black;
            anuallyLabel.BackgroundColor = Color.FromHex("#ba0c90");

        }

        private async void GotoPaymentBillingPage_Tapped(object sender, EventArgs e)
        {
            if (SignUpPay.Text=="Sign Up")
            {
                PopUpConfirm.IsVisible = true;
                MainScroll.Opacity = 0.5;
            }
            else
            {
                var secondPage = new PaymentBillingPage();
                await Navigation.PushAsync(secondPage);
            }
        }

        private async void TapGestureRecognizer_TappedConfirmBox(object sender, EventArgs e)
        {
            UsersMock usersMock = new UsersMock();
            var result = usersMock.SendWelcomeEmail(BRegistrationPage2.newProfile.Email);
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