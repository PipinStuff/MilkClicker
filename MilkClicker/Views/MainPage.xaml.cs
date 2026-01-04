namespace MilkClicker.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is MainViewModel vm)
        {
            vm.Cleanup();
        }
    }
}
