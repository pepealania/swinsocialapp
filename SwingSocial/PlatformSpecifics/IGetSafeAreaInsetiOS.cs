using System;
using Xamarin.Forms;

namespace SwingSocial.Sample.PlatformSpecifics
{
    public interface IGetSafeAreaInsetiOS
    {
        Thickness GetSafeInset();
    }
}
