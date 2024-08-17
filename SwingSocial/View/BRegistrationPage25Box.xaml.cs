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
    public partial class BRegistrationPage25Box : ContentPage
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
        public BRegistrationPage25Box(string email)
        {
            EmailToValidate = email;
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
            Code1Entry.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }


        private void Code1Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Code1Entry.Text != string.Empty)
            {
                Code2Entry.Focus();
            }
        }

        private void Code2Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Code2Entry.Text != string.Empty)
            {
                Code3Entry.Focus();
            }
        }

        private void Code3Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Code3Entry.Text != string.Empty)
            {
                Code4Entry.Focus();
            }
        }

        private void Code4Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Code4Entry.Text != string.Empty)
            {
                Code5Entry.Focus();
            }
        }


        private async void ContinueToRegistrationPage3_Tapped(object sender, EventArgs e)
        {
            if (Code1Entry.Text!=null && Code2Entry.Text != null && Code3Entry.Text != null && Code4Entry.Text != null && Code5Entry.Text != null && Code1Entry.Text != String.Empty && Code2Entry.Text != String.Empty && Code3Entry.Text != String.Empty && Code4Entry.Text != String.Empty && Code5Entry.Text != String.Empty)
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
        }

        private async Task<bool> ValidationCodeIsValid()
        {
            string completeCode = Code1Entry.Text+Code2Entry.Text+Code3Entry.Text+Code4Entry.Text+Code5Entry.Text;
            UsersMock u = new UsersMock();
            var infoToValidate = await u.EmailReturnEmailcode(EmailToValidate);
            return completeCode==infoToValidate.Code.ToString();
        }

        private void ResendCode_Tapped(object sender, EventArgs e)
        {
            UsersMock u = new UsersMock();
            var result = u.SendEmail(EmailToValidate);

        }
        private void Code1Entry_Focused(object sender, FocusEventArgs e)
        {
            Code1Entry.Text = "";
        }
        private void Code2Entry_Focused(object sender, FocusEventArgs e)
        {
            Code2Entry.Text = "";
        }
        private void Code3Entry_Focused(object sender, FocusEventArgs e)
        {
            Code3Entry.Text = "";
        }
        private void Code4Entry_Focused(object sender, FocusEventArgs e)
        {
            Code4Entry.Text = "";
        }
        private void Code5Entry_Focused(object sender, FocusEventArgs e)
        {
            Code5Entry.Text = "";
        }
    }
}