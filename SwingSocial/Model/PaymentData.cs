using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class PaymentData
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public decimal amount { get; set; }
        public string Email { get; internal set; }
        public List<TicketPackage> TicketPackages { get; set; }
    }
}
