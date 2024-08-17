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
    public partial class EmailSentReviewPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static ChatViewModel ChatViewModel { get; set; }
        static Email CurrentEmail { get; set; } 
        public EmailSentReviewPage(Email email)
        {
            InitializeComponent();
            MailAvatar.Source = email.Avatar;
            MailFromUserName.Text = email.ProfileFromUsername;
            subjectEntry.Text = email.Subject;
            emailFromData.Text = email.ProfileFromUsername;
            emailToData.Text = email.ProfileToUsername;
            emailBody.Text = email.Body;
            CreatedAtLabel.Text = email.CreatedAtString;
            CurrentEmail = email;
            ImageToDisplayCoverImage.Source = email.Image;

            ChatViewModel = new ChatViewModel(Navigation);
            BindingContext = ChatViewModel;
            ChatViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;

            pineapple.Source = currentPineapple;
            if (email.Subject.Equals("Friend Request"))
            {
                YesFriendsFrame.IsVisible = true;
            }
            else if(email.Subject.Equals("Access to Private Images Requested"))
            {
                RequestPrivateAccessFrame.IsVisible = true;
            }
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

        private async void ReplyButton_Clicked(object sender, EventArgs e)
        {
            var secondPage = new EmailReplyPage(CurrentEmail);
            await Navigation.PushAsync(secondPage);
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

        private async void YesFriends_Clicked(object sender, EventArgs e)
        {
            EmailService es = new EmailService();
            var result = es.InsertFriend(CurrentEmail);
            var nextPage = new SentBoxPage();
            await Navigation.PushAsync(nextPage);
        }
        //
        private async void RequestPrivateAccess_Clicked(object sender, EventArgs e)
        {
            //EmailService es = new EmailService();
            //var result = es.InsertFriend(CurrentEmail);
            var nextPage = new SentBoxPage();
            await Navigation.PushAsync(nextPage);
        }
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            EmailService es = new EmailService();
            var result = es.DeleteMailMessage(CurrentEmail);
            var nextPage = new SentBoxPage();
            await Navigation.PushAsync(nextPage);
            //MailDeleteMessage
        }
    }
}