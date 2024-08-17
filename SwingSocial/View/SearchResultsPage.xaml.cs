using Android.Content.Res;
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
    public partial class SearchResultsPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static SearchPageViewModel SearchPageViewModel { get; set; }
        public static int TypeSelected;
        public SearchResultsPage(ProfileEntity p)
        {
            InitializeComponent();
            TypeSelected = p.AccountTypeInteger;
            SearchPageViewModel = new SearchPageViewModel(Navigation,p);
            BindingContext = SearchPageViewModel;
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

        private async void ProfilesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileEntity p = e.Item as ProfileEntity;
            p.AccountTypeInteger = p.AccountType=="Couple"?1:p.AccountType=="Male"?2:3;
            //1=couple/2=male/3=female
            var nextPage = new ProfileSearchedDetailsPage(p);
            await Navigation.PushAsync(nextPage);
        }
    }
}