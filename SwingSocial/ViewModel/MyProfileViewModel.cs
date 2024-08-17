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
    public class MyProfileViewModel : BasePageViewModel
    {
        private ProfileEntity _myprofile = new ProfileEntity();
        private ObservableCollection<SwingStyleTagsObject> _swingstyles =new ObservableCollection<SwingStyleTagsObject>();

        private uint _threshold;

        public MyProfileViewModel(INavigation navigation)
        {
            Navigation = navigation;

            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeMyProfile();
            InitializeSwingStyles();
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
        
        public ProfileEntity MyProfile
        {
            get => _myprofile;
            set
            {
                _myprofile = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<SwingStyleTagsObject> SwingStyles
        {
            get => _swingstyles;
            set
            {
                _swingstyles = value;
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
        private async void InitializeMyProfile()
        {
            MyProfileService mock = new MyProfileService();
            MyProfile = await mock.EditProfileMyProfile();
        }

        private async void InitializeSwingStyles()
        {
            MyProfileService mock = new MyProfileService();
            var _SwingerStyles = await mock.LoadSwingStyles();
            foreach (var item in _SwingerStyles)
            {
                SwingStyles.Add(item);
            }
        }
    }
}