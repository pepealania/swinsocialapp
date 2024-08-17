using System;
using Android.App;
using Android.Runtime;

namespace SwingSocialMap.Droid
{
    [Application(UsesCleartextTraffic = true)]
    //[MetaData("com.google.android.maps.v2.API_KEY", Value = AppConstants.GOOGLE_API_ACCESS_KEY)]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}
