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
    public class LoginViewModel : BasePageViewModel
    {
        private ObservableCollection<PineApple> _pineapples = new ObservableCollection<PineApple>();

        private ObservableCollection<ProfileEntity> _pineappleprofiles = new ObservableCollection<ProfileEntity>();

        private uint _threshold;

        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;

            NavigateCommand = new Command<Type>(OnNavigateCommand);
            //InitializePineApples();
            //InitializePineAppleProfiles();
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
        
        public ObservableCollection<ProfileEntity> PineappleProfiles
        {
            get => _pineappleprofiles;
            set
            {
                _pineappleprofiles = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<PineApple> PineApples
        {
            get => _pineapples;
            set
            {
                _pineapples = value;
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
            PineApples.Clear();
        }

        private void OnAddItemsCommand()
        {
        }
        private async void InitializePineAppleProfiles()
        {
            UsersMock mock = new UsersMock();
            List<ProfileEntity> pins = await mock.LoadPineappleProfiles();
            foreach (var item in pins)
            {
                PineappleProfiles.Add(item);
            }


        }
        private async void InitializePineApples()
        {
            try
            {
                UsersMock mock = new UsersMock();
                List<PineApple> pins = await mock.LoadPineapples();
                foreach (var item in pins)
                {
                    PineApples.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });
    }
}