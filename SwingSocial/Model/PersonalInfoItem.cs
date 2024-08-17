using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class PersonalInfoItem
    {
        public string PersonalInfoItemPlaceholder { get; set; }
        public bool IsNotDatePicker { get; internal set; }
        public bool IsDatePicker { get; internal set; }
        public string TextValue { get; set; }
        public string EntryName {  get; set; }
    }
}
