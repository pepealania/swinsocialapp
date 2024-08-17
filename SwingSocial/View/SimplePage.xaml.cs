using SwingSocial.Sample.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimplePage : ContentPage
    {
        public SimplePage()
        {
            InitializeComponent();
            BindingContext = new SimplePageViewModel();
        }
    }
}