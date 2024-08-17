using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class ChatCommentsExtension
    {
        public static List<ChatComment> AddExtraInfo(this List<ChatComment> conversations)
        {
            List<ChatComment> rsvpsOut = new List<ChatComment>();
            foreach (ChatComment r in conversations) {
                if (r.Conversation.Length> 40)
                {
                    r.Conversation = AddMultilinesToConversation(r.Conversation);
                }
                if (r.Conversation.Contains("&amp;#x"))
                {
                    r.Conversation = r.Conversation.Replace("&amp;#x", "&#x");
                }
                if (r.MemberIdFrom==SwipeCardView.UsrId)
                {
                    r.IsLoggedUser = true;
                    r.IsToUser = false;
                }
                else
                {
                    r.IsToUser= true;
                    r.IsLoggedUser = false;
                }
                rsvpsOut.Add(r);
            }

            return rsvpsOut;
        }

        private static string AddMultilinesToConversation(string conversation)
        {
            int numberOfLines = conversation.Length / 40;
            for (int i = 1; i <= numberOfLines; i++)
            {
                conversation = conversation.Insert(i*40,"<br>");
            }   
            return conversation;
        }
    }
}
