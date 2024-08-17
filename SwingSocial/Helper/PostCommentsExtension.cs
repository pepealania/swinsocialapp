using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class PostCommentsExtension
    {
        public static List<RSVP> AddExtraInfo(this List<RSVP> rsvps)
        {
            List<RSVP> rsvpsOut = new List<RSVP>();
            foreach (RSVP r in rsvps) {
                if (r.Avatar==null || r.Avatar=="" || r.Avatar.Contains("data:image/png"))
                {
                    r.Avatar = "noavatar";
                }
                rsvpsOut.Add(r);
            }

            return rsvpsOut;
        }
    }
}
