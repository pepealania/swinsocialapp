using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class Chat
    {
        public string ChatId { get; set; }
        public string ToProfileId { get; set; }
        public string Username { get; set; }
        public string LoggedInProfileId { get; set; }
        public string Avatar { get; set; }
        public int Updated { get; set; }
        public string LastUp { get; set; }

        public string Conversation { get; set; }

    }
}
