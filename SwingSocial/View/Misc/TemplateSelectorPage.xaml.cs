using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwingSocial.Sample.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View.Misc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemplateSelectorPage : ContentPage
    {
        public TemplateSelectorPage()
        {
            InitializeComponent();

            SwipeCardView.ItemsSource = new List<Gender>()
            {
                Gender.Male,
                Gender.Female,
                Gender.Male,
                Gender.Female
            };
        }
    }
}