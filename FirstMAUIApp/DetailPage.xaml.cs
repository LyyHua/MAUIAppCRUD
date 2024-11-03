using FirstMAUIApp.ViewModel;

namespace FirstMAUIApp;

public partial class DetailPage : ContentPage
{
    private readonly DetailViewModel _viewModel;

    public DetailPage(DetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadStudentsAsync();
    }
}