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
    public partial class EmailRSVPAttendeesPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static Email currentEmail;

        static ChatViewModel ChatViewModel { get; set; }
        public EmailRSVPAttendeesPage()
        {
            InitializeComponent();
            ChatViewModel = new ChatViewModel(Navigation);
            BindingContext = ChatViewModel;
            //MailAvatar.Source = e.Avatar;
            //MailFromUserName.Text = e.ProfileToUsername;
            //emailFromLabel.Text = e.ProfileFromUsername;
            //emailToLabel.Text = e.ProfileToUsername;
            //currentEmail = new Email();
            //currentEmail.ProfileTo = e.ProfileTo;
            ChatViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (true)
            {

            }
            //EmailCreatePage
            Email em = new Email();
            em.ProfileIdFrom = SwipeCardView.UsrId.ToString();
            em.ProfileTo = currentEmail.ProfileTo;
            em.Subject = subjectEntry.Text;
            em.Body = BodyEditor.Text;
            EmailService es = new EmailService();
            var result = await es.InsertMailMessage(em);
            SentBoxPage.CommingFromSentBox = true;
            var nextPage = new MessagesMenuTopPage();
            await Navigation.PushAsync(nextPage);

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

        private void EmailDescriptionBold_Clicked(object sender, EventArgs e)
        {
            BodyEditor.FontAttributes = FontAttributes.Bold;
        }
        private void EmailDescriptionItalic_Clicked(object sender, EventArgs e)
        {
            BodyEditor.FontAttributes = FontAttributes.Italic;
        }

    }
}