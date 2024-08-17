using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class ChatPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        public static List<ChatComment> ToProfileChatComments;
        public static ChatViewModel ChatViewModel { get; set; }
        public ChatPage(Chat p)
        {
            ChatId = p.ChatId;
            ToProfileId = p.ToProfileId;
            InitializeComponent();
            ChatViewModel = new ChatViewModel(Navigation);
            BindingContext = ChatViewModel;
            ChatViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;
            TopImage.Source = p.Avatar;
            TopUserName.Text = p.Username;
            pineapple.Source = currentPineapple;

            if (mychatList.ItemsSource != null)
            {
                var lastItem = mychatList.ItemsSource.Cast<ChatComment>().LastOrDefault();
                mychatList.ScrollTo(lastItem, ScrollToPosition.End, true);
            }
            if (SwingerSocialPage.emojiButtonValue != null)
            {
                Button_Clicked();
                SwingerSocialPage.emojiButtonValue = null;
            }

        }
        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            mychatList.ScrollTo(ChatViewModel.ChatComments[ChatViewModel.ChatComments.Count - 1], ScrollToPosition.End, true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
         /*   mychatList.ScrollTo(ChatViewModel.ChatComments[ChatViewModel.ChatComments.Count - 1], ScrollToPosition.End, true)*/;
            if (mychatList.ItemsSource != null)
            {
                var lastItem = mychatList.ItemsSource.Cast<ChatComment>().LastOrDefault();
                mychatList.ScrollTo(lastItem, ScrollToPosition.End, true);
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

        private async void Button_Clicked()
        {
            ChatService chatService = new ChatService();//%26amp%3B%23x1F496
            string conversationText = SwingerSocialPage.emojiButtonValue.Replace("&#x", "%26amp%3B%23x");
            chatService.InsertConversation(ChatId, ToProfileId, conversationText);
            ToProfileChatComments = await chatService.GetToProfileChatComments(conversationText.Replace("%26amp%3B%23x", "&amp;#x"));
            foreach (var item in ToProfileChatComments)
            {
                ChatViewModel.ChatComments.Add(item);
            }
            ChatComment comment = new ChatComment() { IsLoggedUser = true, IsToUser = false, Conversation = conversationText.Replace("%26amp%3B%23x", "&#x") };
            ChatViewModel.ChatComments.Add(comment);

        }
        private async void SaveConversation_Clicked(object sender, EventArgs e)
        {
            ChatService chatService = new ChatService();
            Button button = (Button)sender;
            Xamarin.Forms.Entry conversation = (Xamarin.Forms.Entry)button.CommandParameter;
            string conversationText = conversation.Text.ToString();
            chatService.InsertConversation(ChatId, ToProfileId, conversationText);
            conversation.Text = String.Empty;
            //ToProfileChatComments = await chatService.GetToProfileChatComments(conversationText);
            //foreach (var item in ToProfileChatComments)
            //{
            //    ChatViewModel.ChatComments.Add(item);
            //}
            mychatList.ItemsSource = new ObservableCollection<ChatComment>();
            mychatList.ItemsSource = ChatViewModel.ChatComments;
            ChatComment comment = new ChatComment() { IsLoggedUser = true, IsToUser = false, Conversation = conversationText };
            ChatViewModel.ChatComments.Add(comment);

        }

        private async void OnChatClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var secondPage = new MessagesMenuTopPage();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (!EmojiFrame.IsVisible)
            {
                EmojiFrame.IsVisible = true;
                stkMain.HeightRequest = stkMain.Height-150;
            }
            else
            {
                EmojiFrame.IsVisible = false;
                stkMain.HeightRequest = stkMain.Height+ 150;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Label label = sender as Label;
            //ChatComment comment = new ChatComment() { IsLoggedUser = true, IsToUser = false, Conversation = label.Text };
            //ChatViewModel.ChatComments.Add(comment);
            conversationEntry.Text = conversationEntry.Text + "&amp;#x1F600";
        }

        private async void TopImage_Clicked(object sender, EventArgs e)
        {
            ProfileEntity p = new ProfileEntity();
            p.ProfileId = new Guid(ToProfileId);
            var nextPage = new ProfilesPageChat(p);
            await Navigation.PushAsync(nextPage);
        }
    }
}