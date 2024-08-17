using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Wallet;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.OS;
using SwingSocial.Sample;
using Xamarin.Forms;
using Plugin.CurrentActivity;
namespace SwingSocial.Droid
{
    [Activity(Label = "SwingSocial", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        private GoogleApiClient googleApiClient;
        private PaymentDataRequest paymentRequest;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            TabLayoutResource = SwingSocial.Droid.Resource.Layout.Tabbar;
            ToolbarResource = SwingSocial.Droid.Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.activity_main);
            //FormsGoogleMaps.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            // Initialize GoogleApiClient
            googleApiClient = new GoogleApiClient.Builder(this)
                .AddApi(WalletClass.API, new WalletClass.WalletOptions.Builder()
                    .Build())
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .Build();

            // Connect to GoogleApiClient
            googleApiClient.Connect();


            Forms.Init(this, savedInstanceState);
            //CachedImageRenderer.Init(true);
            // Set our view from the "main" layout resource
            LoadApplication(new App());
            if (ActivityCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReadMediaImages) != Android.Content.PM.Permission.Granted)
            {
                if ((int)Android.OS.Build.VERSION.SdkInt > 32)
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Android.Manifest.Permission.ReadMediaImages }, 101);
                }
            }
            MessagingCenter.Subscribe<string>(this, "PayViaGooglePay", (value) =>
            {
                PayViaGooglePay();
            });

        }
        public void PayViaGooglePay()
        {
            //paymentRequest = PaymentDataRequest.Create()
            //.SetTransactionInfo(TransactionInfo.Create()
            //    .SetTotalPrice("1.00")  // Set total price of the transaction
            //    .SetCurrencyCode("USD"))  // Set currency code
            //.SetPaymentMethodTokenizationParameters(
            //    PaymentMethodTokenizationParameters.FromJson("{\"type\":\"PAYMENT_GATEWAY\",\"parameters\":{\"gateway\":\"your_gateway_name_here\",\"gatewayMerchantId\":\"your_merchant_id_here\"}}")
            //)

            //var request = PaymentDataRequest.FromJson(GetPaymentDataRequestJson());
            //if (request != null)
            //{
            //    var intent = WalletClass.Payments.GetPaymentDataIntent(googleApiClient, request);
            //    StartActivityForResult(intent, REQUEST_CODE_PAYMENT);
            //}

            //.AddAllowedPaymentMethod(WalletConstants.PaymentMethodTokenizedCard)
            //.Build();
        }
        private string GetPaymentDataRequestJson()
        {
            // Example JSON for payment request
            var json = @"
    {
        'apiVersion': 2,
        'apiVersionMinor': 0,
        'allowedPaymentMethods': [
            {
                'type': 'CARD',
                'parameters': {
                    'allowedAuthMethods': ['PAN_ONLY', 'CRYPTOGRAM_3DS'],
                    'allowedCardNetworks': ['MASTERCARD', 'VISA']
                },
                'tokenizationSpecification': {
                    'type': 'PAYMENT_GATEWAY',
                    'parameters': {
                        'gateway': 'example',
                        'gatewayMerchantId': 'exampleGatewayMerchantId'
                    }
                }
            }
        ],
        'transactionInfo': {
            'totalPrice': '10.00',
            'currencyCode': 'USD'
        },
        'merchantInfo': {
            'merchantName': 'Example Merchant'
        }
    }";

            return json;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Implement required methods for GoogleApiClient callbacks
        public void OnConnected(Bundle connectionHint)
        {
            // GoogleApiClient connected successfully
        }

        public void OnConnectionSuspended(int cause)
        {
            // GoogleApiClient connection suspended
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            // GoogleApiClient connection failed
        }

        protected override void OnDestroy()
        {
            // Disconnect GoogleApiClient when no longer needed
            if (googleApiClient != null && googleApiClient.IsConnected)
            {
                googleApiClient.Disconnect();
            }
            base.OnDestroy();
        }
    }
}