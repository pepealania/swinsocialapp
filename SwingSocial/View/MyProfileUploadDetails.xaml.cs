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
    public partial class MyProfileUploadDetails : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static MyProfileViewModel MyProfileViewModel { get; set; }
        public MyProfileUploadDetails(ProfileEntity p)
        {
            InitializeComponent();
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;
            Dob.Date = p.DateOfBirth.Value;
            Gender.ItemsSource = new[]
            {
                "Male",
                "Female",
                "Non-Binary"
            };
            Gender.SelectedItem = p.Gender;
            CouplePartSexOrientationPicker.ItemsSource = new[]
{
                "Straight",
                "Bi",
                "Bi-curious",
                "Open minded"
            };
            CouplePartSexOrientationPicker.SelectedItem = p.SexualOrientation;
            BodyTypePicker.ItemsSource = new[]
            {
 "Average",
"Slim/Petite",
"Ample",
"Athletic",
"BBW/BBM",
"A little extra padding"
            };
            BodyTypePicker.SelectedItem = p.BodyType;
            HairPicker.ItemsSource = new[]
            {
"Platinum Blonde",
"Other",
"Silver",
"Hair? What Hair?",
"Red/Auburn",
"Grey",
"White",
"Blonde",
"Salt and Pepper",
"Brown",
"Black"
            };
            HairPicker.SelectedItem = p.HairColor;
            EyesPicker.ItemsSource = new[]
            {
"Gray",
"Brown",
"Black",
"Green",
"Blue",
"Hazel"
            };
            EyesPicker.SelectedItem = p.EyeColor;
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            //Gender.Focus();
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
            if (Gender.SelectedItem==null)
            {
                await DisplayAlert("Update Status", "You should choose a Gender.", "ok");
                return;
            }
            if (CouplePartSexOrientationPicker.SelectedItem == null)
            {
                await DisplayAlert("Update Status", "You should choose a Sexual Orientation.", "ok");
                return;
            }
            if (EyesPicker.SelectedItem == null)
            {
                await DisplayAlert("Update Status", "You should choose Eyes color.", "ok");
                return;
            }

            newProfile.DateOfBirth = Dob.Date;
            newProfile.Gender = Gender.SelectedItem.ToString();
            newProfile.SexualOrientation = CouplePartSexOrientationPicker.SelectedItem.ToString();
            newProfile.BodyType = BodyTypePicker.SelectedItem==null?"''":BodyTypePicker.SelectedItem.ToString();
            newProfile.EyeColor = EyesPicker.SelectedItem.ToString();
            newProfile.HairColor = HairPicker.SelectedItem==null?String.Empty:HairPicker.SelectedItem.ToString();
            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.EditProfileDetails1(newProfile);
            await DisplayAlert("Update Status", "Swinger Styles successfully updated.", "ok");
            SaveFrame.IsVisible = false;
            FileActivityIndicator.IsVisible = true;
            FileActivityIndicator.IsRunning = true;
            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);
        }

    }
}