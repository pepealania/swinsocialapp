using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
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
    public partial class EventUpdateExtraImage : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        private Event currentEvent;
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        static string coverImageUrl;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public EventUpdateExtraImage(Event ev)
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            currentEvent = ev;

            BindingContext = NewAccountViewModel;
        }
        static String urlEvents = "http://www.expatcallers.com/swingsocial/events/";
        static object imageStreamCoverImage;
        static String ftpurlCoverImage = "ftp://expatcallers.com/swingsocial/events/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpusername = "jose3@expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru"; // e.g. password

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override async void OnAppearing()
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


        private async void Saved_Tapped(object sender, EventArgs e)
        {

            UploadCoverImageToFTP((FileStream)imageStreamCoverImage);           

            IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-US");

            Event ev = new Event();
            ev.Id = currentEvent.Id;
            ev.ImagesString = "''" + coverImageUrl + "''";
            EventsService eventsService = new EventsService();
            var result = await eventsService.EventEditImages(ev);
            await DisplayAlert("Update Status", "Event Cover Image successfully updated.", "ok");

        }
        private async void UploadCoverImageToFTP(FileStream fs)
        {
            try
            {
                string ftpfullpath = ftpurlCoverImage + Path.GetFileName(fs.Name);
                coverImageUrl = urlEvents + Path.GetFileName(fs.Name);
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

        private void CoverImage_Tapped(object sender, EventArgs e)
        {
            PickCoverImage();
        }
        async void PickCoverImage()
        {
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
                imageStreamCoverImage = streamAvatar;
                ImageToDisplayAvatar.Source = ImageSource.FromStream(() => streamAvatar2);

            }
        }

    }
}