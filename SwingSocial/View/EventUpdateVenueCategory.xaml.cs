using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class EventUpdateVenueCategory : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        private Event currentEvent;
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        static string coverImageUrl;
        public static List<ChatComment> ToProfileChatComments;
        public static NewAccountViewModel NewAccountViewModel { get; set; }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public EventUpdateVenueCategory(Event ev)
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            currentEvent = ev;
            LocationEntry.Text = ev.Venue;
            BindingContext = NewAccountViewModel;

            CategoryPicker.ItemsSource = new[]
{
                "house-party",
                "meet-greet",
                "hotel-takeover"
            };
            if (ev.Category == "house-party")
            {
                CategoryPicker.SelectedIndex = 0;
            }
            else if (ev.Category == "meet-greet")
            {
                CategoryPicker.SelectedIndex = 1;

            }
            else if (ev.Category == "hotel-takeover")
            {
                CategoryPicker.SelectedIndex = 2;

            }

        }
        static String urlEvents = "http://www.expatcallers.com/swingsocial/events/";
        static object imageStreamCoverImage;
        static String ftpurlCoverImage = "ftp://expatcallers.com/swingsocial/events/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpusername = "jose3@expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru"; // e.g. password

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }


        private async void Saved_Tapped(object sender, EventArgs e)
        {



        }

        private async void UpdateVenueCategory_Clicked(object sender, EventArgs e)
        {
            Event ev = new Event();
            ev.Id = currentEvent.Id;
            ev.Venue = LocationEntry.Text;
            ev.Category = CategoryPicker.SelectedItem.ToString();
            EventsService eventsService = new EventsService();
            var result = await eventsService.EventEditVenueCategory(ev);
            await DisplayAlert("Update Status", "Event Venue and Category successfully updated.", "ok");

        }
    }
}