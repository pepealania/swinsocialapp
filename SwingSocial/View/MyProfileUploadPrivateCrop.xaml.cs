using Android.Speech.Tts;
using Java.Util.Streams;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using FluentFTP;
using Org.Apache.Http;
using Android.Systems;

namespace SwingSocial.Sample.View
{
    public partial class MyProfileUploadPrivateCrop : ContentPage
    {
        private const int BaseBlockSize = 12;
        static string ftpHost = "sub.expatcallers.com";
        static String ftpurl = $"ftp://sub.expatcallers.com/swingsocial/myprofile/private/"; // e.g. ftp://serverip/foldername/foldername
        static string folderUrl = $"http://sub.expatcallers.com/swingsocial/myprofile/private/";
        static String ftpusername = "clark@sub.expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru2020#"; // e.g. password
        static object imageStream;
        static string _imageName;
        static string imageUrl;
        static SKImage globalSKImage;
        public static int _order;
        public static bool commingFromPrivateCrop;
        public static SKImageImageSource currentStream;
        public static string imagePrivateUrl;
        public MyProfileUploadPrivateCrop(int order,SKImage image,string imageName,Stream stream)
        {
            globalSKImage = image;
            _imageName = imageName;
            InitializeComponent();
            imageStream = ConvertSkImageToStream(image);
            currentStream = (SKImageImageSource)image;
            previewImage.Source = (SKImageImageSource)image;
            _order = order;
        }
        public static Stream ConvertSkImageToStream(SKImage skImage, SKEncodedImageFormat format = SKEncodedImageFormat.Png, int quality = 100)
        {
            // Create a memory stream to hold the encoded image data
            var memoryStream = new MemoryStream();

            // Encode the SKImage to the specified format (PNG or JPEG)
            using (var imageStream = skImage.Encode(format, quality))
            {
                // Copy the encoded image data to the memory stream
                imageStream.SaveTo(memoryStream);
            }

            // Reset the memory stream position to the beginning
            memoryStream.Position = 0;

            return memoryStream;
        }
        private void OnPaintBackground(object sender, SKPaintSurfaceEventArgs e)
        {
            var scale = e.Info.Width / (float)((Xamarin.Forms.View)sender).Width;
            var blockSize = BaseBlockSize * scale;

            var offsetMatrix = SKMatrix.CreateScale(2 * blockSize, blockSize);
            var skewMatrix = SKMatrix.CreateSkew(0.5f, 0);
            var matrix = offsetMatrix.PreConcat(skewMatrix);

            using var path = new SKPath();
            path.AddRect(SKRect.Create(blockSize / -2, blockSize / -2, blockSize, blockSize));

            using var paint = new SKPaint
            {
                PathEffect = SKPathEffect.Create2DPath(matrix, path),
                Color = 0xFFF0F0F0
            };

            var canvas = e.Surface.Canvas;
            var area = SKRect.Create(e.Info.Width + blockSize, e.Info.Height + blockSize);
            canvas.DrawRect(area, paint);
        }

        private async void Publish_Tapped(object sender, System.EventArgs e)
        {
            updateResultLabel.Text = "successfully uploaded";
            MyProfileService myProfileService = new MyProfileService();
            var result = await myProfileService.UpdateAvatar(imageUrl);
            await DisplayAlert("Update Status", "Avatar successfully updated.", "ok");
            var nextPage = new MyProfilePage();
            await Navigation.PushAsync(nextPage);

        }
        private async void UploadImage_TappedConfirmBox(object sender, EventArgs e)
        {
            FileActivityIndicator.IsRunning = true;
            DoUploads();
            PublishButton.IsVisible = true;
            ShowUploadConfirmationFrame.IsVisible = false;
            //WhatshotCaption.IsVisible = true;
            //ImageFrame.IsVisible = true;
        }
        private void ShowPopUp_Tapped(object sender, EventArgs e)
        {
            ShowPopUp();
        }
        private void ShowPopUp()
        {
            PopUpWaitForUpload.IsVisible = true;
            MainScroll.Opacity = 0.5;
        }

        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void DoUploads()
        {
            try
            {
                //UploadFileToFTP((FileStream)imageStream);
                //UpLoadFileToServer((MemoryStream)imageStream);
                bool result = await UploadSkImageToFtpAsync(globalSKImage, ftpHost, ftpusername, ftppassword, _imageName);
                commingFromPrivateCrop = true;
                var nextPage = new MyProfilePage();
                await Navigation.PushAsync(nextPage);
            }
            catch (Exception ex)
            {

                throw;
            }
            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }
        private async void UploadFileToFTP(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + System.IO.Path.GetFileName(fs.Name);
                imageUrl = "http://sub.expatcallers.com/swingsocial/myprofile/private/" + System.IO.Path.GetFileName(fs.Name);
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

        private async void UpLoadFileToServer(MemoryStream memoryStream)
        {
            memoryStream.Position = 0;

            // Convert MemoryStream to byte array
            byte[] _bytearray = memoryStream.ToArray();
            WhatsHotsService whs = new WhatsHotsService();
            WhatsHotStream whsStream = new WhatsHotStream();
            whsStream.base64String = Convert.ToBase64String(_bytearray);
            var result = whs.UploadPostCroppedImage(whsStream);

        }
        public static async Task<bool> UploadSkImageToFtpAsync(SKImage skImage, string ftpHost, string ftpUser, string ftpPassword, string remoteFileName)
        {
            // Convert SKImage to MemoryStream
            var memoryStream = ConvertSkImageToStream(skImage);

            // Upload the MemoryStream to FTP
            object value = await UploadMemoryStreamToFtpAsync((MemoryStream)memoryStream, ftpHost, ftpUser, ftpPassword, remoteFileName);

            // Dispose the MemoryStream
            memoryStream.Dispose();
            return true;
        }
        public static async Task<bool> UploadMemoryStreamToFtpAsync(MemoryStream memoryStream, string ftpHost, string ftpUser, string ftpPassword, string remoteFileName)
        {
            try
            {
                // Initialize FTP client
                using (var client = new FtpClient(ftpHost, ftpUser, ftpPassword))
                {
                    // Connect to the FTP server
                    client.Connect();

                    // Reset the position of the MemoryStream to the beginning
                    memoryStream.Position = 0;

                    // Upload the MemoryStream
                    await Task.Run(() => client.UploadStream(memoryStream, "/swingsocial/myprofile/private/" + remoteFileName, FtpRemoteExists.Overwrite));
                    imageUrl = folderUrl + remoteFileName;
                    imagePrivateUrl = imageUrl;
                    // Optionally, disconnect from the server
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return true;
        }
    }


}
