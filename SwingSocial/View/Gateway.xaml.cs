using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using PCLStorage;
using SwingSocial.Sample.Interface;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class Gateway : ContentPage
    {
        public static Guid Id;
        public static ProfileEntity MyProfile;
        public static string notificationPage = null;

        public Gateway()
        {
            InitializeComponent();
            RedirectIfProfileAlreadyLogged("SwingSocial", "swingsocialcookie.txt");



            //DependencyService.Get<IFileService>().CreateFile("0");
            //string cookieValue = DependencyService.Get<IFileService>().ReadCookie();
            //if (cookieValue != String.Empty && cookieValue != "0")
            //{
            //    string[] cookies = cookieValue.Split(',');
            //    if (cookies[0].Equals("1"))
            //    {
            //        SwipeCardView.UsrId = new Guid(cookies[1]);
            //        SwipeCardView.UsrName = cookies[2];

            //        GoToHomeScreen();
            //    }
            //}


        }
        private async void RedirectIfProfileAlreadyLogged(string v,string f)
        {
            try
            {
                bool folderResult = await v.IsFolderExistAsync();
                if (folderResult)
                {
                    IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(v);
                    bool fileResult = await f.IsFileExistAsync(folder);
                    if (fileResult)
                    {
                        //if file exists then read 
                        string cookieValue = await f.ReadAllTextAsync(folder);

                        if (cookieValue != String.Empty && cookieValue != "0")
                        {
                            string[] cookies = cookieValue.Split(',');
                            if (cookies[0].Equals("1"))
                            {
                                SwipeCardView.UsrId = new Guid(cookies[1]);
                                SwipeCardView.UsrName = cookies[2];

                                GoToHomeScreen();
                            }
                            else
                            {
                                InitializeComponent();
                                var nextPage = new Login();
                                await Navigation.PushAsync(nextPage);
                            }
                        }
                    }
                    else
                    {
                        InitializeComponent();
                        var nextPage = new Login();
                        await Navigation.PushAsync(nextPage);
                    }

                }
                else
                {
                        InitializeComponent();
                    var nextPage = new Login();
                    await Navigation.PushAsync(nextPage);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async void GoToHomeScreen()
        {
            try
            {
                var nextPage = new SwingerSocialPage();
                await Navigation.PushAsync(nextPage);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        //
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!EventCreate.TransactionAlive && !WhatshotUploadFilePage.TransactionAlive && !BRegistrationPage1.TransactionAlive && !MyProfilePage.TransactionAlive)
            {
                if (SwingerSocialPage.notificationPage != null)
                {
                    if (SwingerSocialPage.notificationPage == "chat")
                    {
                        notificationPage = SwingerSocialPage.notificationPage;
                        var secondPage = new MessagesMenuTopPage();
                        await Navigation.PushAsync(secondPage);
                        SwingerSocialPage.notificationPage = null;
                    }
                    else if (SwingerSocialPage.notificationPage == "email")
                    {
                        notificationPage = SwingerSocialPage.notificationPage;
                        var secondPage = new MessagesMenuTopPage();
                        await Navigation.PushAsync(secondPage);
                        SwingerSocialPage.notificationPage = null;
                    }
                }
                else
                {
                    if (SwingerSocialPage.FromSwingerSocialPage)
                    {
                        SwingerSocialPage.FromSwingerSocialPage = false;
                        var nextPage = new SwingerSocialPage();
                        await Navigation.PushAsync(nextPage);
                    }
                    else
                    {
                        Login.LoginPage = new Login();
                        BindingContext = new LoginViewModel(Navigation);

                    }


                } 
            }
            else 
            {
                if (SwingerSocialPage.FromSwingerSocialPage)
                {
                    SwingerSocialPage.FromSwingerSocialPage = false;
                    RedirectIfProfileAlreadyLogged("SwingSocial", "swingsocialcookie.txt");
                    
                }                

            }


        }

        private async Task<bool> UserNameExists(string text)
        {
            UsersMock usersMock = new UsersMock();
            MyProfile = await usersMock.LoadUserProfileIdFromUserName(text);
            List<string> names = new List<string>();
            //foreach (Profile profile in profiles) 
            //{
            if (MyProfile.Username == text)
            {
                SwipeCardView.UsrId = MyProfile.Id;
                SwipeCardView.UsrName = MyProfile.Username;
                //sendemail
                UsersMock u = new UsersMock();
                var result = u.SendEmail(MyProfile.Email);

                return true;
            }
            //}
            return false;
        }

        private async void StartFree_Tapped(object sender, EventArgs e)
        {
            //create or write the cookie
            //DependencyService.Get<IFileService>().CreateFile("0");
            //delete cookie
            DeleteCookie("SwingSocial");
            var nextPage = new BRegistrationPage1();
            await Navigation.PushAsync(nextPage);
        }

        private async void DeleteCookie(string v)
        {
            bool result = await FileInternalStorageService.IsFolderExistAsync(v);
            if (result)
            {
                IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(v);
                await folder.DeleteAsync();
            }
        }
    }
}