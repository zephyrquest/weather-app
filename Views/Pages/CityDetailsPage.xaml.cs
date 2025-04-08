using WeatherApp.ViewModels;

namespace WeatherApp.Views.Pages;

public partial class CityDetailsPage : ContentPage
{
    private CityDetailsViewModel _cityDetailsViewModel;
    
    public CityDetailsPage(CityDetailsViewModel cityDetailsViewModel)
    {
        InitializeComponent();

        _cityDetailsViewModel = cityDetailsViewModel;
        BindingContext = _cityDetailsViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        _cityDetailsViewModel.LoadWeatherInfo();
    }

    private async void OnBackClicked(object? sender, EventArgs eventArgs)
    {
        await Navigation.PopModalAsync();
    }
}