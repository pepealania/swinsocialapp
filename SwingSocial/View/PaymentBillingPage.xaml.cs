using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class PaymentBillingPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string ToProfileId;
        static PaymentBillingViewModel PaymentBillingViewModel { get; set; }
        public PaymentBillingPage()
        {
            InitializeComponent();
            PaymentBillingViewModel = new PaymentBillingViewModel(Navigation);
            BindingContext = PaymentBillingViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnListViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ChatService chatService = new ChatService();
            Button button = (Button)sender;
            string conversationText = button.CommandParameter.ToString();
            chatService.InsertConversation("3e108ddd-ecda-409f-abd1-672d8200b34d", ToProfileId, conversationText);
            PaymentBillingViewModel = new PaymentBillingViewModel(Navigation);
            BindingContext = PaymentBillingViewModel;
            //ChatViewModel.ChatComments.CollectionChanged += MyGroupTickets_CollectionChanged;
        }

        private async void OnChatClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var secondPage = new MyChats();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

        private void PayButton_Clicked(object sender, EventArgs e)
        {
            PopUpConfirm.IsVisible = true;
            MainScroll.Opacity = 0.5;

        }

        private async void PayViaGooglePay(object sender, EventArgs e)
        {
            //MessagingCenter.Send<string>("","PayViaGooglePay");

            PaymentService paymentService = new PaymentService();  
            PaymentData paymentData = new PaymentData();
            paymentData.CardNumber = CreditCardNumber.Text;
            paymentData.ExpirationDate = CreditCardExpDate.Text;
            paymentData.amount = 1;
            paymentData.Email = EventView.MyProfile.Email;
            paymentData.TicketPackages = new List<TicketPackage>();
            foreach (var item in EventView.TicketPackages)
            {
                TicketPackage tkp = new TicketPackage();
                tkp = item;
                tkp.ProfileId = SwipeCardView.UsrId.ToString();
                if (item.TicketsPurchased>0)
                {
                    paymentData.TicketPackages.Add(tkp);
                }       
            }
            var result = await paymentService.AuthorizeNetPay(paymentData);
            DisplayAlert("Payment Status", result, "ok");
            var secondPage = new EventView(EventView.currentEvent);
            await Navigation.PushAsync(secondPage);
        }

        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpConfirm.IsVisible = false;
            MainScroll.Opacity = 1;
        }
    }
}