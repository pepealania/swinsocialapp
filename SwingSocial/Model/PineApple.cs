using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace SwingSocial.Sample.Model
{
    public class PineApple
    {
        public Guid ProfileId { get; set; }

        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public string Region { get; set; }
        public Position Pos { get; set; }
        public string Label { get; set; }
    }
}
