using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class NewAccountVerificationPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public NewAccountVerificationPage()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            ProfileBanner.Source = NewAccountPage.newProfile.ProfileBanner;
            Avatar.Source = NewAccountPage.newProfile.Avatar;
            UserName.Text = NewAccountPage.newProfile.UserName;
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var nextPage = new NewAccountUploadPage();
            await Navigation.PushAsync(nextPage);   
        }
    }
}