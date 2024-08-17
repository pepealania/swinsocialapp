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
    public partial class MyChats : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static ChatViewModel ChatViewModel { get; set; }
        public MyChats()
        {
            InitializeComponent();
            ChatViewModel = new ChatViewModel(Navigation);
            BindingContext = ChatViewModel;
            ChatViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;

            pineapple.Source = currentPineapple;

            if (mychatList.ItemsSource != null)
            {
                var lastItem = mychatList.ItemsSource.Cast<ChatComment>().LastOrDefault();
                mychatList.ScrollTo(lastItem, ScrollToPosition.End, true);
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            ChatService chatService = new ChatService();
            Button button = (Button)sender;
            string conversationText = button.CommandParameter.ToString();
            chatService.InsertConversation("3e108ddd-ecda-409f-abd1-672d8200b34d", ToProfileId, conversationText);
            ChatViewModel = new ChatViewModel(Navigation);
            BindingContext = ChatViewModel;
            ChatViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;
        }

        private async void mychatList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Chat chat = e.Item as Chat;
            var secondPage = new ChatPage(chat);
            await Navigation.PushAsync(secondPage);

        }


    }
}