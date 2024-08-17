using SkiaSharp.Views.Forms;
using SkiaSharp;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SwingSocial.Sample.View
{
    public partial class MyProfileUploadProfileBannerNew : ContentPage
    {
        static String sourcefilepath = "C:\\Users\\jose.alania\\Downloads\\imagenc.jpg"; // e.g. "d:/test.docx"
        static String ftpurl = "ftp://sub.expatcallers.com/swingsocial/banners/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpusername = "clark@sub.expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru2020#"; // e.g. password
        static object imageStream;
        static string imageUrl;
        #region Cropping Variables
        private SKPath _pathToClip;
        private byte[] _bytearray;
        private SKBitmap _originalBitmap;
        private bool _pageLoaded;
        private object croppedStream;

        private SKImage pickedImage;
        private List<SKPoint> cropPoints = new List<SKPoint>();
        private SKPath cropPath;

        #endregion

        public MyProfileUploadProfileBannerNew()
        {
            InitializeComponent();
            BindingContext = new TinderPageViewModel(Navigation);
            CropImageCanvas.PaintSurface += OnCanvasViewPaintSurface;
            _originalBitmap = LoadBitmapResource(typeof(WhatshotUploadFilePage), "SwingSocial.photocamera.png");
        }
        
        async void PickFile()
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
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                var stream3 = await result.OpenReadAsync();
                var stream4 = await result.OpenReadAsync();
                imageStream = stream;

                CroppedImageView.Source = ImageSource.FromStream(() => stream2);

                using (SKManagedStream skStream = new SKManagedStream(stream3))
                {
                    _originalBitmap = SKBitmap.Decode(skStream);
                }
                CropButton.IsVisible = true;
                ManualCropView.IsVisible = true;
                pickedImage?.Dispose();
                pickedImage = null;
                pickedImage = SKImage.FromBitmap(_originalBitmap);
                var width = DeviceDisplay.MainDisplayInfo.Width;
                var height = DeviceDisplay.MainDisplayInfo.Height;

                var resizedStream = await ResizeImageAsync(stream4, (int)width, (int)width);
                pickedImage = SKImage.FromEncodedData(resizedStream);
            }

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MyProfilePage.TransactionAlive = true;
            base.OnAppearing();

            if (_pageLoaded)
            {
                return;
            }

            _pageLoaded = true;
            ManualCropView.Initialize();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MyProfilePage.TransactionAlive = false;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PickFile();
        }

        #region Cropping
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            SKBitmap sKBitmap = null;
            if (croppedStream != null)
            {
                SKBitmap croppedBitmap = new SKBitmap(30, 30);
                canvas.DrawBitmap(croppedBitmap, 0, 0);
                sKBitmap = SKBitmap.Decode((System.IO.Stream)croppedStream);
            }

            if (_pathToClip != null)
            {
                // Crop canvas before drawing image
                canvas.ClipPath(_pathToClip);
            }

            if (croppedStream == null)
            {
                canvas.DrawBitmap(_originalBitmap, info.Rect);
            }
            else
            {
                canvas.DrawBitmap(sKBitmap, 0, 0);
            }

            if (_pathToClip != null)
            {
                // Get cropped image byte array
                var snap = surface.Snapshot();
                var data = snap.Encode();
                _bytearray = data.ToArray();

                ManualCropView.IsVisible = true;
                CropButton.IsVisible = true;
            }
        }
        private static SKBitmap LoadBitmapResource(Type type, string resourceID)
        {
            Assembly assembly = type.GetTypeInfo().Assembly;

            using (System.IO.Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                return SKBitmap.Decode(stream);
            }
        }
        public async Task<MemoryStream> ResizeImageAsync(System.IO.Stream imageStream, int newWidth, int newHeight)
        {
            using (var skiaStream = new SKManagedStream(imageStream))
            {
                // Load the image
                var originalBitmap = SKBitmap.Decode(skiaStream);

                // Create a new bitmap with the desired size
                var resizedBitmap = new SKBitmap(newWidth, newHeight);

                // Resize the image
                using (var canvas = new SKCanvas(resizedBitmap))
                {
                    var paint = new SKPaint
                    {
                        FilterQuality = SKFilterQuality.High
                    };
                    canvas.DrawBitmap(originalBitmap, SKRect.Create(newWidth, newHeight), paint);
                }

                // Save the resized image to a MemoryStream
                var resizedStream = new MemoryStream();
                SKImage.FromBitmap(resizedBitmap).Encode(SKEncodedImageFormat.Jpeg, 90).SaveTo(resizedStream);
                resizedStream.Seek(0, SeekOrigin.Begin);

                // Optionally, save the resized image to file or upload
                // For demonstration, we will just return the resized stream
                return resizedStream;
            }
        }

        #endregion

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
            using var cropPath = new SKPath();
            cropPath.AddPoly(arr);

            // add a little padding all round
            var bounds = SKRectI.Ceiling(cropPath.TightBounds);
            bounds.Inflate(4, 4);

            var info = new SKImageInfo(bounds.Width, bounds.Height);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;

            canvas.Translate(-bounds.Left, -bounds.Top);
            canvas.ClipPath(cropPath);

            canvas.DrawImage(pickedImage, 0, 0);

            var image = surface.Snapshot();

            var fileStreamLocal = (FileStream)imageStream;
            var imageName = System.IO.Path.GetFileName(fileStreamLocal.Name);

            Navigation.PushAsync(new MyProfileUploadProfileBannerCrop(image, imageName, (System.IO.Stream)imageStream));
        }

        private void PickPhoto_Tapped(object sender, EventArgs e)
        {
            PickFile();

        }
    }
}