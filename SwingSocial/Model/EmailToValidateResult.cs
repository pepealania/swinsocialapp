using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class EmailToValidateResult
    {
        public string Code { get; set; }
        public int Expired { get; set; }
    }
}
