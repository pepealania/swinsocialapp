using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage6 : ContentPage
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
        public BRegistrationPage6()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            //lookingForPicker.ItemsSource = new[]
            //{
            //    "Males",
            //    "Females",
            //    "Couples"
            //};
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

        private async void GotoBRegistrationPage7_Tapped(object sender, EventArgs e)
        {
            string lookingFor = "";

            if (Males.IsChecked)
            {
                if (lookingFor == string.Empty)
                {
                    lookingFor = lookingFor + "''Males''";

                }
                else
                {
                    lookingFor = lookingFor + ",''Males''";

                }
            }
            if (Females.IsChecked)
            {
                if (lookingFor == string.Empty)
                {
                    lookingFor = lookingFor + "''Females''";

                }
                else
                {
                    lookingFor = lookingFor + ",''Females''";

                }

            }
            if (Couples.IsChecked)
            {
                if (lookingFor == string.Empty)
                {
                    lookingFor = lookingFor + "''Couples''";

                }
                else
                {
                    lookingFor = lookingFor + ",''Couples''";

                }

            }
            BRegistrationPage2.newProfile.InterestedIn = lookingFor;
            var secondPage = new BRegistrationPage7();
            await Navigation.PushAsync(secondPage);
        }


    }
}