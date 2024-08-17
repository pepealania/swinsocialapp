
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
    public class NewAccountViewModel : BasePageViewModel
    {
        private ObservableCollection<PersonalInfoItem> _personalinfoitems = new ObservableCollection<PersonalInfoItem>();
        private int _itemselected;

        private uint _threshold;

        public NewAccountViewModel(INavigation navigation)
        {
            Navigation = navigation;
            InitializePersonalInfos();
            NavigateCommand = new Command<Type>(OnNavigateCommand);
            Threshold = (uint)(App.ScreenWidth / 3);

            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
            DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);
            ClearItemsCommand = new Command(OnClearItemsCommand);
            AddItemsCommand = new Command(OnAddItemsCommand);
            ItemSelected = 1;
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
        
        public int ItemSelected
        {
            get => _itemselected;
            set
            {
                _itemselected = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<PersonalInfoItem> PersonalInfoItems
        {
            get => _personalinfoitems;
            set
            {
                _personalinfoitems = value;
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
        private async void InitializePersonalInfos()
        {
            PersonalInfoItems = new ObservableCollection<PersonalInfoItem>(){
                new PersonalInfoItem {  EntryName="EntryFirstName", PersonalInfoItemPlaceholder="First name",IsNotDatePicker=true,IsDatePicker=false},
                new PersonalInfoItem { EntryName="EntrySecondName", PersonalInfoItemPlaceholder="Last name",IsNotDatePicker=true,IsDatePicker=false},
                new PersonalInfoItem {EntryName="EntryEmail",PersonalInfoItemPlaceholder = "Email",IsNotDatePicker=true,IsDatePicker=false},
                new PersonalInfoItem {EntryName="EntryUsername",PersonalInfoItemPlaceholder = "Username", IsNotDatePicker = true, IsDatePicker = false},
                new PersonalInfoItem {EntryName="EntryGender",PersonalInfoItemPlaceholder = "Gender", IsNotDatePicker = true, IsDatePicker = false},
                new PersonalInfoItem {EntryName="EntryPhone",PersonalInfoItemPlaceholder = "Phone", IsNotDatePicker = true, IsDatePicker = false},
                //new PersonalInfoItem {PersonalInfoItemPlaceholder = "Birthdate", IsNotDatePicker = false, IsDatePicker = true},
                new PersonalInfoItem {EntryName="EntryPassword",PersonalInfoItemPlaceholder = "Password", IsNotDatePicker = true, IsDatePicker = false},
                new PersonalInfoItem {EntryName="EntryConfirmPassword",PersonalInfoItemPlaceholder = "Confirm Password", IsNotDatePicker = true, IsDatePicker = false}
            };
        }

    }
}