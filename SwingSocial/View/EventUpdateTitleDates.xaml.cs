using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class EventUpdateTitleDates : ContentPage
    {
        public static ProfileEntity newProfile = new ProfileEntity();
        private Event currentEvent;
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
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
        public EventUpdateTitleDates(Event ev)
        {
            InitializeComponent();
            NewAccountViewModel = new NewAccountViewModel(Navigation);
            currentEvent = ev;
            EventName.Text = ev.Name;
            EventStartDateTime.DateTime = ev.StartTime;
            EventEndDateTime.DateTime = ev.EndTime;

            BindingContext = NewAccountViewModel;
        }

        private void MyGroupTickets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            EventName.Focus();
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
            IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-US");

            Event ev = new Event();
            ev.Id = currentEvent.Id;
            ev.Name = EventName.Text;
            ev.StartTime = EventStartDateTime.DateTime;
            ev.StartTimeString = EventStartDateTime.DateTime.ToString("ddd, dd MMM yyy HH:mm", iFormatProvider);
            ev.EndTime = EventStartDateTime.DateTime;
            ev.EndTimeString = EventEndDateTime.DateTime.ToString("ddd, dd MMM yyy HH:mm", iFormatProvider);

            EventsService eventsService = new EventsService();
            var result = await eventsService.EventEditTopInformation(ev);
            await DisplayAlert("Update Status", "Event name and/or Event schedule successfully updated.", "ok");

        }

    }
}