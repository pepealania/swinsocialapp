using Java.Lang;
using Java.Util.Streams;
using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using PCLStorage;
using SwingSocial.Sample.Interface;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class MyProfilePage : ContentPage
    {
        public static MyProfileViewModel MyProfileViewModel { get; set; }
        internal static bool TransactionAlive;
        static object imageStream1;
        static object imageStream2;
        static object imageStream3;
        static object imageStream4;
        static object imageStream5;
        static object imagePrivateStream1;
        static object imagePrivateStream2;
        static object imagePrivateStream3;
        static object imagePrivateStream4;
        static object imagePrivateStream5;
        static string ftpurl = "ftp://expatcallers.com/swingsocial/events/";
        static string imageUrl1;
        static string imageUrl2;
        static string imageUrl3;
        static string imageUrl4;
        static string imageUrl5;
        static string imagePrivateUrl1;
        static string imagePrivateUrl2;
        static string imagePrivateUrl3;
        static string imagePrivateUrl4;
        static string imagePrivateUrl5;
        static string imageId1;
        static string imageId2;
        static string imageId3;
        static string imageId4;
        static string imageId5;
        static string imagePrivateId1;
        static string imagePrivateId2;
        static string imagePrivateId3;
        static string imagePrivateId4;
        static string imagePrivateId5;
        string currentPineapple = "pineapple.gif";

        static string imageUrlBase = "http://www.expatcallers.com/swingsocial/events/";
        static string ftpusername = "jose3@expatcallers.com"; // e.g. username
        static string ftppassword = "Peru2020#Peru"; // e.g. password
        public MyProfilePage()
        {
            InitializeComponent();
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;
            pineapple.Source = currentPineapple;

            //AvailableButtonLabel.Text = "&#x1f34d Available";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            TransactionAlive = true;
            MyProfileService mock = new MyProfileService();
            MyProfileViewModel.MyProfile = await mock.EditProfileMyProfile();
            SwingerSocialPage.FromSwingerSocialPage = false;
            if (MyProfileUploadPrivateCrop.commingFromPrivateCrop)
            {
                if (MyProfileUploadPrivateCrop._order==1)
                {
                    ImageToDisplayPrivateImage1.Source = MyProfileUploadPrivateCrop.currentStream;
                    PrivateImageFrame.IsVisible = true;
                    imagePrivateUrl1 = MyProfileUploadPrivateCrop.imagePrivateUrl;
                    MyProfileUploadPrivateCrop._order = 0;
                }
                if (MyProfileUploadPrivateCrop._order == 2)
                {
                    ImageToDisplayPrivateImage2.Source = MyProfileUploadPrivateCrop.currentStream;
                    PrivateImageFrame.IsVisible = true;
                    imagePrivateUrl2 = MyProfileUploadPrivateCrop.imagePrivateUrl;
                    MyProfileUploadPrivateCrop._order = 0;
                }
                if (MyProfileUploadPrivateCrop._order == 3)
                {
                    ImageToDisplayPrivateImage3.Source = MyProfileUploadPrivateCrop.currentStream;
                    PrivateImageFrame.IsVisible = true;
                    imagePrivateUrl3 = MyProfileUploadPrivateCrop.imagePrivateUrl;
                    MyProfileUploadPrivateCrop._order = 0;
                }
                if (MyProfileUploadPrivateCrop._order == 4)
                {
                    ImageToDisplayPrivateImage4.Source = MyProfileUploadPrivateCrop.currentStream;
                    PrivateImageFrame.IsVisible = true;
                    imagePrivateUrl4 = MyProfileUploadPrivateCrop.imagePrivateUrl;
                    MyProfileUploadPrivateCrop._order = 0;
                }
                if (MyProfileUploadPrivateCrop._order == 5)
                {
                    ImageToDisplayPrivateImage5.Source = MyProfileUploadPrivateCrop.currentStream;
                    PrivateImageFrame.IsVisible = true;
                    imagePrivateUrl5 = MyProfileUploadPrivateCrop.imagePrivateUrl;
                    MyProfileUploadPrivateCrop._order = 0;
                }
                MyProfileUploadPrivateCrop.commingFromPrivateCrop = false;
            }

            if (MyProfileUploadPublicCrop.commingFromPublicCrop)
            {
                if (MyProfileUploadPublicCrop._order==1)
                {
                    ImageToDisplayCoverImage1.Source = MyProfileUploadPublicCrop.currentStream;
                    ImageFrame.IsVisible = true;
                    imageUrl1 = MyProfileUploadPublicCrop.imagePublicUrl;
                    MyProfileUploadPublicCrop._order = 0;
                }
                if (MyProfileUploadPublicCrop._order == 2)
                {
                    ImageToDisplayCoverImage2.Source = MyProfileUploadPublicCrop.currentStream;
                    ImageFrame.IsVisible = true;
                    imageUrl2 = MyProfileUploadPublicCrop.imagePublicUrl;
                    MyProfileUploadPublicCrop._order = 0;
                }
                if (MyProfileUploadPublicCrop._order == 3)
                {
                    ImageToDisplayCoverImage3.Source = MyProfileUploadPublicCrop.currentStream;
                    ImageFrame.IsVisible = true;
                    imageUrl3 = MyProfileUploadPublicCrop.imagePublicUrl;
                    MyProfileUploadPublicCrop._order = 0;
                }
                if (MyProfileUploadPublicCrop._order == 4)
                {
                    ImageToDisplayCoverImage4.Source = MyProfileUploadPublicCrop.currentStream;
                    ImageFrame.IsVisible = true;
                    imageUrl4 = MyProfileUploadPublicCrop.imagePublicUrl;
                    MyProfileUploadPublicCrop._order = 0;
                }
                if (MyProfileUploadPublicCrop._order == 5)
                {
                    ImageToDisplayCoverImage5.Source = MyProfileUploadPublicCrop.currentStream;
                    ImageFrame.IsVisible = true;
                    imageUrl5 = MyProfileUploadPublicCrop.imagePublicUrl;
                    MyProfileUploadPublicCrop._order = 0;
                }
                MyProfileUploadPublicCrop.commingFromPublicCrop = false;
            }

        }
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            TransactionAlive = false;
        }
        private void HideShowPartnerTable(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var partnerTableLabel = view.FindByName<Label>("PartnerTableLabel");
            var partnerTable = view.FindByName<WebView>("PartnerTable");
            var partnerTableHideShow = view.FindByName<ImageButton>("PartnerTableHideShow");

            if (partnerTable.IsVisible)
            {
                partnerTableHideShow.Source = "circleplus";
                partnerTableHideShow.HeightRequest = 50;
                partnerTable.IsVisible = false;
            }
            else
            {
                partnerTableHideShow.Source = "circleminus";
                partnerTable.IsVisible = true;
                partnerTableHideShow.HeightRequest = 50;

            }
        }

        private async void Preferences_Tapped(object sender, EventArgs e)
        {
            try
            {
                var nextPage = new PreferencesPage();
                await Navigation.PushAsync(nextPage);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
        
        private async void Billing_Tapped(object sender, EventArgs e)
        {
            var nextPage = new BillingScreenPage();
            await Navigation.PushAsync(nextPage);
        }

        private async void EditAvatar_Clicked(object sender, EventArgs e)
        {
            var nextPage = new MyProfileUploadAvatarNew();
            await Navigation.PushAsync(nextPage);  
        }

        private async void EditProfileBanner_Clicked(object sender, EventArgs e)
        {
            var nextPage = new MyProfileUploadProfileBannerNew();
            await Navigation.PushAsync(nextPage);

        }

        private async void EditUserNameAge_Clicked(object sender, EventArgs e)
        {
            ImageButton b = sender as ImageButton;
            ProfileEntity p = b.CommandParameter as ProfileEntity;
            var nextPage = new MyProfileUploadUserNameAge(p);
            await Navigation.PushAsync(nextPage);
        }

        private async void Location_Clicked(object sender, EventArgs e)
        {
            ImageButton b = sender as ImageButton;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            var nextPage = new MyProfileUploadLocation(p);
            await Navigation.PushAsync(nextPage);
        }

        private async void SwingStyles_Clicked(object sender, EventArgs e)
        {
            ImageButton b = sender as ImageButton;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            var secondPage = new MyProfileUploadSwingerStyles(p);
            await Navigation.PushAsync(secondPage);
        }

        private async void About_Clicked(object sender, EventArgs e)
        {
            ImageButton b = sender as ImageButton;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            var nextPage = new MyProfileUploadAbout(p);
            await Navigation.PushAsync(nextPage);
        }
        
        private async void Tegline_Clicked(object sender, EventArgs e)
        {
            ImageButton b = sender as ImageButton;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            var nextPage = new MyProfileUploadTagline(p);
            await Navigation.PushAsync(nextPage);
        }

        private async void Details_Clicked(object sender, EventArgs e)
        {
            ImageButton b = sender as ImageButton; 
            ProfileEntity p = b.CommandParameter as ProfileEntity;
            var nextPage = new MyProfileUploadDetails(p);
            await Navigation.PushAsync(nextPage);
        }

        private async void Logout_Tapped(object sender, EventArgs e)
        {
            Login.MyProfile=null;
            //DependencyService.Get<IFileService>().CreateFile("0");
            DeleteCookie("SwingSocial");
            await DisplayAlert("Logout Status", "Seesion successfully finished.", "ok");
            var secondPage = new Login();
            await Navigation.PushAsync(secondPage);

        }
        private async void DeleteCookie(string v)
        {
            bool result = await FileInternalStorageService.IsFolderExistAsync(v);
            if (result)
            {
                IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.GetFolderAsync(v);
                await folder.DeleteAsync();
            }
        }

        private async void EditAccountType_Clicked(object sender, EventArgs e)
        {
            ImageButton b = sender as ImageButton;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            var nextPage = new MyProfileUploadAccountType(p);
            await Navigation.PushAsync(nextPage);   
        }

        private async void PartnerDetails_Clicked(object sender, EventArgs e)
        {
            ImageButton b = sender as ImageButton;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            var nextPage = new MyProfileUploadPartnerDetails(p);
            await Navigation.PushAsync(nextPage);
        }
        private void MyPhoto1_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;
            imageId1 = myProfile.Image1Id == null ? null : myProfile.Image1Id;
            PickFile1();
        }
        private void MyPhoto2_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;
            imageId2 = myProfile.Image2Id == null ? null : myProfile.Image2Id;
            PickFile2();
        }
        private void MyPhoto3_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;
            imageId3 = myProfile.Image3Id == null ? null : myProfile.Image3Id;
            PickFile3();
        }
        private void MyPhoto4_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;

            imageId4 = myProfile.Image4Id==null?null:myProfile.Image4Id;
            PickFile4();
        }
        private void MyPhoto5_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;
            imageId5 = myProfile.Image5Id == null ? null : myProfile.Image5Id;
            PickFile5();
        }

        private void MyPrivatePhoto1_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;
            imagePrivateId1 = myProfile.PrivateImage1Id == null ? null : myProfile.PrivateImage1Id;
            PickPrivateFile1();
        }
        private void MyPrivatePhoto2_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;
            imagePrivateId2 = myProfile.PrivateImage2Id == null ? null : myProfile.PrivateImage2Id;
            PickPrivateFile2();
        }
        private void MyPrivatePhoto3_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;
            imagePrivateId3 = myProfile.PrivateImage3Id == null ? null : myProfile.PrivateImage3Id;
            PickPrivateFile3();
        }
        private void MyPrivatePhoto4_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;

            imagePrivateId4 = myProfile.PrivateImage4Id == null ? null : myProfile.PrivateImage4Id;
            PickPrivateFile4();
        }
        private void MyPrivatePhoto5_Tapped(object sender, EventArgs e)
        {
            ProfileEntity myProfile = (ProfileEntity)((TappedEventArgs)e).Parameter;
            imagePrivateId5 = myProfile.PrivateImage5Id == null ? null : myProfile.PrivateImage5Id;
            PickPrivateFile5();
        }
        async void PickFile1()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imageStream1 = stream;
                ImageToDisplayCoverImage1.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPublicNew(1, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        async void PickFile2()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imageStream2 = stream;
                ImageToDisplayCoverImage2.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPublicNew(2, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        async void PickFile3()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imageStream3 = stream;
                ImageToDisplayCoverImage3.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPublicNew(3, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        async void PickFile4()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imageStream4 = stream;
                ImageToDisplayCoverImage4.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPublicNew(4, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        async void PickFile5()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imageStream5 = stream;
                ImageToDisplayCoverImage5.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPublicNew(5, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        async void PickPrivateFile1()
        {
            FileResult result = await PickAndCrop();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imagePrivateStream1 = stream;
                ImageToDisplayPrivateImage1.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPrivateNew(1,streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }

        private static async Task<FileResult> PickAndCrop()
        {
            return await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select your picture",
                FileTypes = FilePickerFileType.Images,
            });
        }

        async void PickPrivateFile2()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imagePrivateStream2 = stream;
                ImageToDisplayPrivateImage2.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPrivateNew(2, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);

                //deletephotoFrame.IsVisible = true;
                //UploadFileToFTP((FileStream)imageStream);
                //ShowUploadConfirmationFrame.IsVisible = true;
            }
        }
        async void PickPrivateFile3()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imagePrivateStream3 = stream;
                ImageToDisplayPrivateImage3.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPrivateNew(3, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        async void PickPrivateFile4()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imagePrivateStream4 = stream;
                ImageToDisplayPrivateImage4.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPrivateNew(4, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        async void PickPrivateFile5()
        {
            FileResult result = await PickAndCrop();

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imagePrivateStream5 = stream;
                ImageToDisplayPrivateImage5.Source = ImageSource.FromStream(() => stream2);
                var nextPage = new MyProfileUploadPrivateNew(5, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        private async void Available_Tapped(object sender, EventArgs e)
        {
            MyProfileService ps =new MyProfileService();
            ProfileEdit pe = new ProfileEdit();
            pe.profileId = SwipeCardView.UsrId.ToString();
            var result = await ps.EditProfileAvailable(pe);
            if (AvailableButtonLabel.Text == "&#x1f34d Available")
            {
                DisplayAlert("Update Availability Status", "Not available to others in the Pinapple list", "ok");
                AvailableButtonLabel.Text = "Unavailable";
            }
            else
            {
                DisplayAlert("Update Availability Status", "Available to others in the Pineapple list", "ok");
                AvailableButtonLabel.Text = "&#x1f34d Available";
            }
        }

        private void CheckBox1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ImageFrame.IsVisible = false;
            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                ImageFrame.IsVisible = true;
                //UploadDeleteButtonsFrame.IsVisible = true;
            }
            else if (!CheckBox2.IsChecked && !CheckBox3.IsChecked && !CheckBox4.IsChecked && !CheckBox5.IsChecked)
            {
                ImageFrame.IsVisible = false;
                //UploadDeleteButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ImageFrame.IsVisible = false;

            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                ImageFrame.IsVisible = true;
                //UploadDeleteButtonsFrame.IsVisible = true;
            }
            else if (!CheckBox1.IsChecked && !CheckBox3.IsChecked && !CheckBox4.IsChecked && !CheckBox5.IsChecked)
            {
                ImageFrame.IsVisible = false;
                //UploadDeleteButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBox3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ImageFrame.IsVisible = false;
            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                ImageFrame.IsVisible = true;
                //UploadDeleteButtonsFrame.IsVisible = true;
            }
            else if (!CheckBox2.IsChecked && !CheckBox1.IsChecked && !CheckBox4.IsChecked && !CheckBox5.IsChecked)
            {
                ImageFrame.IsVisible = false;
                //UploadDeleteButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBox4_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ImageFrame.IsVisible = false;
            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                ImageFrame.IsVisible = true;
                //UploadDeleteButtonsFrame.IsVisible = true;
            }
            else if (!CheckBox2.IsChecked && !CheckBox3.IsChecked && !CheckBox1.IsChecked && !CheckBox5.IsChecked)
            {
                ImageFrame.IsVisible = false;
                //UploadDeleteButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBox5_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ImageFrame.IsVisible = false;
            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                ImageFrame.IsVisible = true;
                //UploadDeleteButtonsFrame.IsVisible = true;
            }
            else if (!CheckBox2.IsChecked && !CheckBox3.IsChecked && !CheckBox4.IsChecked && !CheckBox1.IsChecked)
            {
                ImageFrame.IsVisible = false;
                //UploadDeleteButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBoxPrivate1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            PrivateImageFrame.IsVisible = false;
            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                PrivateImageFrame.IsVisible = true;
                //UploadDeletePrivateButtonsFrame.IsVisible = true;
            }
            else if (!CheckBoxPrivate2.IsChecked && !CheckBoxPrivate3.IsChecked && !CheckBoxPrivate4.IsChecked && !CheckBoxPrivate5.IsChecked)
            {
                PrivateImageFrame.IsVisible = false;
                //UploadDeletePrivateButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBoxPrivate2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            PrivateImageFrame.IsVisible = false;

            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                PrivateImageFrame.IsVisible = true;
                //UploadDeletePrivateButtonsFrame.IsVisible = true;
            }
            else if (!CheckBoxPrivate1.IsChecked && !CheckBoxPrivate3.IsChecked && !CheckBoxPrivate4.IsChecked && !CheckBoxPrivate5.IsChecked)
            {
                PrivateImageFrame.IsVisible = false;
                //UploadDeletePrivateButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBoxPrivate3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            PrivateImageFrame.IsVisible = false;
            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                PrivateImageFrame.IsVisible = true;
                //UploadDeletePrivateButtonsFrame.IsVisible = true;
            }
            else if (!CheckBoxPrivate2.IsChecked && !CheckBoxPrivate1.IsChecked && !CheckBoxPrivate4.IsChecked && !CheckBoxPrivate5.IsChecked)
            {
                PrivateImageFrame.IsVisible = false;
                //UploadDeletePrivateButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBoxPrivate4_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            PrivateImageFrame.IsVisible = false;
            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                PrivateImageFrame.IsVisible = true;
                //UploadDeletePrivateButtonsFrame.IsVisible = true;
            }
            else if (!CheckBoxPrivate2.IsChecked && !CheckBoxPrivate3.IsChecked && !CheckBoxPrivate1.IsChecked && !CheckBoxPrivate5.IsChecked)
            {
                PrivateImageFrame.IsVisible = false;
                //UploadDeletePrivateButtonsFrame.IsVisible = false;
            }
        }

        private void CheckBoxPrivate5_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            PrivateImageFrame.IsVisible = false;
            CheckBox c = sender as CheckBox;
            if (c.IsChecked)
            {
                PrivateImageFrame.IsVisible = true;
                //UploadDeletePrivateButtonsFrame.IsVisible = true;
            }
            else if (!CheckBoxPrivate2.IsChecked && !CheckBoxPrivate3.IsChecked && !CheckBoxPrivate4.IsChecked && !CheckBoxPrivate1.IsChecked)
            {
                PrivateImageFrame.IsVisible = false;
                //UploadDeletePrivateButtonsFrame.IsVisible = false;
            }
        }

        private void ShowPopUpButton_Clicked(object sender, EventArgs e)
        {
            ShowPopUp();
        }

        private void ShowPopUpPrivateButton_Clicked(object sender, EventArgs e)
        {
            ShowPopUpPrivate();
        }
        private void ShowPopUp()
        {
            PopUpWaitForUpload.IsVisible = true;
            MainScroll.Opacity = 0.5;
        }
        private void ShowPopUpPrivate()
        {
            PopUpPrivateWaitForUpload.IsVisible = true;
            MainScroll.Opacity = 0.5;
        }

        private async void TapGestureRecognizer_TappedConfirmBox(object sender, EventArgs e)
        {
            FileActivityIndicator.IsRunning = true;

            if (CheckBox1.IsChecked)
            {
                DoUploads1();
            }
            if (CheckBox2.IsChecked)
            {
                DoUploads2();
            }

            if (CheckBox3.IsChecked)
            {
                DoUploads3();

            }
            if (CheckBox4.IsChecked)
            {
                DoUploads4();
            }

            if (CheckBox5.IsChecked)
            {
                DoUploads5();
            }

            ImageFrame.IsVisible = true;
            UploadDeleteButtonsFrame.IsVisible = false;
        }

        private async void TapGestureRecognizerPrivate_TappedConfirmBox(object sender, EventArgs e)
        {
            PrivateFileActivityIndicator.IsRunning = true;

            if (CheckBoxPrivate1.IsChecked)
            {
                DoPrivateUploads1();
            }
            if (CheckBoxPrivate2.IsChecked)
            {
                DoPrivateUploads2();
            }

            if (CheckBoxPrivate3.IsChecked)
            {
                DoPrivateUploads3();

            }
            if (CheckBoxPrivate4.IsChecked)
            {
                DoPrivateUploads4();
            }

            if (CheckBoxPrivate5.IsChecked)
            {
                DoPrivateUploads5();
            }

            PrivateImageFrame.IsVisible = true;
            UploadDeletePrivateButtonsFrame.IsVisible = false;
        }

        private async void DoUploads1()
        {
            UploadFileToFTP1((FileStream)imageStream1);

            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void DoUploads2()
        {
            UploadFileToFTP2((FileStream)imageStream2);

            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void DoUploads3()
        {
            UploadFileToFTP3((FileStream)imageStream3);

            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void DoUploads4()
        {
            UploadFileToFTP4((FileStream)imageStream4);

            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }

        private async void DoUploads5()
        {
            UploadFileToFTP5((FileStream)imageStream5);

            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }

        private async void DoPrivateUploads1()
        {
            UploadFileToPrivateFTP1((FileStream)imagePrivateStream1);

            PopUpPrivateWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void DoPrivateUploads2()
        {
            UploadFileToPrivateFTP2((FileStream)imagePrivateStream2);

            PopUpPrivateWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void DoPrivateUploads3()
        {
            UploadFileToPrivateFTP3((FileStream)imagePrivateStream3);

            PopUpPrivateWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void DoPrivateUploads4()
        {
            UploadFileToPrivateFTP4((FileStream)imagePrivateStream4);

            PopUpPrivateWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }

        private async void DoPrivateUploads5()
        {
            UploadFileToPrivateFTP5((FileStream)imagePrivateStream5);

            PopUpPrivateWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void UploadFileToFTP1(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imageUrl1 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToFTP2(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imageUrl2 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToFTP3(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imageUrl3 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToFTP4(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imageUrl4 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToFTP5(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imageUrl5 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToPrivateFTP1(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imagePrivateUrl1 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToPrivateFTP2(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imagePrivateUrl2 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToPrivateFTP3(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imagePrivateUrl3 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToPrivateFTP4(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imagePrivateUrl4 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadFileToPrivateFTP5(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imagePrivateUrl5 = imageUrlBase + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }


        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }

        private void TapGestureRecognizerBackPrivate_Tapped(object sender, EventArgs e)
        {
            PopUpPrivateWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }

        private async void SaveImages_Clicked(object sender, EventArgs e)
        {
            //public.event_insert(qrofileid,
            // qtarttime, qendtime,
            // qname,
            // qdescription,
            // qcategory,
            // qisvenuehidden,
            // qvenue,
            //qcoverimageurl ,
            // qemaildescription,
            // qimages -array)
            
            SwingSocial.Sample.Model.Image img = new SwingSocial.Sample.Model.Image();

            MyProfileService ps = new MyProfileService();
            if (CheckBox1.IsChecked)
            {
                img.ImageId = imageId1 == null ? "" : imageId1;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imageUrl1;
                if (imageId1 == null)
                {
                    var result = await ps.ImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.ImagesReplacePublicImage(img);
                }
            }
            if (CheckBox2.IsChecked)
            {
                img.ImageId = imageId2 == null ? "" : imageId2;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imageUrl2;
                if (imageId2 == null)
                {
                    var result = await ps.ImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.ImagesReplacePublicImage(img);
                }
            }

            if (CheckBox3.IsChecked)
            {
                img.ImageId = imageId3 == null ? "" : imageId3;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imageUrl3;
                if (imageId3 == null)
                {
                    var result = await ps.ImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.ImagesReplacePublicImage(img);
                }
            }
            if (CheckBox4.IsChecked)
            {
                img.ImageId = imageId4 == null ? "" : imageId4;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imageUrl4;
                if (imageId4 == null)
                {
                    var result = await ps.ImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.ImagesReplacePublicImage(img);
                }
            }

            if (CheckBox5.IsChecked)
            {
                img.ImageId = imageId5 == null ? "" : imageId5;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imageUrl5;
                if (imageId5 == null)
                {
                    var result = await ps.ImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.ImagesReplacePublicImage(img);
                } 
            }

            await DisplayAlert("Images Update Status", "Selected images were updated correctly.", "ok");
            //EventCreated = true;
            //var nextpage = new Community();
            //await Navigation.PushAsync(nextpage);
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;

            ImageFrame.IsVisible = false;
        }

        private async void DeleteImages_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            SwingSocial.Sample.Model.Image img = new SwingSocial.Sample.Model.Image();

            MyProfileService ps = new MyProfileService();
            if (CheckBox1.IsChecked)
            {
                img.ImageId = p.Image1Id == null ? "" : p.Image1Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.Image1Url;
                if (p.Image1Id == null)
                {
                }
                else
                {
                    var result = await ps.ImagesDeletePublicImage(img);
                }
            }
            if (CheckBox2.IsChecked)
            {
                img.ImageId = p.Image2Id == null ? "" : p.Image2Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.Image2Url;
                if (p.Image2Id == null)
                {
                }
                else
                {
                    var result = await ps.ImagesDeletePublicImage(img);
                }
            }

            if (CheckBox3.IsChecked)
            {
                img.ImageId = p.Image3Id == null ? "" : p.Image3Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.Image3Url;
                if (p.Image3Id == null)
                {
                }
                else
                {
                    var result = await ps.ImagesDeletePublicImage(img);
                }
            }
            if (CheckBox4.IsChecked)
            {
                img.ImageId = p.Image4Id == null ? "" : p.Image4Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.Image4Url;
                if (p.Image4Id == null)
                {
                }
                else
                {
                    var result = await ps.ImagesDeletePublicImage(img);
                }
            }

            if (CheckBox5.IsChecked)
            {
                img.ImageId = p.Image5Id == null ? "" : p.Image5Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.Image5Url;
                if (p.Image5Id == null)
                {
                }
                else
                {
                    var result = await ps.ImagesDeletePublicImage(img);
                }
            }

            await DisplayAlert("Images Update Status", "Selected images were updated correctly.", "ok");
            //EventCreated = true;
            //var nextpage = new Community();
            //await Navigation.PushAsync(nextpage);
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;

            ImageFrame.IsVisible = false;
        }

        private async void SavePrivateImages_Clicked(object sender, EventArgs e)
        {
            SwingSocial.Sample.Model.Image img = new SwingSocial.Sample.Model.Image();

            MyProfileService ps = new MyProfileService();
            if (CheckBoxPrivate1.IsChecked)
            {
                img.ImageId = imagePrivateId1 == null ? "" : imagePrivateId1;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imagePrivateUrl1;
                if (imagePrivateId1 == null)
                {
                    var result = await ps.PrivateImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.PrivateImagesReplacePublicImage(img);
                }
            }
            if (CheckBoxPrivate2.IsChecked)
            {
                img.ImageId = imagePrivateId2 == null ? "" : imagePrivateId2;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imagePrivateUrl2;
                if (imagePrivateId2 == null)
                {
                    var result = await ps.PrivateImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.PrivateImagesReplacePublicImage(img);
                }
            }

            if (CheckBoxPrivate3.IsChecked)
            {
                img.ImageId = imagePrivateId3 == null ? "" : imagePrivateId3;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imagePrivateUrl3;
                if (imagePrivateId3 == null)
                {
                    var result = await ps.PrivateImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.PrivateImagesReplacePublicImage(img);
                }
            }
            if (CheckBoxPrivate4.IsChecked)
            {
                img.ImageId = imagePrivateId4 == null ? "" : imagePrivateId4;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imagePrivateUrl4;
                if (imagePrivateId4 == null)
                {
                    var result = await ps.PrivateImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.PrivateImagesReplacePublicImage(img);
                }
            }

            if (CheckBoxPrivate5.IsChecked)
            {
                img.ImageId = imagePrivateId5 == null ? "" : imagePrivateId5;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = imagePrivateUrl5;
                if (imagePrivateId5 == null)
                {
                    var result = await ps.PrivateImagesInsertPublicImage(img);
                }
                else
                {
                    var result = await ps.PrivateImagesReplacePublicImage(img);
                }
            }

            await DisplayAlert("Images Update Status", "Selected images were updated correctly.", "ok");
            //EventCreated = true;
            //var nextpage = new Community();
            //await Navigation.PushAsync(nextpage);
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;
            MyProfileService mock = new MyProfileService();
            MyProfileViewModel.MyProfile = await mock.EditProfileMyProfile();
            PrivateImageFrame.IsVisible = false;
        }

        private async void DeletePrivateImages_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            ProfileEntity p = b.CommandParameter as ProfileEntity;

            SwingSocial.Sample.Model.Image img = new SwingSocial.Sample.Model.Image();

            MyProfileService ps = new MyProfileService();
            if (CheckBoxPrivate1.IsChecked)
            {
                img.ImageId = p.PrivateImage1Id == null ? "" : p.PrivateImage1Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.PrivateImage1Url;
                if (p.PrivateImage1Id == null)
                {
                }
                else
                {
                    var result = await ps.PrivateImagesDeletePublicImage(img);
                }
            }
            if (CheckBoxPrivate2.IsChecked)
            {
                img.ImageId = p.PrivateImage2Id == null ? "" : p.PrivateImage2Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.PrivateImage2Url;
                if (p.PrivateImage2Id == null)
                {
                }
                else
                {
                    var result = await ps.PrivateImagesDeletePublicImage(img);
                }
            }

            if (CheckBoxPrivate3.IsChecked)
            {
                img.ImageId = p.PrivateImage3Id == null ? "" : p.PrivateImage3Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.PrivateImage3Url;
                if (p.PrivateImage3Id == null)
                {
                }
                else
                {
                    var result = await ps.PrivateImagesDeletePublicImage(img);
                }
            }
            if (CheckBoxPrivate4.IsChecked)
            {
                img.ImageId = p.PrivateImage4Id == null ? "" : p.PrivateImage4Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.PrivateImage4Url;
                if (p.PrivateImage4Id == null)
                {
                }
                else
                {
                    var result = await ps.PrivateImagesDeletePublicImage(img);
                }
            }

            if (CheckBoxPrivate5.IsChecked)
            {
                img.ImageId = p.PrivateImage5Id == null ? "" : p.PrivateImage5Id;
                img.ProfileId = SwipeCardView.UsrId.ToString();
                img.Text = p.PrivateImage5Url;
                if (p.PrivateImage5Id == null)
                {
                }
                else
                {
                    var result = await ps.PrivateImagesDeletePublicImage(img);
                }
            }

            await DisplayAlert("Images Update Status", "Selected images were updated correctly.", "ok");
            //EventCreated = true;
            //var nextpage = new Community();
            //await Navigation.PushAsync(nextpage);
            MyProfileViewModel = new MyProfileViewModel(Navigation);
            BindingContext = MyProfileViewModel;

            PrivateImageFrame.IsVisible = false;
        }

        private async void HomeMenu_Clicked(object sender, EventArgs e)
        {
            var nextPage = new SwingerSocialPage();
            await Navigation.PushAsync(nextPage);
        }
        private async void CommunityMenu_Clicked(object sender, EventArgs e)
        {
            var nextPage = new Community();
            await Navigation.PushAsync(nextPage);
        }

        private async void Pineapple_Clicked(object sender, EventArgs e)
        {
            var nextPage = new MapPage();
            await Navigation.PushAsync(nextPage);
        }

        private async void MatchesMenu_Clicked(object sender, EventArgs e)
        {
            var nextPage = new Matches();
            await Navigation.PushAsync(nextPage);
        }
        private async void OnChatClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var currentSwipeProfile = view.FindByName<SwipeCardView>("MySwipeCardView");
            ProfileEntity profile = currentSwipeProfile.TopItem as ProfileEntity;
            var secondPage = new MessagesMenuTopPage();
            await Navigation.PushAsync(secondPage);
        }

    }
}