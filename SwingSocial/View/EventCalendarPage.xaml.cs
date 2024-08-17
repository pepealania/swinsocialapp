using Android.Locations;
using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using PCLStorage;
using SwingSocial.Sample.Komponents;
using SwingSocial.Sample.Interface;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class EventCalendarPage : ContentPage
    {
        public static Guid Id;
        public static ProfileEntity MyProfile;
        public static string notificationPage = null;
        AdvancedPageViewModel AdvancedPageViewModel;
        public static bool IsCommingFromCalendar;
        public static int month;
        public static int year;
        public EventCalendarPage()
        {
            InitializeComponent();

            PickerFirstDayOfWeek.ItemsSource = Enum.GetValues(typeof(DayOfWeek));
            PickerFirstDayOfWeek.SelectedItem = Calendar.FirstDayOfWeek;

            PickerSelectionMode.ItemsSource = Enum.GetValues(typeof(CalendarSelectionMode));
            PickerSelectionMode.SelectedItem = Calendar.SelectionMode;

            Calendar.Date = DateTime.Now;
        }
        private void Calendar_OnDayUpdated(object sender, CalendarDayAddedEventArgs e)
        {
            e.CalendarDay.ControlTemplate = Resources["CalendarDayControlTemplate"] as ControlTemplate;

            if (e.CalendarDay.Date.DayOfWeek == DayOfWeek.Saturday ||
                e.CalendarDay.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.CalendarDay.ControlTemplate = Resources["CalendarDayControlTemplateWeekend"] as ControlTemplate;
            }

            if (e.CalendarDay.Date.Day == 3 ||
                e.CalendarDay.Date.Day == 10)
            {
                e.CalendarDay.IsSelectable = false;
            }
            else
            {
                e.CalendarDay.IsSelectable = true;
            }

            if (e.CalendarDay.Date.Day == 5 ||
                e.CalendarDay.Date.Day == 16)
            {
                var day = e.CalendarDay.BindingContext as Day;

                if (day == null)
                {
                    day = new Day
                    {
                        HasAppointments = true
                    };

                    e.CalendarDay.BindingContext = day;
                }

                day.HasAppointments = true;
            }
            else
            {
                var day = e.CalendarDay.BindingContext as Day;

                if (day != null)
                {
                    day.HasAppointments = false;
                }
            }

            if (e.CalendarDay.Date.Month != Calendar.Date.Month)
            {
                e.CalendarDay.IsSelectable = false;
                e.CalendarDay.ControlTemplate =
                    Resources["CalendarDayFromOtherMonthControlTemplate"] as ControlTemplate;
            }
        }

        private void ButtonPreviousMonth_OnClicked(object sender, EventArgs e)
        {
            Calendar.Date = Calendar.Date.AddMonths(-1);
        }

        private void ButtonNextMonth_OnClicked(object sender, EventArgs e)
        {
            Calendar.Date = Calendar.Date.AddMonths(1);
        }

        private async void Calendar_OnDayTapped(object sender, CalendarDayTappedEventArgs e)
        {
            //await DisplayAlert(title: "", message: "You clicked on: " + e.CalendarDay.Date, cancel: "ok");

            EventCalendarPage.IsCommingFromCalendar= true;
            EventCalendarPage.month = e.CalendarDay.Date.Month;
            EventCalendarPage.year = e.CalendarDay.Date.Year;
            var nextPage = new Community();
            await Navigation.PushAsync(nextPage);

        }

        private void CheckBoxShowWeekends_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (CheckBoxShowWeekends.IsChecked)
            {
                PickerFirstDayOfWeek.ItemsSource = Enum.GetValues(typeof(DayOfWeek));
            }
            else
            {
                var values = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();
                values.Remove(DayOfWeek.Saturday);
                values.Remove(DayOfWeek.Sunday);

                PickerFirstDayOfWeek.ItemsSource = values;
            }

            PickerFirstDayOfWeek.SelectedItem = Calendar.FirstDayOfWeek;
        }
    }

    public class Day : INotifyPropertyChanged
    {
        private bool _hasAppointments;

        public bool HasAppointments
        {
            get => _hasAppointments;
            set
            {
                _hasAppointments = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}