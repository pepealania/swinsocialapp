using Java.Lang;
using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using static Android.Provider.Telephony;

namespace SwingSocial.Sample.View
{
    public partial class EventView : ContentPage
    {
        public static Event currentEvent = new Event();
        public static string EventId;
        string currentPineapple = "pineapple.gif";
        EventPageViewModel EventPageViewModel;
        public static ProfileEntity MyProfile;
        public static ObservableCollection<TicketPackage> TicketPackages;

        public EventView(Event ev)
        {
            currentEvent = ev;
            InitializeComponent();
            EventView.EventId = ev.Id.ToString();
            EventPageViewModel = new EventPageViewModel(Navigation);
            BindingContext = EventPageViewModel;
            SetMyProfileData();
            string organizerId = ev.OrganizerId;
            if (organizerId == SwipeCardView.UsrId.ToString())
            {
                EditEvent.IsVisible = true;
            }
            else
            {
                EditEvent.IsVisible = false;
            }
            List<ExtraImage> extraImages = new List<ExtraImage>();
            foreach (var item in ev.Images)
            {
                ExtraImage ei = new ExtraImage();
                ei.ImageUrl = item;
                extraImages.Add(ei);
            }
            if (extraImages.Count > 0)
            {
                EventPageViewModel.ExtraImagesExist = true;
            }
            else
            {
                EventPageViewModel.ExtraImagesExist = false;
            }

            ExtraImagesCollection.ItemsSource = extraImages;
            
            IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-US");
            eventImage.Source = ev.CoverImageUrl;
            eventStartTime.Text = ev.StartTime.ToString("ddd, dd MMM yyy HH:mm", iFormatProvider);
            eventStartTime.TextColor = Color.White;
            eventStartTime.BackgroundColor = Color.Black;

            eventEndTime.Text = ev.EndTime.ToString("ddd, dd MMM yyy HH:mm", iFormatProvider);
            eventEndTime.TextColor = Color.White;
            eventEndTime.BackgroundColor = Color.Black;

            eventName.Text = ev.Name;
            eventName.TextColor = Color.White;
            eventName.BackgroundColor = Color.Black;
            eventsTable.Source = ev.DetailsHtmlTable;
            eventDescription.Text = ev.Description;
            pineapple.Source = currentPineapple;
            //eventImages.Children.Add(eventImage);
        }
        private async void SetMyProfileData()
        {
            MyProfileService mock = new MyProfileService();
            MyProfile = await mock.EditProfileMyProfile();
        }
        //public EventView(Event event)
        //{
        //    //InitializeComponent();
        //    //BindingContext = new TinderPageViewModel(Navigation);
        //    //pineapple.Source = currentPineapple;
        //}

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var nextPage = new Community();
            await Navigation.PushAsync(nextPage);
        }
        private void HideShowPartnerTable(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var partnerTableLabel = view.FindByName<Label>("RSVPLabel");
            var RSVPFrame = view.FindByName<Frame>("RSVPFrame");
            var RSVPCollectionHideShow = view.FindByName<ImageButton>("RSVPCollectionHideShow");

            if (RSVPFrame.IsVisible)
            {
                RSVPCollectionHideShow.Source = "circleplus";
                RSVPCollectionHideShow.HeightRequest = 50;
                RSVPFrame.IsVisible = false;
            }
            else
            {
                RSVPCollectionHideShow.Source = "circleminus";
                RSVPFrame.IsVisible = true;
                RSVPCollectionHideShow.HeightRequest = 50;

            }
        }

        private void HideShowTicketsList(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var ticketsLabel = view.FindByName<Label>("TicketsLabel");
            var ticketsListView = view.FindByName<ListView>("TicketsListView");
            var ticketListViewHideShow = view.FindByName<ImageButton>("TicketListViewHideShow");

            var ticketsCheckout = view.FindByName<Frame>("TicketsCheckout");
            if (ticketsListView.IsVisible)
            {
                ticketListViewHideShow.Source = "circleplus";
                ticketListViewHideShow.HeightRequest = 50;
                ticketsListView.IsVisible = false;
                ticketsCheckout.IsVisible = false;
            }
            else
            {
                ticketListViewHideShow.Source = "circleminus";
                ticketsListView.IsVisible = true;
                ticketListViewHideShow.HeightRequest = 50;
                ticketsCheckout.IsVisible = true;
            }
        }

        private void HideShowAttendees(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var attendeesLabel = view.FindByName<Label>("AttendeesLabel");
            var attendeeFrame = view.FindByName<Frame>("AttendeeFrame");
            var attendeesCollectionHideShow = view.FindByName<ImageButton>("AttendeesCollectionHideShow");

            var ticketsCheckout = view.FindByName<Frame>("TicketsCheckout");
            if (attendeeFrame.IsVisible)
            {
                attendeesCollectionHideShow.Source = "circleplus";
                attendeesCollectionHideShow.HeightRequest = 50;
                attendeeFrame.IsVisible = false;
            }
            else
            {
                attendeesCollectionHideShow.Source = "circleminus";
                attendeeFrame.IsVisible = true;
                attendeesCollectionHideShow.HeightRequest = 50;
            }
        }
        private void HideShowExtraImages(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var ImagesExtraLabel = view.FindByName<Label>("ImagesExtraLabel");
            var ImagesExtraFrame = view.FindByName<Frame>("ImagesExtraFrame");
            var ImagesExtraCollectionHideShow = view.FindByName<ImageButton>("ImagesExtraCollectionHideShow");

            var ticketsCheckout = view.FindByName<Frame>("TicketsCheckout");
            if (ImagesExtraFrame.IsVisible)
            {
                ImagesExtraCollectionHideShow.Source = "circleplus";
                ImagesExtraCollectionHideShow.HeightRequest = 50;
                ImagesExtraFrame.IsVisible = false;
            }
            else
            {
                ImagesExtraCollectionHideShow.Source = "circleminus";
                ImagesExtraFrame.IsVisible = true;
                ImagesExtraCollectionHideShow.HeightRequest = 50;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_RSVP(object sender, EventArgs e)
        {
            UsersMock usersMock = new UsersMock();
            usersMock.InsertEvenRsvp();
            BindingContext = new EventPageViewModel(Navigation);
            DependencyService.Get<IToast>().ShortToast("RSVP Successfully saved");
            RSVPsCollection.ItemsSource = await EventPageViewModel.ReloadRSVPs();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!double.TryParse(e.NewTextValue, out double value))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }

        private async void RSVPImage_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;

            var selectedRSVP = imageButton.BindingContext as RSVP;
            var secondPage = new ProfilesPage(selectedRSVP);
            await Navigation.PushAsync(secondPage);
        }

        private async void EditEvent_Clicked(object sender, EventArgs e)
        {

            var nextPage = new EventEdit(currentEvent);
            await Navigation.PushAsync(nextPage);
        }

        private async void SendEmailRSVPAttendees_Clicked(object sender, EventArgs e)
        {
            var nextPage = new EmailRSVPAttendeesPage();
            await Navigation.PushAsync(nextPage);  
        }
        private async void OnChatClicked(object sender, EventArgs e)
        {
            var secondPage = new MessagesMenuTopPage();
            await Navigation.PushAsync(secondPage);
        }

        private async void TicketsCheckoutClicked(object sender, EventArgs e)
        {
            TicketPackages = EventPageViewModel.TicketPackages;

            var nextPage = new PaymentBillingPage();
            await Navigation.PushAsync(nextPage);
        }
    }
}