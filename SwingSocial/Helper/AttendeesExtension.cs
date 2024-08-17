using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class AttendeesExtension
    {
        public static List<Attendee> AddExtraInfo(this List<Attendee> rsvps)
        {
            List<Attendee> attendeesOut = new List<Attendee>();
            foreach (Attendee r in rsvps) {
                if (r.Avatar==null || r.Avatar=="" || r.Avatar.Contains("data:image/png"))
                {
                    r.Avatar = "noavatar";
                }
                attendeesOut.Add(r);
            }

            return attendeesOut;
        }
    }
}
