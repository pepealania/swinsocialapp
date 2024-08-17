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
    public partial class BRegistrationPage713 : ContentPage
    {

        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        
        static String urlAvatar = "http://www.expatcallers.com/Avatar/";
        static String urlBanner = "http://www.expatcallers.com/Banner/";
        static String ftpurlAvatar = "ftp://expatcallers.com/Avatar/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpurlBanner = "ftp://expatcallers.com/Banner/";
        static String ftpusername = "jose3@expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru"; // e.g. password

        static object imageStreamAvatar;
        static object imageStreamBanner;
        static string imageUrlAvatar;
        static string imageUrlBanner;
        public static List<ChatComment> ToProfileChatComments;
        private byte[] _bytearray;
        private bool _pageLoaded;
        CropViewModel cpviewmodel;
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public BRegistrationPage713(ProfileEntity p)
        {
            InitializeComponent();
            cpviewmodel = new CropViewModel();//= new NewAccountViewModel(Navigation);
            BindingContext = cpviewmodel;
            cpviewmodel.ImageUrl = p.ProfileBanner;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private void TapGestureRecognizerAvatar_Tapped(object sender, EventArgs e)
        {
            PickFileAvatar();

        }

        private void TapGestureRecognizerBanner_Tapped(object sender, EventArgs e)
        {
            PickFileBanner();

        }
        async void PickFileAvatar()
        {
            //FileActivityIndicator.IsEnabled = true;
            //FileActivityIndicator.IsRunning = true;
            //FileActivityIndicator.IsVisible = true;
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
                imageStreamAvatar = streamAvatar;
                //FileActivityIndicator.IsEnabled = false;
                //FileActivityIndicator.IsRunning = false;
                //FileActivityIndicator.IsVisible = false;
            }
        }
        async void PickFileBanner()
        {
            //FileActivityIndicator.IsEnabled = true;
            //FileActivityIndicator.IsRunning = true;
            //FileActivityIndicator.IsVisible = true;
            // Opening the File Picker - Filter with Jpeg image
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select your picture",
                FileTypes = FilePickerFileType.Images,
            });

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var streamBanner = await result.OpenReadAsync();
                var streamBanner2 = await result.OpenReadAsync();
                imageStreamBanner = streamBanner;
                // ImageToDisplayBanner.Source = ImageSource.FromStream(() => streamBanner2);
            }
        }

        private async void Continue_Tapped(object sender, EventArgs e)
        {
            UploadAvatarFileToFTP((FileStream)imageStreamAvatar);
            UploadBannerFileToFTP((FileStream)imageStreamBanner);
            BRegistrationPage2.newProfile.Avatar = imageUrlAvatar;
            BRegistrationPage2.newProfile.ProfileBanner = imageUrlBanner;
            //UsersMock u = new UsersMock();
            //var result = await u.EditProfilePage4(NewAccountPage.newProfile);
            var nextPage = new BRegistrationPage72();
            await Navigation.PushAsync(nextPage);
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
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void UploadBannerFileToFTP(FileStream fs)
        {
            try
            {
                string ftpfullpath = ftpurlBanner + Path.GetFileName(fs.Name);
                imageUrlBanner = urlBanner + Path.GetFileName(fs.Name);
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

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

        }

        private async void SfImageEditor_ImageSaving(object sender, Syncfusion.SfImageEditor.XForms.ImageSavingEventArgs args)
        {
            UploadBannerFileToFTP((FileStream)imageStreamBanner);
            BRegistrationPage2.newProfile.ProfileBanner = imageUrlBanner;
            var nextPage = new BRegistrationPage72();
            await Navigation.PushAsync(nextPage);
        }

        private async void SfImageEditor_ImageSaved(object sender, Syncfusion.SfImageEditor.XForms.ImageSavedEventArgs args)
        {
            var location = args.Location;
            using (FileStream fileStream = new FileStream(location, FileMode.Create, FileAccess.ReadWrite))

            {
                UploadBannerFileToFTP(fileStream);
            }

            BRegistrationPage2.newProfile.ProfileBanner = imageUrlBanner;
            var nextPage = new BRegistrationPage72();
            await Navigation.PushAsync(nextPage);
        }
    }
}