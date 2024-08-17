using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{

    public class Preference
    {
        public string ProfileId { get; set; }
        public int Couples { get; set; }
        public int SingleMales { get; set; }
        public int SingleFemales { get; set; }
        public int OnlyPhotos { get; set; }
        public int BlockCouples { get; set; }
        public int BlockSingleMales { get; set; }
        public int BlockSingleFemales { get; set; }
        public string CityState { get; set; }
    }

}
