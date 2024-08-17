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
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class ChatGroupPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static ChatViewModel ChatViewModel { get; set; }
        public ChatGroupPage()
        {
            InitializeComponent();
            ChatViewModel = new ChatViewModel(Navigation);
            BindingContext = ChatViewModel;
            ChatViewModel.Profiles.CollectionChanged += MyGroupTickets_CollectionChanged;

            pineapple.Source = currentPineapple;

            if (myprofileschatList.ItemsSource != null)
            {
                var lastItem = myprofileschatList.ItemsSource.Cast<ChatComment>().LastOrDefault();
                myprofileschatList.ScrollTo(lastItem, ScrollToPosition.End, true);
            }


        }
        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            myprofileschatList.ScrollTo(ChatViewModel.Profiles[ChatViewModel.Profiles.Count - 1], ScrollToPosition.End, true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
         /*   mychatList.ScrollTo(ChatViewModel.ChatComments[ChatViewModel.ChatComments.Count - 1], ScrollToPosition.End, true)*/;
            if (myprofileschatList.ItemsSource != null)
            {
                var lastItem = myprofileschatList.ItemsSource.Cast<ProfileEntity>().LastOrDefault();
                myprofileschatList.ScrollTo(lastItem, ScrollToPosition.End, true);
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
            Button button = (Button)sender;
            string conversationText = button.CommandParameter.ToString();
            myprofileschatList.ItemsSource = ChatViewModel.Profiles.Where(x=>x.UserName.Contains(conversationText));
        }

        private async void OnChatClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var secondPage = new MyChats();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

        private void myprofileschatList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}