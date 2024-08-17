using Android.Views;
using Android.Widget;
using SwingSocial.Sample;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]

namespace SwingSocial.Sample
{
    internal class ToastMessage:IToast
    {
        public void ShortToast(string message)
        {
            Toast t =Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short);
            t.SetGravity(GravityFlags.Top & GravityFlags.CenterHorizontal, 0, 0);            
            t.Show();
        }
        public void LongToast(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}
