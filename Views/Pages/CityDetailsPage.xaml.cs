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
        
        _cityDetailsViewModel.LoadCurrentWeather();
    }

    private async void OnForecastClicked(object? sender, EventArgs eventArgs)
    {
        await Navigation.PushModalAsync(new CityForecastPage(new CityForecastViewModel(_cityDetailsViewModel.City)));
    }

    private async void OnBackClicked(object? sender, EventArgs eventArgs)
    {
        _cityDetailsViewModel.CurrentWeather = null;
        _cityDetailsViewModel.City = null;
        
        await Navigation.PopModalAsync();
    }
}