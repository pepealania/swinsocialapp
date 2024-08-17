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
    public partial class CreateChatListPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static ChatViewModel ChatViewModel { get; set; }
        public CreateChatListPage()
        {
            InitializeComponent();

            ChatViewModel = new ChatViewModel(Navigation);
            BindingContext = ChatViewModel;
            ChatViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;

            pineapple.Source = currentPineapple;

            if (myprofilesemailList.ItemsSource != null)
            {
                var lastItem = myprofilesemailList.ItemsSource.Cast<ChatComment>().LastOrDefault();
                myprofilesemailList.ScrollTo(lastItem, ScrollToPosition.End, true);
            }


        }
        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            myprofilesemailList.ScrollTo(ChatViewModel.ChatComments[ChatViewModel.ChatComments.Count - 1], ScrollToPosition.End, true);
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

        private async void OnChatClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var secondPage = new MyChats();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

        private async void myprofileschatList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Chat c = new Chat();
            Button b = sender as Button;

            ProfileEntity p = e.Item as ProfileEntity;
            ChatService cs = new ChatService();
            c = await cs.ChatCreateUserChat(p.Id);
            var nextPage = new ChatPage(c);
            await Navigation.PushAsync(nextPage);
        }

        private async void ButtonStartChat_Clicked(object sender, EventArgs e)
        {
            Chat c = new Chat();
            Button b = sender as Button;

            ProfileEntity p = b.CommandParameter as ProfileEntity;
            ChatService cs = new ChatService();
            c = await cs.ChatCreateUserChat(p.Id);
            var nextPage = new ChatPage(c);
            await Navigation.PushAsync(nextPage);
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string conversationText = button.CommandParameter.ToString();
            //myprofilesemailList.ItemsSource = ChatViewModel.Profiles.Where(x => x.UserName.Contains(conversationText));
            UsersMock mock = new UsersMock();
            List<ProfileEntity> profiles = await mock.LoadSearchedUserProfiles(conversationText);
            ChatViewModel.Profiles = new System.Collections.ObjectModel.ObservableCollection<ProfileEntity>();
            foreach (var item in profiles)
            {
                ChatViewModel.Profiles.Add(item);
            }
            myprofilesemailList.ItemsSource = ChatViewModel.Profiles;
        }


    }
}