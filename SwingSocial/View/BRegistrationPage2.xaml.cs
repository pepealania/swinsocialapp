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
    public partial class BRegistrationPage2 : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static string EmailToValidate;
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
        public BRegistrationPage2(string email)
        {
            EmailToValidate = email;
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;

        }
        private async void DisplayCodeResendAlert()
        {
            await DisplayAlert("Registration Status", "Code sent, if not received in 1 min then click resend", "ok");
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            Code1Entry.Focus();
            DisplayCodeResendAlert();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }




        private async void ContinueToRegistrationPage3_Tapped(object sender, EventArgs e)
        {
            if (Code1Entry.Text!=null && Code1Entry.Text != String.Empty)
            {
                bool isValidCode = await ValidationCodeIsValid();
                if (isValidCode)
                {
                    newProfile.Email = EmailToValidate;
                    var secondPage = new BRegistrationPage3();
                    await Navigation.PushAsync(secondPage);

                }
                else
                {
                    await DisplayAlert("Registration Status", "Write a valid verification code.", "ok");
                }
            }
            else
            {
                Code1Entry.Focus();
                await DisplayAlert("Registration Status", "Write a valid verification code.", "ok");
            }
            //var pineappleTimer = new System.Timers.Timer(20000);
            //pineappleTimer.Elapsed += new ElapsedEventHandler(DisplayCodeResendAlert);
            //pineappleTimer.Enabled = true;

        }

        private async Task<bool> ValidationCodeIsValid()
        {
            string completeCode = Code1Entry.Text;
            UsersMock u = new UsersMock();
            var infoToValidate = await u.EmailReturnEmailcode(EmailToValidate);
            return completeCode==infoToValidate.Code.ToString();
        }

        private void ResendCode_Tapped(object sender, EventArgs e)
        {
            UsersMock u = new UsersMock();
            var result = u.SendEmail(EmailToValidate);
            DisplayCodeResendAlert();

        }

    }
}