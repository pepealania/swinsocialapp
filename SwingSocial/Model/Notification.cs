using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class Notification
    {
        public string ProfileId { get; set; }
        public string NotificationType { get; set; }
        public int Notify { get; set; }
        public string Message { get; set; }

    }
}
