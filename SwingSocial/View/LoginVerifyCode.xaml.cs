using MLToolkit.Forms.SwipeCardView;
using PCLStorage;
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
    public partial class LoginVerifyCode : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static string EmailToValidate;
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
        public LoginVerifyCode(string email)
        {
            EmailToValidate = email;
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            BindingContext = NewAccountViewModel;
            int indexAtSymbol = email.IndexOf('@');
            EmailAddressSpan.Text = email.Substring(0, 3) + "..." + email.Substring(indexAtSymbol,email.Length-1-indexAtSymbol); 
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            Code1Entry.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void ContinueToSwingSocialPage_Tapped(object sender, EventArgs e)
        {
            if (Code1Entry.Text!=null && Code1Entry.Text != String.Empty)
            {
                bool isValidCode = await ValidationCodeIsValid();
                isValidCode = true;
                if (isValidCode)
                {
                    //create or write the cookie                    
                    //DependencyService.Get<IFileService>().CreateFile("1,"+SwipeCardView.UsrId+","+SwipeCardView.UsrName);

                    CreateCookie("SwingSocial", "swingsocialcookie.txt");

                    var secondPage = new SwingerSocialPage();
                    await Navigation.PushAsync(secondPage);

                }
                else
                {
                    await DisplayAlert("Login Status", "Write a valid verification code.", "ok");
                }
            }
            else
            {
                Code1Entry.Focus();
                await DisplayAlert("Login Status", "Write a valid verification code.", "ok");
            }
        }

        private async void CreateCookie(string v, string f)
        {
            IFolder folder=null;
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
            bool writeResult = await f.WriteTextAllAsync("1," + SwipeCardView.UsrId + "," + SwipeCardView.UsrName, folder);
            //else
            //{
            //    IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(v);
            //    await folder.DeleteAsync();
            //}

        }


        private async Task<bool> ValidationCodeIsValid()
        {
            string completeCode = Code1Entry.Text;
            UsersMock u = new UsersMock();
            var infoToValidate = await u.EmailReturnEmailcode(EmailToValidate);
            return completeCode==infoToValidate.Code.ToString();
        }

        private void ResendCode_Tapped(object sender, EventArgs e)
        {
            UsersMock u = new UsersMock();
            var result = u.SendEmail(EmailToValidate);

        }

    }
}