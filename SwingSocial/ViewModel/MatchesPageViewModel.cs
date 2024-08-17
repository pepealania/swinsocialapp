using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SwingSocial.Sample.ViewModel
{
    internal class MatchesPageViewModel : BasePageViewModel
    {
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();
        private ObservableCollection<ProfileEntity> _likedprofiles = new ObservableCollection<ProfileEntity>();
        private ObservableCollection<ProfileEntity> _blockedprofiles = new ObservableCollection<ProfileEntity>();
        private ObservableCollection<ProfileEntity> _maybeprofiles = new ObservableCollection<ProfileEntity>();
        private ObservableCollection<ProfileEntity> _nopedprofiles = new ObservableCollection<ProfileEntity>();        
        private ObservableCollection<ProfileEntity> _friendprofiles = new ObservableCollection<ProfileEntity>();
        private ObservableCollection<ProfileEntity> _searchedprofiles = new ObservableCollection<ProfileEntity>();
        private ObservableCollection<ProfileEntity> _onlineprofiles = new ObservableCollection<ProfileEntity>();
        
        public MatchesPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            NavigateCommand = new Command<Type>(OnNavigateCommand);
            //InitializeNopedProfiles();
            //InitializeLikedProfiles();
            /*InitializeOnlineProfiles();                        
            InitializeMaybeProfiles();
            InitializeFriendsProfiles();*/
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

        public ObservableCollection<Event> Events
        {
            get => _events;
            set {
                _events = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<ProfileEntity> LikedProfiles
        {
            get => _likedprofiles;
            set
            {
                _likedprofiles = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProfileEntity> BlockedProfiles
        {
            get => _blockedprofiles;
            set
            {
                _blockedprofiles = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProfileEntity> MaybeProfiles
        {
            get => _maybeprofiles;
            set
            {
                _maybeprofiles = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProfileEntity> NopedProfiles
        {
            get => _nopedprofiles;
            set
            {
                _nopedprofiles = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProfileEntity> FriendProfiles
        {
            get => _friendprofiles;
            set
            {
                _friendprofiles = value;
                RaisePropertyChanged();
            }
        }
        
        public ObservableCollection<ProfileEntity> SearchedProfiles
        {
            get => _searchedprofiles;
            set
            {
                _searchedprofiles = value;
                RaisePropertyChanged();
            }
        }
        
        public ObservableCollection<ProfileEntity> OnlineProfiles
        {
            get => _onlineprofiles;
            set
            {
                _onlineprofiles = value;
                RaisePropertyChanged();
            }
        }
        public async void InitializeEvents()
        {
            EventsService eventsService = new EventsService();
            List<Event> events = await eventsService.LoadUserEvents();
            foreach (var item in events)
            {
                Events.Add(item);
            }
        }

        public async void InitializeLikedProfiles()
        {
            UsersMock userMock = new UsersMock();
            List<ProfileEntity> profiles = await userMock.LoadLikedUserProfiles();
            foreach (var profile in profiles) 
            {
                LikedProfiles.Add(profile);
            }
        }
        private async void InitializeMaybeProfiles()
        {
            UsersMock userMock = new UsersMock();
            List<ProfileEntity> profiles = await userMock.LoadMaybeUserProfiles();
            foreach (var profile in profiles)
            {
                MaybeProfiles.Add(profile);
            }
        }

        private async void InitializeNopedProfiles()
        {
            UsersMock userMock = new UsersMock();
            List<ProfileEntity> profiles = await userMock.LoadNopedUserProfiles();
            foreach (var profile in profiles)
            {
                NopedProfiles.Add(profile);
            }
        }

        private async void InitializeFriendsProfiles()
        {
            UsersMock userMock = new UsersMock();
            List<ProfileEntity> profiles = await userMock.LoadFriendsUserProfiles();
            foreach (var profile in profiles)
            {
                FriendProfiles.Add(profile);
            }
        }
        
        private async void InitializeOnlineProfiles()
        {
            UsersMock userMock = new UsersMock();
            int typeSelected = SearchResultsPage.TypeSelected;
            List<ProfileEntity> profiles = await userMock.GetOnlineList();
            foreach (var profile in profiles)
            {
                OnlineProfiles.Add(profile);
            }
        }
    }
}
