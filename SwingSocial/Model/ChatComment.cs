using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class ChatComment
    {
        public string ChatId { get; set; }
        public string ConversationId { get; set; }
        public string Conversation { get; set; }
        public Guid MemberIdFrom { get; set; }
        public Guid MemberIdTo { get; set; }
        public string FromUsername { get; set; }
        public string ToUsername { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsLoggedUser { get; internal set; }
        public bool IsToUser { get; internal set; }
        public string AvatarTo { get; set; }
        public string AvatarFrom { get; set; }

    }

}
