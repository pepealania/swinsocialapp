using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage7 : ContentPage
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
        public BRegistrationPage7()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
//             swingStylePicker.ItemsSource = new[]
//{
//                "Full Swap",
//                "Soft Swap",
//                "Voyeur"
//            };
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

        private async void GotoBRegistrationPage71_Tapped(object sender, EventArgs e)
        {
            string swingStyle = "";

            //if (ExploringUnsure.IsChecked)
            //{
            //    if (swingStyle == string.Empty)
            //    {
            //        swingStyle = swingStyle + "''Exploring/Unsure''";

            //    }
            //    else
            //    {
            //        swingStyle = swingStyle + ",''Exploring/Unsure''";

            //    }
            //}
            if (FullSwap.IsChecked)
            {
                if (swingStyle == string.Empty)
                {
                    swingStyle = swingStyle + "''Full Swap''";

                }
                else
                {
                    swingStyle = swingStyle + ",''Full Swap''";

                }

            }
            if (SoftSwap.IsChecked)
            {
                if (swingStyle == string.Empty)
                {
                    swingStyle = swingStyle + "''Soft Swap''";

                }
                else
                {
                    swingStyle = swingStyle + ",''Soft Swap''";

                }

            }
            if (Voyeur.IsChecked)
            {
                if (swingStyle == string.Empty)
                {
                    swingStyle = swingStyle + "''Voyeur''";

                }
                else
                {
                    swingStyle = swingStyle + ",''Voyeur''";

                }

            }
            BRegistrationPage2.newProfile.SwingStyleString = swingStyle;

            var secondPage = new BRegistrationPage71();
            await Navigation.PushAsync(secondPage);
        }


    }
}