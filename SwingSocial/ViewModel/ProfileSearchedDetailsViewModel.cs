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
    internal class ProfileSearchedDetailsViewModel : BasePageViewModel
    {
        private ObservableCollection<ProfileEntity> _profiles = new ObservableCollection<ProfileEntity>();
        private Page _page;
        private uint _threshold;

        public ProfileSearchedDetailsViewModel(INavigation navigation, ProfileEntity p,Page page)
        {
            Navigation = navigation;
            _page = page;
            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeSearchedProfiles(p);
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

        }

        private void OnAddItemsCommand()
        {
        }

        private async void InitializeSearchedProfiles(ProfileEntity p)
        {
            try
            {
                SearchsService userMock = new SearchsService();

                Profiles = new ObservableCollection<ProfileEntity>();
                ProfileEntity profile = await userMock.GetSwipescreenOneprofile(p.ProfileId.ToString());
                Profiles.Add(profile);
            }
            catch (Exception ex)
            {
                _page.DisplayAlert("DATA ERROR", "Data Error please contact support.", "ok");
                throw;
            }

        }


    }
}