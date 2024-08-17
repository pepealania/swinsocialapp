using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class Email
    {
        public string MessageId { get; set; }
        public string MailBoxId { get; set; }
        public string ProfileIdFrom { get; set; }
        public string ProfileTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProfileFromUsername { get; set;}
        public string ProfileToUsername { get; set; }
        public string CreatedAtString { get; internal set; }
        public string Avatar { get; set; }
        public string Image { get; set; }

    }
}
