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
    public partial class MyProfileUploadUserNameAge : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        private string currentUserName;
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
        public MyProfileUploadUserNameAge(ProfileEntity p)
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            EntryUserName.Text = p.UserName;
            currentUserName = p.UserName;
            DoBDatePicker.Date = p.DateOfBirth.Value;
            BindingContext = NewAccountViewModel;
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            EntryUserName.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }


        private async void Saved_Tapped(object sender, EventArgs e)
        {
                        UsersMock u = new UsersMock();
                        var results = await u.InsertProfileCheckUsername(EntryUserName.Text);
                        if (results.Results.Equals("error duplicate") && EntryUserName.Text!=currentUserName)
                        {
                            await DisplayAlert("Input Validation", "Username already exists", "yes");
                            return;
                        }
                        else
                        {
                            newProfile.UserName = EntryUserName.Text;
                        }
                        newProfile.DateOfBirth = DoBDatePicker.Date;

            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.UpdateUserNameAge(newProfile);
            await DisplayAlert("Update Status", "Username and Age successfully updated.", "ok");
            SaveButton.IsVisible = false;
            FileActivityIndicator.IsVisible = true;
            FileActivityIndicator.IsRunning = true;
            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);
        }

    }
}