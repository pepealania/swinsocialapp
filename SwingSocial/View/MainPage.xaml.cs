using SwingSocial.Sample.ViewModel;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }
    }
}