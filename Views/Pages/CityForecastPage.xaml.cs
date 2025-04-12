using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels;

namespace WeatherApp.Views.Pages;

public partial class CityForecastPage : ContentPage
{
    private CityForecastViewModel _cityForecastViewModel;
    
    public CityForecastPage(CityForecastViewModel cityForecastViewModel)
    {
        InitializeComponent();

        _cityForecastViewModel = cityForecastViewModel;
        BindingContext = _cityForecastViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        _cityForecastViewModel.LoadWeatherForecast();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        _cityForecastViewModel.DailyForecasts.Clear();
    }

    private async void OnBackClicked(object? sender, EventArgs eventArgs)
    {
        await Navigation.PopModalAsync();
    }
}