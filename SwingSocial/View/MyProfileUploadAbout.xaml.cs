using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class MyProfileUploadAbout : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static MyProfileViewModel MyProfileViewModel { get; set; }
        public MyProfileUploadAbout(ProfileEntity p)
        {
            InitializeComponent();
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;
            aboutMeEditor.Text = p.About;   
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //MyProfileService mock = new MyProfileService();
            //List<SwingStyleTagsObject> _SwingerStyles = new List<SwingStyleTagsObject>() { new SwingStyleTagsObject { Description = "Exploring/Unsure" }, new SwingStyleTagsObject { Description = "Full Swap" }, new SwingStyleTagsObject { Description = "Swap" }, new SwingStyleTagsObject { Description = "Voyeur" } };
            //MyProfileViewModel.SwingStyles = new ObservableCollection<SwingStyleTagsObject>();
            //foreach (var item in _SwingerStyles)
            //{
            //    MyProfileViewModel.SwingStyles.Add(item);
            //}

           // SwingerStylesList.ItemsSource= MyProfileViewModel.SwingStyles;
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
            newProfile.About = aboutMeEditor.Text;
            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.EditProfileAbout(newProfile);
            await DisplayAlert("Update Status", "Information About me successfully updated.", "ok");
            SaveFrame.IsVisible = false;
            FileActivityIndicator.IsVisible = true;
            FileActivityIndicator.IsRunning = true;
            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);
        }

    }
}