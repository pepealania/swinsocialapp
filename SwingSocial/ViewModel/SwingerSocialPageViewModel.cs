using Android.App;
using Android.Content;
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

namespace SwingSocial.Sample.ViewModel
{
    internal class SwingerSocialPageViewModel : BasePageViewModel
    {
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();
        private ObservableCollection<ProfileEntity> _profiles = new ObservableCollection<ProfileEntity>();
        private Page _page;
        private uint _threshold;

        public SwingerSocialPageViewModel(INavigation navigation,Page page)
        {
            Navigation = navigation;
            _page = page;
            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeProfiles();
            //InitializeEvents();
            Threshold = (uint)(App.ScreenWidth / 3);

            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
            DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);
            ClearItemsCommand = new Command(OnClearItemsCommand);
            AddItemsCommand = new Command(OnAddItemsCommand);
        }
        public ICommand NavigateCommand { get; private set; }
        private INavigation Navigation { get; set; }

        public ObservableCollection<Event> EventsAll
        {
            get => _events;
            set
            {
                _events = value;
                RaisePropertyChanged();
            }
        }
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
        public List<ProfileEntity> ProfilesList { get; set; }
        public ObservableCollection<ProfileEntity> Profiles
        {
            get => _profiles;
            set
            {
                _profiles = value;
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
            Profiles.Clear();
        }

        private void OnAddItemsCommand()
        {
        }

        private async void InitializeProfiles()
        {
            try
            {
                UsersMock mock = new UsersMock();
                List<ProfileEntity> profiles = await mock.LoadHomeUserProfiles(_page);
                foreach (var item in profiles)
                {
                        Profiles.Add(item);
                }
            }
            catch (Exception ex)
            {
                _page.DisplayAlert("Data Error", "Server not responding. Please email tech support.", "Ok");
            }
        }

        public async void InitializeEvents()
        {
            EventsService eventsService = new EventsService();
            List<Event> events = await eventsService.LoadUserEvents();
            foreach (var item in events)
            {
                EventsAll.Add(item);
            }
        }
    }
}