using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage71 : ContentPage
    {

        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        
        static String urlAvatar = "http://sub.expatcallers.com/Avatar/";
        static String urlBanner = "http://sub.expatcallers.com/Banner/";
        static String ftpurlAvatar = "ftp://sub.expatcallers.com/Avatar/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpurlBanner = "ftp://sub.expatcallers.com/Banner/";
        static String ftpusername = "clark@sub.expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru2020#"; // e.g. password

        static object imageStreamAvatar;
        static object imageStreamBanner;
        static string imageUrlAvatar;
        static string imageUrlBanner;
        public static List<ChatComment> ToProfileChatComments;
        private byte[] _bytearray;
        private bool _pageLoaded;
        CropViewModel cpviewmodel;
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public BRegistrationPage71()
        {
            InitializeComponent();
            cpviewmodel = new CropViewModel();//= new NewAccountViewModel(Navigation);
            BindingContext = cpviewmodel;

        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (RegistrationUploadCrop.commingFromRegistrationCrop)
            {
                if (RegistrationUploadCrop._order == 1)
                {
                    ImageToDisplayAvatar.Source = RegistrationUploadCrop.currentStream;
                    //PrivateImageFrame.IsVisible = true;
                    imageUrlAvatar = RegistrationUploadCrop.imagePrivateUrl;
                    RegistrationUploadCrop._order = 0;
                }
                RegistrationUploadCrop.commingFromRegistrationCrop = false;
            }

        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private void Avatar_Tapped(object sender, EventArgs e)
        {
            PickFileAvatar();

        }

        //private void TapGestureRecognizerBanner_Tapped(object sender, EventArgs e)
        //{
        //    PickFileBanner();

        //}
        async void PickFileAvatar()
        {
            // Opening the File Picker - Filter with Jpeg image
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select your picture",
                FileTypes = FilePickerFileType.Images,
            });

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var streamAvatar = await result.OpenReadAsync();
                var streamAvatar2 = await result.OpenReadAsync();
                var streamToCrop = await result.OpenReadAsync();
                var streamToCrop2 = await result.OpenReadAsync();
                var streamToCrop3 = await result.OpenReadAsync();
                var streamToCrop4 = await result.OpenReadAsync();
                imageStreamAvatar = streamAvatar;
                ImageToDisplayAvatar.Source = ImageSource.FromStream(() => streamAvatar2);
                var nextPage = new RegistrationUploadNew(1, streamToCrop, streamToCrop2, streamToCrop3, streamToCrop4);
                await Navigation.PushAsync(nextPage);
            }
        }
        //async void PickFileBanner()
        //{
        //    var result = await FilePicker.PickAsync(new PickOptions
        //    {
        //        PickerTitle = "Select your picture",
        //        FileTypes = FilePickerFileType.Images,
        //    });

        //    // Here add the code that is being explained in the next step
        //    if (result != null)
        //    {
        //        var streamBanner = await result.OpenReadAsync();
        //        var streamBanner2 = await result.OpenReadAsync();
        //        imageStreamBanner = streamBanner;
        //        ImageToDisplayBanner.Source = ImageSource.FromStream(() => streamBanner2);
        //    }
        //}

        private async void Continue_Tapped(object sender, EventArgs e)
        {
            BRegistrationPage2.newProfile.Avatar = imageUrlAvatar;
            BRegistrationPage2.newProfile.ProfileBanner = "http://sub.expatcallers.com/swingsocial/banners/ProfileBanner.png";//imageUrlBanner;
            var nextPage = new BRegistrationPage72();
            await Navigation.PushAsync(nextPage);

            //ShowPopUp();
        }

        private async void DoUploads()
        {
            UploadAvatarFileToFTP((FileStream)imageStreamAvatar);
            //UploadBannerFileToFTP((FileStream)imageStreamBanner);
            BRegistrationPage2.newProfile.Avatar = imageUrlAvatar;
            BRegistrationPage2.newProfile.ProfileBanner = "http://sub.expatcallers.com/swingsocial/banners/ProfileBanner.png";//imageUrlBanner;
            var nextPage = new BRegistrationPage72();
            await Navigation.PushAsync(nextPage);

        }
        private async void TapGestureRecognizer_TappedConfirmBox(object sender, EventArgs e)
        {
            FileActivityIndicator.IsRunning = true;
            DoUploads();
        }

        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }


        private void ShowPopUp()
        {
            PopUpWaitForUpload.IsVisible = true;
            MainScroll.Opacity = 0.5;
        }

        private async void UploadAvatarFileToFTP(FileStream fs)
        {
            try
            {
                string ftpfullpath = ftpurlAvatar + Path.GetFileName(fs.Name);
                imageUrlAvatar = urlAvatar + Path.GetFileName(fs.Name);
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

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        //private async void UploadBannerFileToFTP(FileStream fs)
        //{
        //    try
        //    {
        //        string ftpfullpath = ftpurlBanner + Path.GetFileName(fs.Name);
        //        imageUrlBanner = urlBanner + Path.GetFileName(fs.Name);
        //        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
        //        ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

        //        ftp.KeepAlive = true;
        //        ftp.UseBinary = true;
        //        ftp.Method = WebRequestMethods.Ftp.UploadFile;

        //        byte[] buffer = new byte[fs.Length];
        //        fs.Read(buffer, 0, buffer.Length);

        //        Stream ftpstream = ftp.GetRequestStream();
        //        ftpstream.Write(buffer, 0, buffer.Length);
        //        ftpstream.Close();
        //        ftpstream.Dispose();

        //    }
        //    catch (WebException ex)
        //    {
        //        throw ex;
        //    }
        //}

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

        }


    }
}