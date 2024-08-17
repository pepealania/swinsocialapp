using System;
using Xamarin.Forms;

namespace SwingSocial.Sample.Komponents
{
    public class CalendarWeekDayHeader : TemplatedView
    {
        public DayOfWeek DayOfWeek
        {
            get;
        }
        
        public CalendarWeekDayHeader(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }
    }
}