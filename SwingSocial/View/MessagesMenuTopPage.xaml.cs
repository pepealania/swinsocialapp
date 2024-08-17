using SwingSocial.Sample.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using SwingSocial.Sample.Model;
using System.Linq;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Internals;
using SwingSocial.Sample.Services;
using System.Collections.ObjectModel;
using Android.Text.Style;
using Java.Lang;

namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesMenuTopPage : ContentPage
    {
        private const string ExpandAnimationName = "ExpandAnimation";
        private const string CollapseAnimationName = "CollapseAnimation";
        private const double SlideAnimationDuration = 0.25;
        //private const int AnimationDuration = 600;
        private const int AnimationDuration = 600;
        private const double PageScale = 0.98;
        //private const double PageTranslation = 0.35;
        private const double PageTranslation = 0.20;
        private readonly IEnumerable<Xamarin.Forms.View> _menuItemsView;
        private bool _isAnimationRun;
        private double _safeInsetsTop;
        private static string _currentPageRight="Events";
        string currentPineapple = "pineapple.gif";
        static MessagesMenuPageViewModel MessagesMenuPageViewModel { get; set; }

        public MessagesMenuTopPage()
        {
            try
            {
                InitializeComponent();
                MessagesMenuPageViewModel = new MessagesMenuPageViewModel(Navigation);

                BindingContext = MessagesMenuPageViewModel;
                pineapple.Source = currentPineapple;
                Shell.SetTabBarIsVisible(this, false);
                _currentPageRight = "Chats";
                ListTitle.Text = "Chats";
                //TopGrid.ColumnDefinitions[0].Width = 310;
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ChatService mock = new ChatService();
            List<Chat> _chats = await mock.LoadChats();
            MessagesMenuPageViewModel.Chats = new ObservableCollection<Chat>();
            foreach (var item in _chats)
            {
                MessagesMenuPageViewModel.Chats.Add(item);
            }
            mychatList.ItemsSource = MessagesMenuPageViewModel.Chats;
            if (!SentBoxPage.CommingFromSentBox && !MailPage.CommingFromWriteEmail && !EmailReviewPage.CommingFromEmailReview)
            {
                ListTitle.Text = "Chats";
                ChatStacklayoutView.IsVisible = true;
                EMailStacklayoutView.IsVisible = false;
                SentBoxButton.IsVisible = false;
                MailButtonFrame.IsVisible = true;
                ChatButtonFrame.IsVisible = false;
            }
            else
            {
                ListTitle.Text = "Inbox";
                ChatStacklayoutView.IsVisible = false;
                EMailStacklayoutView.IsVisible = true;
                SentBoxButton.IsVisible = true;
                MailPage.CommingFromWriteEmail = false;
                SentBoxPage.CommingFromSentBox = false;
                EmailReviewPage.CommingFromEmailReview = false;
                MailButtonFrame.IsVisible = false;
                ChatButtonFrame.IsVisible = true;
            }
            if (Login.notificationPage != null)
            {
                if (Login.notificationPage == "chat")
                {
                    ListTitle.Text = "Chats";
                    ChatStacklayoutView.IsVisible = true;
                    EMailStacklayoutView.IsVisible = false;
                    SentBoxButton.IsVisible = false;
                    MailButtonFrame.IsVisible = true;
                    ChatButtonFrame.IsVisible = false;
                }
                else
                {
                    ListTitle.Text = "Inbox";
                    ChatStacklayoutView.IsVisible = false;
                    EMailStacklayoutView.IsVisible = true;
                    SentBoxButton.IsVisible = true;
                    MailButtonFrame.IsVisible = false;
                    ChatButtonFrame.IsVisible = true;
                }
                //Login.notificationPage = null;
            }

        }

        private async void OnLearnClicked(object sender, EventArgs e)
        {
            //SwipeCardView.InvokeSwipe(SwipeCardDirection.Left);
            var navigationParams = new Dictionary<string, object>
            {
            };
            await Shell.Current.GoToAsync("LearnPage", true);
        }


        private async void OnChatClicked(object sender, EventArgs e)
        {
            var secondPage = new MessagesMenuTopPage();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }
        private async void mychatList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Chat chat = e.Item as Chat;
            var secondPage = new ChatPage(chat);
            await Navigation.PushAsync(secondPage);

        }

        private void SearchEmail_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            string conversationText = button.CommandParameter.ToString();
            myprofilesEmailList.ItemsSource = MessagesMenuPageViewModel.Emails.Where(x => x.Subject.Contains(conversationText));

        }
        private async void myprofilesInboxEmailList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Email currentEmail = e.Item as Email;
            var secondPage = new EmailReviewPage(currentEmail);
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

        private void OnShowChats_Tapped(object sender, EventArgs e)
        {
            ListTitle.Text = "Chats";
            ChatStacklayoutView.IsVisible = true;
            EMailStacklayoutView.IsVisible = false;
            SentBoxButton.IsVisible = false;
            MailButtonFrame.IsVisible = true;
            ChatButtonFrame.IsVisible = false;
        }
        private void OnShowEmails_Tapped(object sender, EventArgs e)
        {
            ListTitle.Text = "Inbox";
            ChatStacklayoutView.IsVisible = false;
            EMailStacklayoutView.IsVisible = true;
            SentBoxButton.IsVisible = true;
            MailButtonFrame.IsVisible = false;
            ChatButtonFrame.IsVisible = true;
        }

        private async void SentBox_Tapped(object sender, EventArgs e)
        {
            var nextPage = new SentBoxPage();
            await Navigation.PushAsync(nextPage);
        }

        private async void DeleteChat(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Chat c = b.CommandParameter as Chat;
            ChatService cs = new ChatService(); 
            var result = await cs.DeleteChat(c);
            MessagesMenuPageViewModel.Chats.Remove(c);
        }
    }
}
