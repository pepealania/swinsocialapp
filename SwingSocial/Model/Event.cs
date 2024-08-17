using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class Event
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string CoverImageUrl { get; set; }
        public string[] Images { get; set; }
        public string ImagesString { get; set; }
        public WebViewSource DetailsHtmlTable { get; internal set; }
        public string Venue { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<TicketPackage> RSVPs { get; set; }
        public string OrganizerId { get; set; }
        public string UserName { get; set; }
        public int IsVenueHidden { get; internal set; }
        public string EmailDescription { get; internal set; }
        public string StartTimeString { get; internal set; }
        public string EndTimeString { get; internal set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
    }
}
