using PCLStorage;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage72 : ContentPage
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
        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public BRegistrationPage72()
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override void OnAppearing()
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


        private async void Continue_Tapped(object sender, EventArgs e)
        {
            //save all
            UsersMock u = new UsersMock();
            BRegistrationPage2.newProfile.About = aboutMeEditor.Text;
            BRegistrationPage2.newProfile.Tagline = taglineEditor.Text;
            var result = await u.InsertRegistrationProfile(BRegistrationPage2.newProfile);
            //DependencyService.Get<IFileService>().CreateFile("1," + result.ProfileId.ToString() + "," + BRegistrationPage2.newProfile.UserName);

            CreateCookie("SwingSocial", "swingsocialcookie.txt", result);

            var nextPage = new BRegistrationPage8();
            await Navigation.PushAsync(nextPage);
        }

        private async void CreateCookie(string v, string f,InsertNewProfileResult r)
        {
            IFolder folder = null;
            bool result = await v.IsFolderExistAsync();
            if (!result)
            {
                folder = await v.CreateFolder();
                bool fileResult = await f.IsFileExistAsync(folder);
                if (!fileResult)
                {
                    IFile file = await f.CreateFile(folder);
                }

            }
            else
            {
                folder = await FileSystem.Current.LocalStorage.GetFolderAsync(v);
                bool fileResult = await f.IsFileExistAsync(folder);
                if (!fileResult)
                {
                    IFile file = await f.CreateFile(folder);
                }

            }

            bool writeResult = await f.WriteTextAllAsync("1," + r.ProfileId.ToString() + "," + BRegistrationPage2.newProfile.UserName, folder);

        }

        private void AboutMeDescriptionBold_Clicked(object sender, EventArgs e)
        {
            aboutMeEditor.FontAttributes = FontAttributes.Bold;
        }
        private void AboutMeDescriptionItalic_Clicked(object sender, EventArgs e)
        {
            aboutMeEditor.FontAttributes = FontAttributes.Italic;
        }
    }
}