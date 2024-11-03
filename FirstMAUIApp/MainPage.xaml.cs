using FirstMAUIApp.ViewModel;
using System.Runtime.Intrinsics.X86;

namespace FirstMAUIApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
