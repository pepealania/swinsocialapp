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
    internal class ProfilesPagePineappleViewModel : BasePageViewModel
    {
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();
        private ObservableCollection<ProfileEntity> _profilesRSVP = new ObservableCollection<ProfileEntity>();

        private uint _threshold;

        public ProfilesPagePineappleViewModel(INavigation navigation,ProfileEntity p)
        {
            Navigation = navigation;

            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeProfiles(p);
            //InitializeEvents();
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

        public ObservableCollection<ProfileEntity> ProfilesPineapple
        {
            get => _profilesRSVP;
            set
            {
                _profilesRSVP = value;
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
            ProfilesPineapple.Clear();
        }

        private void OnAddItemsCommand()
        {
        }

        private async void InitializeProfiles(ProfileEntity p)
        {
            UsersMock mock = new UsersMock();
            List<ProfileEntity> profiles = await mock.LoadPineappleProfiles(p);

            foreach (var item in profiles)
            {
                ProfilesPineapple.Add(item);
            }

            // Photos are from https://unsplash.com/. Name and Age values are fictional.
            //Profiles.Add(new Profile { ProfileId = 1, Name = "Clark145", Age = 25, Gender = Gender.Male, Photo = "https://swingsocialphotos.blob.core.windows.net/images/3d6a0510-3c7c-4c28-a9b0-83990621a95e" });
            //Profiles.Add(new Profile { ProfileId = 2, Name = "Anlow", Age = 30, Gender = Gender.Female, Photo = "https://swingsocialphotos.blob.core.windows.net/images/2506a376-01c3-425e-9664-78473dbc8d9f" });
            //Profiles.Add(new Profile { ProfileId = 3, Name = "MOCOcoupleMnM", Age = 45, Gender = Gender.Male, Photo = "https://swingsocialphotos.blob.core.windows.net/images/bc339765-43a1-4acc-97c8-6cf0e8e5b3f0" });
            //Profiles.Add(new Profile { ProfileId = 4, Name = "Kellyjo", Age = 30, Gender = Gender.Female, Photo = "https://swingsocialphotos.blob.core.windows.net/images/11ecac52-55a6-40dc-b535-a38677739404.jpeg" });
            //Profiles.Add(new Profile { ProfileId = 5, Name = "2fornaughtyfun", Age = 35, Gender = Gender.Female, Photo = "https://swingsocialphotos.blob.core.windows.net/images/94a8b6ee-7c7a-4e96-a693-087cc4dc8772" });
            //Profiles.Add(new Profile { ProfileId = 1, Name = "Laura", Age = 24, Gender = Gender.Female, Photo = "p705193.jpg" });
            //Profiles.Add(new Profile { ProfileId = 2, Name = "Sophia", Age = 21, Gender = Gender.Female, Photo = "p597956.jpg" });
            //Profiles.Add(new Profile { ProfileId = 3, Name = "Anne", Age = 19, Gender = Gender.Female, Photo = "p497489.jpg" });
            //Profiles.Add(new Profile { ProfileId = 4, Name = "Yvonne ", Age = 27, Gender = Gender.Female, Photo = "p467499.jpg" });
            //Profiles.Add(new Profile { ProfileId = 5, Name = "Abby", Age = 25, Gender = Gender.Female, Photo = "p589739.jpg" });
            //Profiles.Add(new Profile { ProfileId = 6, Name = "Andressa", Age = 28, Gender = Gender.Female, Photo = "p453095.jpg" });
            //Profiles.Add(new Profile { ProfileId = 7, Name = "June", Age = 29, Gender = Gender.Female, Photo = "p503001.jpg" });
            //Profiles.Add(new Profile { ProfileId = 8, Name = "Kim", Age = 22, Gender = Gender.Female, Photo = "p627958.jpg" });
            //Profiles.Add(new Profile { ProfileId = 9, Name = "Denesha", Age = 26, Gender = Gender.Female, Photo = "p474893.jpg" });
            //Profiles.Add(new Profile { ProfileId = 10, Name = "Sasha", Age = 23, Gender = Gender.Female, Photo = "p458914.jpg" });

            //Profiles.Add(new Profile { ProfileId = 11, Name = "Austin", Age = 28, Gender = Gender.Male, Photo = "p378674.jpg" });
            //Profiles.Add(new Profile { ProfileId = 11, Name = "James", Age = 32, Gender = Gender.Male, Photo = "p398931.jpg" });
            //Profiles.Add(new Profile { ProfileId = 11, Name = "Chris", Age = 27, Gender = Gender.Male, Photo = "p401107.jpg" });
            //Profiles.Add(new Profile { ProfileId = 11, Name = "Alexander", Age = 30, Gender = Gender.Male, Photo = "p731150.jpg" });
            //Profiles.Add(new Profile { ProfileId = 11, Name = "Steve", Age = 31, Gender = Gender.Male, Photo = "p327144.jpg" });
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
    }
}