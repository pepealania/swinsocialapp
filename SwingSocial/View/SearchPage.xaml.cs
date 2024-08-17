using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class SearchPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static PaymentBillingViewModel PaymentBillingViewModel { get; set; }
        public SearchPage()
        {
            InitializeComponent();
            PaymentBillingViewModel = new PaymentBillingViewModel(Navigation);
            BindingContext = PaymentBillingViewModel;
            typesPicker.ItemsSource = new[]
{
                "Couples",
                "Single Males",
                "Single Females"
            };
            foreach (var item in typesPicker.Items)
            {
                item.FontSize = 12;
            }
            typesPicker.SelectedIndex = 0;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            var selectedType = typesPicker.SelectedIndex;
            ProfileEntity p = new ProfileEntity();
            var nextPage = new SearchResultsPage(p); 
            await Navigation.PushAsync(nextPage);
        }


    }
}