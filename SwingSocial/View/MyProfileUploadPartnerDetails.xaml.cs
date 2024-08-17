using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Models;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class MyProfileUploadPartnerDetails : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static MyProfileViewModel MyProfileViewModel { get; set; }
        public MyProfileUploadPartnerDetails(ProfileEntity p)
        {
            InitializeComponent();
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;

            Dob.Date = p.PartnerDateOfBirth.Value;
            Gender.ItemsSource = new[]
            {
                "Male",
                "Female",
                "Non-Binary"
            };
            Gender.SelectedItem = p.PartnerGender;
            CouplePartSexOrientationPicker.ItemsSource = new[]
{
                "Straight",
                "Bi",
                "Bi-curious",
                "Open minded"
            };
            CouplePartSexOrientationPicker.SelectedItem = p.PartnerSexualOrientation;
            BodyTypePicker.ItemsSource = new[]
            {
 "Average",
"Slim/Petite",
"Ample",
"Athletic",
"BBW/BBM",
"A little extra padding"
            };
            BodyTypePicker.SelectedItem = p.PartnerBodyType;
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
            HairPicker.SelectedItem = p.PartnerHairColor;
            EyesPicker.ItemsSource = new[]
            {
"Gray",
"Brown",
"Black",
"Green",
"Blue",
"Hazel"
            };
            EyesPicker.SelectedItem = p.PartnerEyeColor;
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
            newProfile.PartnerDateOfBirth = Dob.Date;
            newProfile.PartnerGender = Gender.SelectedItem.ToString();
            newProfile.PartnerSexualOrientation = CouplePartSexOrientationPicker.SelectedItem.ToString();
            newProfile.PartnerBodyType = BodyTypePicker.SelectedItem.ToString();
            newProfile.PartnerEyeColor = EyesPicker.SelectedItem.ToString();
            newProfile.PartnerHairColor = HairPicker.SelectedItem==null?"":HairPicker.SelectedItem.ToString();

            ProfileDetails profileDetails = new ProfileDetails();
            profileDetails.Gender = newProfile.PartnerGender;
            IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-US");
            profileDetails.Birthday = newProfile.PartnerDateOfBirth.Value.ToString("MM/dd/yyyy", iFormatProvider);
            profileDetails.BodyType = newProfile.PartnerBodyType;
            profileDetails.Orientation = newProfile.PartnerSexualOrientation;
            profileDetails.EyeColor = newProfile.PartnerEyeColor;
            profileDetails.HairColor = newProfile.PartnerHairColor;
            profileDetails.ProfileId = SwipeCardView.UsrId.ToString();
            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.EditPartnerProfileDetails2(profileDetails);
            await DisplayAlert("Update Status", "Partner Details successfully updated.", "ok");
            SaveFrame.IsVisible = false;
            FileActivityIndicator.IsVisible = true;
            FileActivityIndicator.IsRunning = true;
            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);
        }

    }
}