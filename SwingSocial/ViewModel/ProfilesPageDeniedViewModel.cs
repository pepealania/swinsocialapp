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
    internal class ProfilesPageDeniedViewModel : BasePageViewModel
    {
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();
        private ObservableCollection<ProfileEntity> _profilesliked = new ObservableCollection<ProfileEntity>();

        private uint _threshold;

        public ProfilesPageDeniedViewModel(INavigation navigation,ProfileEntity p)
        {
            Navigation = navigation;

            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeProfiles(p);
            Threshold = (uint)(App.ScreenWidth / 3);

            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
            DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);
            ClearItemsCommand = new Command(OnClearItemsCommand);
            AddItemsCommand = new Command(OnAddItemsCommand);
        }
        public ICommand NavigateCommand { get; private set; }
        private INavigation Navigation { get; set; }

        public ObservableCollection<Event> Events
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

        public ObservableCollection<ProfileEntity> ProfilesDenied
        {
            get => _profilesliked;
            set
            {
                _profilesliked = value;
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
            ProfilesDenied.Clear();
        }

        private void OnAddItemsCommand()
        {
        }

        private async void InitializeProfiles(ProfileEntity p)
        {
            UsersMock mock = new UsersMock();
            List<ProfileEntity> profiles = await mock.LoadDeniedProfiles(p);

            foreach (var item in profiles)
            {
                ProfilesDenied.Add(item);
            }
        }

    }
}