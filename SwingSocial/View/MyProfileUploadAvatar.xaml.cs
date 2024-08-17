using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class MyProfileUploadAvatar : ContentPage
    {
        static String sourcefilepath = "C:\\Users\\jose.alania\\Downloads\\imagenc.jpg"; // e.g. "d:/test.docx"
        static String ftpurl = "ftp://expatcallers.com/swingsocial/avatars/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpusername = "jose3@expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru"; // e.g. password
        static object imageStream;
        static string imageUrl;
        public MyProfileUploadAvatar()
        {
            InitializeComponent();
            BindingContext = new TinderPageViewModel(Navigation);
        }
        
        async void PickFile()
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
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                imageStream = stream;
                ImageToDisplay.Source = ImageSource.FromStream(() => stream2);
                deletephotoFrame.IsVisible = true;
                ImageToDisplaySection.IsVisible = true;
                ImageFrame.IsVisible = true;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PickFile();
        }


        private async void TapGestureRecognizer_PublishTapped(object sender, EventArgs e)
        {
            updateResultLabel.Text = "successfully uploaded";
            UploadFileToFTP((FileStream)imageStream);
            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.UpdateAvatar(imageUrl);
            await DisplayAlert("Update Status", "Avatar successfully updated.", "ok");
            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);
        }
        private async void UploadFileToFTP(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imageUrl = "http://www.expatcallers.com/swingsocial/avatars/" + Path.GetFileName(fs.Name);
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
    }
}