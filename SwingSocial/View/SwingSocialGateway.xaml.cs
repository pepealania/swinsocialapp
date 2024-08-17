using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using PCLStorage;
using SwingSocial.Sample.Interface;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class SwingSocialGateway : ContentPage
    {

        public SwingSocialGateway()
        {
            InitializeComponent();
            RedirectBack();            

        }
        private async void RedirectBack()
        { 
            var nextPage = new SwingerSocialPage();
            await Navigation.PushAsync(nextPage);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();


        }

    }
}