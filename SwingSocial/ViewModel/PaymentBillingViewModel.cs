using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace SwingSocial.Sample.ViewModel
{
    public class PaymentBillingViewModel : BasePageViewModel
    {
        private ObservableCollection<ChatComment> _chatcomments = new ObservableCollection<ChatComment>();

        private ObservableCollection<Chat> _chats = new ObservableCollection<Chat>();

        private int _totalComments;

        private uint _threshold;

        public PaymentBillingViewModel(INavigation navigation)
        {
            Navigation = navigation;

            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeChatConversations();
            InitializeChats();
            Threshold = (uint)(App.ScreenWidth / 3);

            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
            DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);
            ClearItemsCommand = new Command(OnClearItemsCommand);
            AddItemsCommand = new Command(OnAddItemsCommand);
        }
        public ICommand NavigateCommand { get; private set; }
        private INavigation Navigation { get; set; }

        private async void OnNavigateCommand(Type pageType)
        {
            try
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ObservableCollection<ChatComment> ChatComments
        {
            get => _chatcomments;
            set
            {
                _chatcomments = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Chat> Chats
        {
            get => _chats;
            set
            {
                _chats = value;
                RaisePropertyChanged();
            }
        }

        public int TotalComments
        {
            get => _totalComments;
            set
            {
                _totalComments = value;
                RaisePropertyChanged();
            }
        }

        public uint Threshold
        {
            get => _threshold;
            set
            {
                _threshold = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SwipedCommand { get; }

        public ICommand DraggingCommand { get; }

        public ICommand ClearItemsCommand { get; }

        public ICommand AddItemsCommand { get; }

        private void OnSwipedCommand(SwipedCardEventArgs eventArgs)
        {
        }

        private void OnDraggingCommand(DraggingCardEventArgs eventArgs)
        {
            switch (eventArgs.Position)
            {
                case DraggingCardPosition.Start:
                    return;

                case DraggingCardPosition.UnderThreshold:
                    break;

                case DraggingCardPosition.OverThreshold:
                    break;

                case DraggingCardPosition.FinishedUnderThreshold:
                    return;

                case DraggingCardPosition.FinishedOverThreshold:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnClearItemsCommand()
        {
            ChatComments.Clear();
        }

        private void OnAddItemsCommand()
        {
        }
        
        private async void InitializeChatConversations()
        {
            ChatService mock = new ChatService();
            List<ChatComment> _chatComments = await mock.LoadChatComments();
            foreach (var item in _chatComments)
            {
                ChatComments.Add(item);
            }
        }
        private async void InitializeChats()
        {
            ChatService mock = new ChatService();
            List<Chat> _chats = await mock.LoadChats();
            foreach (var item in _chats)
            {
                Chats.Add(item);
            }
        }

    }
}