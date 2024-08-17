using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class EmailCreatePage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static SwingSocial.Sample.Model.Email currentEmail;
        static object imageStream;
        static String ftpurl = "ftp://expatcallers.com/swingsocial/emailattachments/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpusername = "jose3@expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru"; // e.g. password
        static string imageUrl = "http://www.expatcallers.com/swingsocial/emailattachments/";

        static ChatViewModel ChatViewModel { get; set; }
        public EmailCreatePage(SwingSocial.Sample.Model.Email e)
        {
            InitializeComponent();
            ChatViewModel = new ChatViewModel(Navigation);
            BindingContext = ChatViewModel;
            MailAvatar.Source = e.Avatar;
            MailFromUserName.Text = e.ProfileToUsername;
            emailFromLabel.Text = e.ProfileFromUsername;
            emailToLabel.Text = e.ProfileToUsername;
            currentEmail = new SwingSocial.Sample.Model.Email();
            currentEmail.ProfileTo = e.ProfileTo;
            ChatViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;
            subjectEntry.Focus();
            pineapple.Source = currentPineapple;

        }
        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
         /*   mychatList.ScrollTo(ChatViewModel.ChatComments[ChatViewModel.ChatComments.Count - 1], ScrollToPosition.End, true)*/;
            //((INotifyCollectionChanged)mychatList.ItemsSource).CollectionChanged += OnListViewCollectionChanged;
        }

        //if (ChatViewModel.ChatComments.Count>0)
        //{
        //    mychatList.ScrollTo(ChatViewModel.ChatComments[ChatViewModel.ChatComments.Count - 1], ScrollToPosition.End, true);

        //}        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //((INotifyCollectionChanged)mychatList.ItemsSource).CollectionChanged -= OnListViewCollectionChanged;
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //var myList = ((IEnumerable<ChatComment>)mychatList.ItemsSource).ToList();

            //// Must be ran on main thread or Android chokes.
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    // For some reason Android requires a small delay or the scroll never happens.
            //    await Task.Delay(50);
            //    mychatList.ScrollTo(myList.Last(), ScrollToPosition.End, false);
            //});
        }

        private async void SendEmail(object sender, EventArgs e)
        {
            //upload attached image
            UploadFileToFTP((FileStream)imageStream);
            SwingSocial.Sample.Model.Email em = new SwingSocial.Sample.Model.Email();
            em.ProfileIdFrom = SwipeCardView.UsrId.ToString();
            em.ProfileTo = currentEmail.ProfileTo;
            em.Subject = subjectEntry.Text;
            em.Body = BodyEditor.Text;
            em.Image = imageUrl;
            EmailService es = new EmailService();
            var result = await es.InsertMailMessage(em);
            SentBoxPage.CommingFromSentBox = true;
            var nextPage = new MessagesMenuTopPage();
            await Navigation.PushAsync(nextPage);

        }
        private async void UploadFileToFTP(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imageUrl = imageUrl + Path.GetFileName(fs.Name);
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
        private async void OnChatClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var secondPage = new MyChats();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

        private void myprofilesemailList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void CoverPhoto_Tapped(object sender, EventArgs e)
        {
            PickFile();
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
                ImageToDisplayCoverImage.Source = ImageSource.FromStream(() => stream2);
                AttachedImageFrame.IsVisible=true;
            }
        }
    }
}