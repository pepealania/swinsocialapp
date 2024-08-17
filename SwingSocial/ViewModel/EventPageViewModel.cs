using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SwingSocial.Sample.ViewModel
{
    public class EventPageViewModel : BasePageViewModel
    {
        private ObservableCollection<RSVP> _rsvps = new ObservableCollection<RSVP>();
        private int _rsvpheight;
        private int _attendeesheight;
        
        private ObservableCollection<Attendee> _attendees = new ObservableCollection<Attendee>();

        private ObservableCollection<TicketPackage> _ticketPackages = new ObservableCollection<TicketPackage>();
        private bool _extraImagesExist = false;

        private bool _showTicketsSection;
        private uint _threshold;

        public EventPageViewModel(INavigation navigation)
        {
            Navigation = navigation;

            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeRSVPs();
            InitializeAttendees();
            InitializeTicketPäckages();

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

        public int RSVPHeight
        {
            get => _rsvpheight;
            set
            {
                _rsvpheight = value;
                RaisePropertyChanged();
            }
        }

        public int AttendeesHeight
        {
            get => _attendeesheight;
            set
            {
                _attendeesheight = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<RSVP> RSVPs
        {
            get => _rsvps;
            set
            {
                _rsvps = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Attendee> Attendees
        {
            get => _attendees;
            set
            {
                _attendees = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<TicketPackage> TicketPackages
        {
            get => _ticketPackages;
            set
            {
                _ticketPackages = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowTicketsSection
        {
            get => _showTicketsSection;
            set
            {
                _showTicketsSection = value;
                RaisePropertyChanged();
            }
        }
        
        public bool ExtraImagesExist
        {
            get => _extraImagesExist;
            set
            {
                _extraImagesExist = value;
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
            RSVPs.Clear();
        }

        private void OnAddItemsCommand()
        {
        }
        private async void InitializeRSVPs()
        {
            UsersMock mock = new UsersMock();
            List<RSVP> _RSVPs = await mock.LoadRSVPs();
            foreach (var item in _RSVPs)
            {
                RSVPs.Add(item);
            }
            if (RSVPs.Count>8)
            {
                RSVPHeight = 800;
            }
            else if (RSVPs.Count>2)
            {
                RSVPHeight = (RSVPs.Count /3) * 100;
            }
            else if (RSVPs.Count<=2)
            {
                RSVPHeight = 200;
            }

        }

        public async Task<ObservableCollection<RSVP>> ReloadRSVPs()
        {
            RSVPs = new ObservableCollection<RSVP>();
            UsersMock mock = new UsersMock();
            List<RSVP> _RSVPs = await mock.LoadRSVPs();
            foreach (var item in _RSVPs)
            {
                RSVPs.Add(item);
            }
            if (RSVPs.Count > 8)
            {
                RSVPHeight = 800;
            }
            else if (RSVPs.Count > 2)
            {
                RSVPHeight = (RSVPs.Count / 3) * 100;
            }
            else if (RSVPs.Count <= 2)
            {
                RSVPHeight = 200;
            }
            return RSVPs;
        }
        private async void InitializeAttendees()
        {
            UsersMock mock = new UsersMock();
            List<Attendee> _Attendees = await mock.LoadAttendees();
            foreach (var item in _Attendees)
            {
                Attendees.Add(item);
            }
            if (Attendees.Count > 8)
            {
                AttendeesHeight = 800;
            }
            else if (Attendees.Count > 2)
            {
                AttendeesHeight = (Attendees.Count / 3) * 100;
            }
            else if (Attendees.Count <= 2)
            {
                AttendeesHeight = 200;
            }

        }
        private async void InitializeTicketPäckages()
        {
            UsersMock mock = new UsersMock();
            List<TicketPackage> _TicketPackages = await mock.Load_TicketPackages();
            foreach (var item in _TicketPackages)
            {
                TicketPackages.Add(item);
            }
            if (_TicketPackages.Count==0)
            {
                ShowTicketsSection = false;
            }
            else
            {
                ShowTicketsSection = true;
            }

        }
    }
}