using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{

    public class TicketPackage
    {
        public string TicketPackageId { get; set; }
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string EventId { get; set; }
        public string TypeUpperCase { get; internal set; }
        public int TicketsPurchased { get; internal set; }
    }

}
