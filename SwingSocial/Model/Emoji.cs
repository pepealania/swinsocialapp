using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class Emoji
    {
        public string Code { get; set; }
        public string Hex { get; set; }
        public string Description { get; set; }
        public string MostUsed { get; set; }
        public string HexAmp { get; internal set; }
    }
}
