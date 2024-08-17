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
    public partial class NewAccountPage : ContentPage
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
        public NewAccountPage()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            ansPicker.ItemsSource = new[]
            {
                "Free",
                "Premium"
            };
            ansPicker.SelectedIndex = 0;
            premiumOptions.IsVisible = false;
            ansPicker.CheckedChanged += ansPicker_CheckedChanged;
            var view = (Xamarin.Forms.View)PersonalInfoListView;
            //var element = PersonalInfoListView.BindingContext;
            view.Focus();
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
                listOfFeaturesPerAccountType.Source = "premiumquarterly.png";
                premiumOptions.IsVisible = true;
            }
            else
            {
                listOfFeaturesPerAccountType.Source = "freeaccount.png";
                premiumOptions.IsVisible = false;
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
            listOfFeaturesPerAccountType.Source = "premiummonthly.png";
            monthlyLabel.BackgroundColor = Color.Violet;
            quarterlyLabel.BackgroundColor = Color.Black;
            bianuallyLabel.BackgroundColor = Color.Black;
            anuallyLabel.BackgroundColor = Color.Black;
        }
        private void QuarterlyTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            listOfFeaturesPerAccountType.Source = "premiumquarterly.png";
            monthlyLabel.BackgroundColor = Color.Black;
            quarterlyLabel.BackgroundColor = Color.Violet;
            bianuallyLabel.BackgroundColor = Color.Black;
            anuallyLabel.BackgroundColor = Color.Black;

        }
        private void BiAnuallyTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            listOfFeaturesPerAccountType.Source = "premiumbianually.png";
            monthlyLabel.BackgroundColor = Color.Black;
            quarterlyLabel.BackgroundColor = Color.Black;
            bianuallyLabel.BackgroundColor = Color.Violet;
            anuallyLabel.BackgroundColor = Color.Black;

        }
        private void AnuallyTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            listOfFeaturesPerAccountType.Source = "premiumanually.png";
            monthlyLabel.BackgroundColor = Color.Black;
            quarterlyLabel.BackgroundColor = Color.Black;
            bianuallyLabel.BackgroundColor = Color.Black;
            anuallyLabel.BackgroundColor = Color.Violet;

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            foreach (var item in NewAccountViewModel.PersonalInfoItems)
            {
                switch (item.PersonalInfoItemPlaceholder)
                {
                    case "First name":
                        if (item.TextValue == null || item.TextValue.Trim().Equals(String.Empty))
                        {
                            await DisplayAlert("Input Validation", "You have to insert First Name", "yes");
                            var view = (Xamarin.Forms.View)PersonalInfoListView;

                            //var entryFirstName = view.FindByName<Entry>("EntryFirstName");
                            view.Focus();

                            return;
                        }
                        else
                        {
                            newProfile.FirstName = item.TextValue;

                        }
                        break;
                    case "Last name":
                        if (item.TextValue == null || item.TextValue.Trim().Equals(String.Empty))
                        {
                            await DisplayAlert("Input Validation", "You have to insert Last Name", "yes");
                            var view = (Xamarin.Forms.View)PersonalInfoListView;

                            //var entryFirstName = view.FindByName<Entry>("EntryFirstName");
                            view.Focus();

                            return;
                        }
                        else
                        {
                            newProfile.LastName = item.TextValue;

                        }
                        break;

                    case "Email":

                        if (IsValidEmail(item.TextValue))
                        {
                            newProfile.Email = item.TextValue;
                        }
                        else
                        {
                            await DisplayAlert("Input Validation", "Use email format: user@email.com", "yes");
                            return;
                        }
                        break;
                    case "Username":
                        UsersMock u = new UsersMock();
                        var results = await u.InsertProfileCheckUsername(item.TextValue);
                        if (results.Results.Equals("error duplicate"))
                        {
                            await DisplayAlert("Input Validation", "Username already exists", "yes");
                            return;
                        }
                        else
                        {
                            newProfile.UserName = item.TextValue;
                        }
                        break;
                    case "Phone":
                        newProfile.Phone = item.TextValue;
                        break;
                    case "6/21/2018":
                        newProfile.DateOfBirth = Convert.ToDateTime(item.TextValue);
                        break;
                    case "Password":
                        newProfile.Password = item.TextValue;
                        break;
                    case "Confirm Password":
                        newProfile.ConfirmPassword = item.TextValue;
                        break;
                    case "Gender":
                        newProfile.Gender = item.TextValue;
                        break;
                    default:
                        break;
                }
            }
            newProfile.Type = ansPicker.SelectedIndex +1;
            PopUpConfirm.IsVisible = true;
            MainScroll.Opacity = 0.5;
        }

        private async void TapGestureRecognizer_TappedConfirmBox(object sender, EventArgs e)
        {
            UsersMock u = new UsersMock();
            var result = await u.InsertProfilePage1(newProfile);
            newProfile.ProfileId = result.ProfileId;
            var secondPage = new NewAccountBioInfoPage();
            await Navigation.PushAsync(secondPage);
        }

        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpConfirm.IsVisible = false;
            MainScroll.Opacity = 1;
        }
    }
}