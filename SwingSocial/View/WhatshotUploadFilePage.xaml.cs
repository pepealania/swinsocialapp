                                                       using Android.Content.Res;
using Android.Graphics;
using Android.Hardware.Lights;
using Android.Media;
using Java.Util.Streams;
using Plugin.FilePicker;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SwingSocial.Model.Factory;
using SwingSocial.Sample.Controls;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class WhatshotUploadFilePage : ContentPage
    {
        static String sourcefilepath = "C:\\Users\\jose.alania\\Downloads\\imagenc.jpg"; // e.g. "d:/test.docx"
        static String ftpurl = "ftp://sub.expatcallers.com/swingsocial/wha/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpusername = "clark@sub.expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru2020#"; // e.g. password
        static object imageStream;
        static string imageUrl;
        internal static bool TransactionAlive;
        private SKPath _pathToClip;
        private byte[] _bytearray;
        private SKBitmap _originalBitmap;
        private bool _pageLoaded;
        private object croppedStream;

        private SKImage pickedImage;
        private List<SKPoint> cropPoints = new List<SKPoint>();
        private SKPath cropPath;

        public WhatshotUploadFilePage(WhatsHot whatshot)
        {
            InitializeComponent();
            var MyViewModelFactory = new TinderPageViewModelFactory();
            BindingContext = MyViewModelFactory.FactoryMethod(Navigation);
            CropImageCanvas.PaintSurface += OnCanvasViewPaintSurface;
            _originalBitmap = LoadBitmapResource(typeof(WhatshotUploadFilePage), "SwingSocial.photocamera.png");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            TransactionAlive = true;
            SwingerSocialPage.FromSwingerSocialPage = false;
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
            TransactionAlive = false;
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

                //UploadImageView.Source = ImageSource.FromStream(() => stream2);
                CroppedImageView.Source = ImageSource.FromStream(() => stream2);

                //_originalBitmap = LoadBitmapResource(typeof(WhatshotUploadFilePage), "SwingSocial.wallpaper.png");
                using (SKManagedStream skStream = new SKManagedStream(stream3))
                {
                    _originalBitmap = SKBitmap.Decode(skStream);
                }
                //deletephotoFrame.IsVisible = false;
                //ImageToDisplaySection.IsVisible = false;
                // ShowUploadConfirmationFrame.IsVisible = true;
                CropButton.IsVisible = true;
                ManualCropView.IsVisible = true;
                pickedImage?.Dispose();
                pickedImage = null;
                pickedImage = SKImage.FromBitmap(_originalBitmap);
                //var picked = await CrossFilePicker.Current.PickFile();

                //using (var fileStream = picked.DataArray.ToMemoryStream())
                //{
                    var width = DeviceDisplay.MainDisplayInfo.Width;
                    var height = DeviceDisplay.MainDisplayInfo.Height;

                    var resizedStream = await ResizeImageAsync(stream4, (int)width, (int)width);
                    pickedImage = SKImage.FromEncodedData(resizedStream);
                //}

                //skiaView.InvalidateSurface();
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

        private void PickPhoto_Tapped(object sender, EventArgs e)
        {
            PickFile();
        }



        //private async void Publish_Tapped(object sender, EventArgs e)
        //{
        //    updateResultLabel.Text = "successfully uploaded";
        //    //UploadFileToFTP((FileStream)imageStream);
        //    //string captionText = (string)((TappedEventArgs)e).Parameter;
        //    Button b = sender as Button;
        //    string captionText = b.CommandParameter as string;
        //    WhatsHotsService whatsHotsService = new WhatsHotsService();
        //    whatsHotsService.InsertPost(captionText,imageUrl);
        //    captionEntry.Text = "ChFile Successfully Uploaded";
        //    var nextPage = new Community();
        //    await Navigation.PushAsync(nextPage);

        //}

        private async void UploadFileToFTP(FileStream fs)
        {
            try
            {
                string ftpfullpath = ftpurl + System.IO.Path.GetFileName(fs.Name);
                imageUrl = "http://www.expatcallers.com/swingsocial/wha/"+ System.IO.Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                System.IO.Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        //private async void UploadImage_TappedConfirmBox(object sender, EventArgs e)
        //{
        //    FileActivityIndicator.IsRunning = true;
        //    //DoUploads();
        //    PublishButton.IsVisible = true;
        //    ShowUploadConfirmationFrame.IsVisible = false;
        //    WhatshotCaption.IsVisible = true;
        //    ImageFrame.IsVisible = true;
        //}

        //private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        //{
        //    PopUpWaitForUpload.IsVisible = false;
        //    MainScroll.Opacity = 1;
        //}

        //private async void DoUploads()
        //{
        //    try
        //    {
        //        UploadFileToFTP((FileStream)imageStream);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    PopUpWaitForUpload.IsVisible = false;
        //    MainScroll.Opacity = 1;
        //}
        //private void ShowPopUp_Tapped(object sender, EventArgs e)
        //{
        //    ShowPopUp();
        //}
        //private void ShowPopUp()
        //{
        //    PopUpWaitForUpload.IsVisible = true;
        //    MainScroll.Opacity = 0.5;
        //}


        //async void PickVideoFile()
        //{
        //    // Opening the File Picker - Filter with Jpeg image
        //    var result = await FilePicker.PickAsync(new PickOptions
        //    {
        //        PickerTitle = "Select your video",
        //        FileTypes = FilePickerFileType.Videos,
        //    });

        //    // Here add the code that is being explained in the next step
        //    if (result != null)
        //    {
        //        var stream = await result.OpenReadAsync();
        //        var stream2 = await result.OpenReadAsync();
        //        imageStream = stream;
        //        VideoToDisplay.Source = VideoSource.FromStream(() => { return stream2; }, "mp4");
        //        deletephotoFrame.IsVisible = true;
        //        VideoToDisplaySection.IsVisible = true;
        //        ImageToDisplaySection.IsVisible = false;
        //        ImageFrame.IsVisible = false;
        //        VideoFrame.IsVisible = true;
        //    }
        //}
        //private void TapGestureRecognizer_VideoTapped(object sender, EventArgs e)
        //{
        //    PickVideoFile();
        //}
        //private void TapGestureRecognizer_VideoPublishTapped(object sender, EventArgs e)
        //{
        //    FileActivityIndicator.IsEnabled = true;
        //    FileActivityIndicator.IsRunning = true;
        //    FileActivityIndicator.IsVisible = true;
        //    updateResultLabel.Text = "successfully uploaded";
        //    UploadFileToFTP((FileStream)imageStream);
        //    string captionText = (string)((TappedEventArgs)e).Parameter;
        //    WhatsHotsService whatsHotsService = new WhatsHotsService();
        //    whatsHotsService.InsertVideoPost(captionText, imageUrl);
        //    captionEntry.Text = "Video Successfully Uploaded";
        //    FileActivityIndicator.IsEnabled = false;
        //    FileActivityIndicator.IsRunning = false;
        //    FileActivityIndicator.IsVisible = false;
        //    imageStream = null;
        //}

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
            //CropImageCanvas.InvalidateSurface(); // redraw
            using var cropPath = new SKPath();
            cropPath.AddPoly(arr);

            // add a little padding all round
            var bounds = SKRectI.Ceiling(cropPath.TightBounds);
            bounds.Inflate(4, 4);

            var info = new SKImageInfo(bounds.Width, bounds.Height);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;

            //canvas.Clear(SKColors.Transparent);

            canvas.Translate(-bounds.Left, -bounds.Top);
            canvas.ClipPath(cropPath);

            canvas.DrawImage(pickedImage, 0, 0);

            var image = surface.Snapshot();

            var fileStreamLocal = (FileStream)imageStream;
            var imageName = System.IO.Path.GetFileName(fileStreamLocal.Name);
            //UploadImageView.Source = (SKImageImageSource)image;

            Navigation.PushAsync(new PreviewPage(image,imageName, (System.IO.Stream)imageStream));
        }

        //private async void DisplayCroppedButton_OnClicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DisplayCroppedButton.IsVisible = false;
        //        CropImageCanvas.IsVisible = false;
        //        CroppedImageView.IsVisible = true;

        //        System.IO.Stream stream = new MemoryStream(_bytearray);
        //        System.IO.Stream stream2 = new MemoryStream(_bytearray);
        //        System.IO.Stream streamCropped = new MemoryStream(_bytearray);
        //        croppedStream = stream;
        //        //send to the api to do upload
        //        WhatsHotsService whs = new WhatsHotsService();
        //        WhatsHotStream whsStream = new WhatsHotStream();
        //        whsStream.base64String = Convert.ToBase64String(_bytearray);
        //        var result = whs.UploadPostCroppedImage(whsStream);
        //        //streamCropped.Position = 0;
        //        //streamCropped.CopyTo((FileStream)imageStream);
        //        //UploadImageView.Source = ImageSource.FromStream(() => stream2);
        //        CroppedImageView.Source = ImageSource.FromStream(() => stream);
        //        CroppedImageView.Aspect = Aspect.Fill;
        //        CroppedImageView.HorizontalOptions = LayoutOptions.FillAndExpand;
        //        CroppedImageView.HorizontalOptions = LayoutOptions.FillAndExpand;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            SKBitmap sKBitmap = null;
            if (croppedStream!=null)
            {
                SKBitmap croppedBitmap = new SKBitmap(30, 30);
                canvas.DrawBitmap(croppedBitmap, 0, 0);
                sKBitmap = SKBitmap.Decode((System.IO.Stream)croppedStream); 
            }
            //canvas.Clear();

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
                //DisplayCroppedButton.IsVisible = true;
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

        private void OnPaintSurafce(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Black);
            SKImage image = SKImage.FromBitmap(_originalBitmap);
            if (image != null)
            {
                canvas.DrawImage(image, 0, 0);
            }

            //if (pickedImage != null)
            //{
            //    canvas.DrawImage(pickedImage, 0, 0);
            //}

            //if (croppedStream != null)
            //{
            //    SKBitmap croppedBitmap = new SKBitmap(30, 30);
            //    canvas.DrawBitmap(croppedBitmap, 0, 0);
            //}

            if (cropPoints.Count > 0)
            {
                using var pathPaint = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 6,
                    Color = SKColors.Gray,
                };
                canvas.DrawPath(cropPath, pathPaint);

                pathPaint.StrokeWidth = 4;
                pathPaint.Color = SKColors.White;
                canvas.DrawPath(cropPath, pathPaint);
            }
        }

        private void OnClear(object sender, EventArgs e)
        {
            cropPoints.Clear();
            cropPath?.Dispose();
            cropPath = null;

            //skiaView.InvalidateSurface();
        }

        private void OnTouchSurface(object sender, SKTouchEventArgs e)
        {
            if (e.InContact)
            {
                cropPoints.Add(e.Location);

                cropPath?.Dispose();
                cropPath = new SKPath();
                cropPath.AddPoly(cropPoints.ToArray());

                //skiaView.InvalidateSurface();
            }

            e.Handled = true;
        }

        private void OnCropImage(object sender, EventArgs e)
        {
            using var cropPath = new SKPath();
            cropPath.AddPoly(cropPoints.ToArray());

            // add a little padding all round
            var bounds = SKRectI.Ceiling(cropPath.TightBounds);
            bounds.Inflate(4, 4);

            var info = new SKImageInfo(bounds.Width, bounds.Height);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;

            canvas.Clear(SKColors.Transparent);

            canvas.Translate(-bounds.Left, -bounds.Top);
            canvas.ClipPath(cropPath);

            canvas.DrawImage(pickedImage, 0, 0);

            var image = surface.Snapshot();

            Navigation.PushAsync(new PreviewPage(image,null,null));
        }
    }

    public static class Extensions
    {
        public static MemoryStream ToMemoryStream(this byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}