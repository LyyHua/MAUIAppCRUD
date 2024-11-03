using FirstMAUIApp.Entities;
using FirstMAUIApp.ViewModel;

namespace FirstMAUIApp;

public partial class UpdateDialog : ContentPage
{
    public UpdateDialog(UpdateDialogViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}