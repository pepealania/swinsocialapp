using System;
using Xamarin.Forms;

namespace SwingSocial.Sample.ViewModel
{
    public class CropViewModel : BasePageViewModel
    {
        double mX = 0f;
        double mY = 0f;
        double mRatioPan = -0.0015f;
        double mRatioZoom = 0.8f;
        private string _imageUrl =string.Empty;




        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                RaisePropertyChanged();
            }
        }


        public void Reload()
        {
            CurrentZoomFactor = 1d;
            CurrentXOffset = 0d;
            CurrentYOffset = 0d;
        }

        void ReloadImage()
        {
            MessagingCenter.Send<object, object>(this, "Refresh", "");
        }

        public double CurrentZoomFactor { get; set; }

        public double CurrentXOffset { get; set; }

        public double CurrentYOffset { get; set; }

        internal void OnPanUpdated(PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Completed)
            {
                mX = CurrentXOffset;
                mY = CurrentYOffset;
            }
            else if (e.StatusType == GestureStatus.Running)
            {
                CurrentXOffset = (e.TotalX * mRatioPan) + mX;
                CurrentYOffset = (e.TotalY * mRatioPan) + mY;
                ReloadImage();
            }
        }

        internal void OnPinchUpdated(PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Completed)
            {
                mX = CurrentXOffset;
                mY = CurrentYOffset;

            }
            else if (e.Status == GestureStatus.Started || e.Status == GestureStatus.Running)
            {
                CurrentZoomFactor += (e.Scale - 1) * CurrentZoomFactor * mRatioZoom;
                CurrentZoomFactor = Math.Max(1, CurrentZoomFactor);

                CurrentXOffset = (e.ScaleOrigin.X * mRatioPan) + mX;
                CurrentYOffset = (e.ScaleOrigin.Y * mRatioPan) + mY;
                ReloadImage();
            }
        }
    }
}
