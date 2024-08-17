using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class NewAccountDesireFantasiesPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public NewAccountDesireFantasiesPage()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            desiresFantasiesPicker.ItemsSource = new[]
            {
                "Anal",
                "BDSM"
            };
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

        private async void TapGestureRecognizerSkip_Tapped(object sender, EventArgs e)
        {
            var nextPage = new NewAccountVerificationPage();
            await Navigation.PushAsync(nextPage);   
        }

        private async void TapGestureRecognizerContinue_Tapped(object sender, EventArgs e)
        {
            UsersMock u = new UsersMock();
            NewAccountPage.newProfile.Desires = new string[] { desiresFantasiesPicker.SelectedItem.ToString() };
            NewAccountPage.newProfile.Fantasies = fantasiesEditor.Text;
            var result = u.EditProfilePage5(NewAccountPage.newProfile);
            var nextPage = new NewAccountVerificationPage();
            await Navigation.PushAsync(nextPage);

        }
    }
}