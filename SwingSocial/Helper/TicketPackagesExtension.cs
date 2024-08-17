using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class TicketPackagesExtension
    {
        public static List<TicketPackage> AddExtraInfo(this List<TicketPackage> ticketPackages)
        {
            List<TicketPackage> ticketPackagesOut = new List<TicketPackage>();
            foreach (TicketPackage t in ticketPackages) {
                t.TypeUpperCase = t.Type.ToUpper();
                t.TicketsPurchased = 0;
                ticketPackagesOut.Add(t);
            }

            return ticketPackagesOut;
        }
    }
}
