using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SwingSocial.Sample.ViewModel
{
    public class EmailViewModel : BasePageViewModel
    {
        private ObservableCollection<ChatComment> _chatcomments = new ObservableCollection<ChatComment>();
        
        private ObservableCollection<Email> _emailssent = new ObservableCollection<Email>();

        private ObservableCollection<Email> _emails = new ObservableCollection<Email>();
        private ObservableCollection<SwingSocial.Sample.Model.ProfileEntity> _profiles = new ObservableCollection<SwingSocial.Sample.Model.ProfileEntity>();

        private int _totalComments;

        private uint _threshold;

        public EmailViewModel(INavigation navigation)
        {
            Navigation = navigation;

            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeInbox();
            InitializeSentbox();
            InitializeProfiles();

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

        public ObservableCollection<Email> Emails
        {
            get => _emails;
            set
            {
                _emails = value;
                RaisePropertyChanged();
            }
        }
        
        public ObservableCollection<Email> EmailsSent
        {
            get => _emailssent;
            set
            {
                _emailssent = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<SwingSocial.Sample.Model.ProfileEntity> Profiles
        {
            get => _profiles;
            set
            {
                _profiles = value;
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

        
        private async void InitializeSentbox()
        {
            EmailService mock = new EmailService();
            List<Email> _emails = await mock.LoadSentboxEmails();
            foreach (var item in _emails)
            {
                EmailsSent.Add(item);
            }
        }
        private async void InitializeInbox()
        {
            EmailService mock = new EmailService();
            List<Email> _emails = await mock.LoadImboxEmails();
            foreach (var item in _emails)
            {
                Emails.Add(item);
            }
        }
        private async void InitializeProfiles()
        {
            UsersMock mock = new UsersMock();
            List<SwingSocial.Sample.Model.ProfileEntity> profiles = await mock.LoadUserProfiles();

            foreach (var item in profiles)
            {
                Profiles.Add(item);
            }

        }
    }
}