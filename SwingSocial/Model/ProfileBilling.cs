using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class ProfileBilling
    {
        public string Username { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Period { get; set; }
    }
}
