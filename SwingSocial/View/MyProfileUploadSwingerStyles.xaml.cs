using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class MyProfileUploadSwingerStyles : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static MyProfileViewModel MyProfileViewModel { get; set; }
        public MyProfileUploadSwingerStyles(ProfileEntity p)
        {
            InitializeComponent();
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;
            for (int i = 0; i < p.SwingStyleTags.Length; i++)
            {
                if (p.SwingStyleTags[i]=="Exploring/Unsure")
                {
                    ExploringUnsure.IsChecked = true;
                }
                if (p.SwingStyleTags[i] == "Full Swap")
                {
                    FullSwap.IsChecked = true;
                }
                if (p.SwingStyleTags[i] == "Soft Swap") 
                {
                    SoftSwap.IsChecked = true;  
                }
                if (p.SwingStyleTags[i] == "Voyeur")
                {
                    Voyeur.IsChecked=true;
                }
            }
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
            string swingStyle = "";

            if (ExploringUnsure.IsChecked)
            {
                if (swingStyle== string.Empty)
                {
                    swingStyle = swingStyle + "''Exploring/Unsure''";

                }
                else 
                {
                    swingStyle = swingStyle + ",''Exploring/Unsure''";

                }
            }
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

            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.EditProfileSwingstyle(swingStyle);
            await DisplayAlert("Update Status", "Swinger Styles successfully updated.", "ok");
            SaveFrame.IsVisible = false;
            FileActivityIndicator.IsVisible = true;
            FileActivityIndicator.IsRunning = true;

            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);
        }
         
    }
}