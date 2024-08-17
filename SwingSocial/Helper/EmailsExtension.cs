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
    public static class EmailsExtension
    {
        public static List<Email> AddExtraInfo(this List<Email> emails)
        {
            List<Email> emailsOut = new List<Email>();
            foreach (Email r in emails) {
                r.CreatedAtString = r.CreatedAt.ToShortDateString();
                emailsOut.Add(r);
            }

            return emailsOut;
        }
    }
}
