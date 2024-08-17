using SkiaSharp.Views.Forms;
using SkiaSharp;
using SwingSocial.Sample.Controls;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class BRegistrationPage7111 : ContentPage
    {

        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        public static string ChatId;
        
        static String urlAvatar = "http://www.expatcallers.com/Avatar/";
        static String urlBanner = "http://www.expatcallers.com/Banner/";
        static String ftpurlAvatar = "ftp://expatcallers.com/Avatar/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpurlBanner = "ftp://expatcallers.com/Banner/";
        static String ftpusername = "jose3@expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru"; // e.g. password

        static object imageStreamAvatar;
        static object imageStreamBanner;
        static string imageUrlAvatar;
        static string imageUrlBanner;
        public static List<ChatComment> ToProfileChatComments;
        CropViewModel cpviewmodel;
        private SKBitmap _originalBitmap;
        private bool _pageLoaded;
        private SKPath _pathToClip;
        private byte[] _bytearray;



        public static NewAccountViewModel NewAccountViewModel { get; set; }

        public BRegistrationPage7111()
        {
            InitializeComponent();
            cpviewmodel = new CropViewModel();//= new NewAccountViewModel(Navigation);
            BindingContext = cpviewmodel;

        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_pageLoaded)
            {
                return;
            }

            _pageLoaded = true;
            ManualCropView.Initialize();
            // Load web bitmap.
            //string url = "https://swingsocialphotos.blob.core.windows.net/images/9bde2131-8573-4550-869e-a59b96b5a584_IMG_6146.jpeg";
            //var httpClient = new HttpClient();
            //try
            //{
            //    //SKBitmap _originalBitmap;
            //    using (Stream stream = await httpClient.GetStreamAsync(url))
            //    using (MemoryStream memStream = new MemoryStream())
            //    {
            //        await stream.CopyToAsync(memStream);
            //        memStream.Seek(0, SeekOrigin.Begin);

            //        _originalBitmap = SKBitmap.Decode(memStream);
            //        CropImageCanvas.PaintSurface += OnCanvasViewPaintSurface;
            //        CropImageCanvas.InvalidateSurface();
            //    };
            //}
            //catch
            //{
            //}
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private void Avatar_Tapped(object sender, EventArgs e)
        {
            PickFileAvatar();

        }

        async void PickFileAvatar()
        {
            // Opening the File Picker - Filter with Jpeg image
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select your picture",
                FileTypes = FilePickerFileType.Images,
            });

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var streamAvatar = await result.OpenReadAsync();
                var streamAvatar2 = await result.OpenReadAsync();
                imageStreamAvatar = streamAvatar;
                _originalBitmap = SKBitmap.Decode(streamAvatar2);
                CropImageCanvas.PaintSurface += OnCanvasViewPaintSurface;
                CropImageCanvas.InvalidateSurface();
                //ImageToDisplayAvatar.Source = ImageSource.FromStream(() => streamAvatar2);

            }
            //var nextPage = new BRegistrationPage712();
            //await Navigation.PushAsync(nextPage);   
        }

        private void Continue_Tapped(object sender, EventArgs e)
        {
        }
            
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            if (_pathToClip != null)
            {
                // Crop canvas before drawing image
                canvas.ClipPath(_pathToClip);
            }

            canvas.DrawBitmap(_originalBitmap, info.Rect);

            if (_pathToClip != null)
            {
                // Get cropped image byte array
                var snap = surface.Snapshot();
                var data = snap.Encode();
                _bytearray = data.ToArray();

                ManualCropView.IsVisible = false;
                CropButton.IsVisible = false;
                DisplayCroppedButton.IsVisible = true;
            }
        }

        private void CropButton_OnClicked(object sender, EventArgs e)
        {
            _pathToClip = new SKPath();
            SKPoint[] arr = new SKPoint[]
            {
                new SKPoint((float) (ManualCropView.TopLeftCorner.X * DeviceDisplay.MainDisplayInfo.Density), (float) (ManualCropView.TopLeftCorner.Y * DeviceDisplay.MainDisplayInfo.Density)),
                new SKPoint((float) (ManualCropView.TopRightCorner.X * DeviceDisplay.MainDisplayInfo.Density), (float) (ManualCropView.TopRightCorner.Y * DeviceDisplay.MainDisplayInfo.Density)),
                new SKPoint((float) (ManualCropView.BottomRightCorner.X * DeviceDisplay.MainDisplayInfo.Density), (float) (ManualCropView.BottomRightCorner.Y * DeviceDisplay.MainDisplayInfo.Density)),
                new SKPoint((float) (ManualCropView.BottomLeftCorner.X * DeviceDisplay.MainDisplayInfo.Density), (float) (ManualCropView.BottomLeftCorner.Y * DeviceDisplay.MainDisplayInfo.Density))
            };
            _pathToClip.AddPoly(arr);

            CropImageCanvas.InvalidateSurface(); // redraw
        }

        private void DisplayCroppedButton_OnClicked(object sender, EventArgs e)
        {
            DisplayCroppedButton.IsVisible = false;
            CropImageCanvas.IsVisible = false;
            CroppedImageView.IsVisible = true;

            System.IO.Stream stream = new MemoryStream(_bytearray);
            CroppedImageView.Source = ImageSource.FromStream(() => stream);
        }

    }
}