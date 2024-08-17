using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class SentBoxPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static EmailViewModel EmailViewModel { get; set; }
        public static bool CommingFromSentBox {  get; set; }
        public SentBoxPage()
        {
            InitializeComponent();

            EmailViewModel = new EmailViewModel(Navigation);
            BindingContext = EmailViewModel;
            EmailViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;

            pineapple.Source = currentPineapple;

            if (myprofilesemailList.ItemsSource != null)
            {
                var lastItem = myprofilesemailList.ItemsSource.Cast<ChatComment>().LastOrDefault();
                myprofilesemailList.ScrollTo(lastItem, ScrollToPosition.End, true);
            }


        }
        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            myprofilesemailList.ScrollTo(EmailViewModel.ChatComments[EmailViewModel.ChatComments.Count - 1], ScrollToPosition.End, true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
         /*   mychatList.ScrollTo(ChatViewModel.ChatComments[ChatViewModel.ChatComments.Count - 1], ScrollToPosition.End, true)*/;
            if (myprofilesemailList.ItemsSource != null)
            {
                var lastItem = myprofilesemailList.ItemsSource.Cast<ChatComment>().LastOrDefault();
                myprofilesemailList.ScrollTo(lastItem, ScrollToPosition.End, true);
            }

            //((INotifyCollectionChanged)mychatList.ItemsSource).CollectionChanged += OnListViewCollectionChanged;
        }

        //if (ChatViewModel.ChatComments.Count>0)
        //{
        //    mychatList.ScrollTo(ChatViewModel.ChatComments[ChatViewModel.ChatComments.Count - 1], ScrollToPosition.End, true);

        //}        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CommingFromSentBox = true;
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

        private void Search_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string conversationText = button.CommandParameter.ToString();
            myprofilesemailList.ItemsSource = EmailViewModel.EmailsSent.Where(x => x.Subject.Contains(conversationText));
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

        private async void ButtonStartMail_Clicked(object sender, EventArgs e)
        {
            Email email = new Email();
            Button b = sender as Button;

            ProfileEntity p = b.CommandParameter as ProfileEntity;
            email.Avatar = p.Avatar;
            email.ProfileIdFrom = SwipeCardView.UsrId.ToString();
            email.ProfileFromUsername = SwipeCardView.UsrName;
            email.ProfileTo = p.Id.ToString();
            email.ProfileToUsername = p.UserName;
            var nextPage = new EmailCreatePage(email);
            await Navigation.PushAsync(nextPage);
        }

        private async void Inbox_Tapped(object sender, EventArgs e)
        {
            CommingFromSentBox = true;
            var nextPage = new MessagesMenuTopPage();
            await Navigation.PushAsync(nextPage);
        }

        private async void myprofilesSentEmailList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Email currentEmail = e.Item as Email;
            CommingFromSentBox = true;
            var secondPage = new EmailSentReviewPage(currentEmail);
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }
    }
}