using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using PCLStorage;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SwingSocial.Sample.Interface;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    [DesignTimeVisible(false)]
    public partial class CroppedExample : ContentPage
    {
        public static Guid Id;
        public static ProfileEntity MyProfile;
        public static string notificationPage = null;
        private readonly SKBitmap _originalBitmap;
        private bool _pageLoaded;
        private SKPath _pathToClip;
        private byte[] _bytearray;

        public CroppedExample()
        {
            InitializeComponent();

            try
            {
                _originalBitmap = LoadBitmapResource(typeof(CroppedExample), "SwingSocial.wallpaper.png");

            }
            catch (Exception ex)
            {

                throw;
            }           
            CropImageCanvas.PaintSurface += OnCanvasViewPaintSurface;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_pageLoaded)
            {
                return;
            }

            _pageLoaded = true;
            ManualCropView.Initialize();
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

            Stream stream = new MemoryStream(_bytearray);
            CroppedImageView.Source = ImageSource.FromStream(() => stream);
        }

        private static SKBitmap LoadBitmapResource(Type type, string resourceID)
        {
            Assembly assembly = type.GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                return SKBitmap.Decode(stream);
            }
        }

    }
}