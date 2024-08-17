using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Helper;
using SwingSocial.Sample.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace SwingSocial.Sample.Services
{
    internal class EventsService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public List<Event> Events { get; set; }
        public Event Event { get; set; }
        public EventsService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<Event>> LoadUserEvents() {
            Events = new List<Event>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetAllEvents",string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Event> temp =
                        JsonSerializer.Deserialize<List<Event>>(content, serializerOptions);
                    Events = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Events.AddExtraInfo();
        }

        public async Task<List<Event>> EventsByMonth(int month, int year)
        {
            Events = new List<Event>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EventsByMonth?month="+month+"&year="+year, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Event> temp =
                        JsonSerializer.Deserialize<List<Event>>(content, serializerOptions);
                    Events = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Events.AddExtraInfo();
        }

        internal async Task<InsertNewEventResult> EventInsert(Event ev)
        {
            InsertNewEventResult result = new InsertNewEventResult();

            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EventInsert?profileId="+SwipeCardView.UsrId+ "&startTime="+HttpUtility.UrlEncode(ev.StartTimeString)+ "&endTime="+ HttpUtility.UrlEncode(ev.EndTimeString)+"&name="+ev.Name+ "&description="+ev.Description+ "&category="+ev.Category+ "&isVenueHidden="+ev.IsVenueHidden+ "&venue="+HttpUtility.UrlEncode(ev.Venue)+ "&coverImageUrl="+ ev.CoverImageUrl+ "&emailDescription="+ev.EmailDescription+ "&images="+ev.ImagesString + "&lattitude=" + ev.Lattitude + "&longitude=" + ev.Longitude, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    InsertNewEventResult temp =
                        JsonSerializer.Deserialize<InsertNewEventResult>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<EventUpdateResult> EventEditTopInformation(Event ev)
        {
            EventUpdateResult result = new EventUpdateResult();

            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EventEditTopInformation?eventId=" + ev.Id + "&start=" + HttpUtility.UrlEncode(ev.StartTimeString) + "&end=" + HttpUtility.UrlEncode(ev.EndTimeString) + "&name=" + ev.Name, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EventUpdateResult temp =
                        JsonSerializer.Deserialize<EventUpdateResult>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<EventUpdateResult> EventEditCover(Event ev)
        {
            EventUpdateResult result = new EventUpdateResult();

            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EventEditCover?eventId=" + ev.Id + "&coverImage=" + ev.CoverImageUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EventUpdateResult temp =
                        JsonSerializer.Deserialize<EventUpdateResult>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }
        internal async Task<EventUpdateResult> EventEditVenueCategory(Event ev)
        {
            EventUpdateResult result = new EventUpdateResult();

            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EventEditVenueCategory?eventId=" + ev.Id + "&venue=" + ev.Venue + "&category=" + ev.Category, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EventUpdateResult temp =
                        JsonSerializer.Deserialize<EventUpdateResult>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<EventUpdateResult> EventEditImages(Event ev)
        {
            EventUpdateResult result = new EventUpdateResult();

            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EventEditImages?eventId=" + ev.Id + "&images=" + ev.ImagesString, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EventUpdateResult temp =
                        JsonSerializer.Deserialize<EventUpdateResult>(content, serializerOptions);
                    result = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        internal async Task<Event> EventGetDetails(Event ev)
        {
            Event = new Event();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/EventGetDetails?eventId="+ev.Id.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Event temp =
                        JsonSerializer.Deserialize<Event>(content, serializerOptions);
                    Event = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Event.AddExtraInfo();
        }
    }
}
