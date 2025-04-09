using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels;

namespace WeatherApp.Views.Pages;

public partial class CityForecastPage : ContentPage
{
    private CityDetailsViewModel _cityDetailsViewModel;
    
    public CityForecastPage(CityDetailsViewModel cityDetailsViewModel)
    {
        InitializeComponent();

        _cityDetailsViewModel = cityDetailsViewModel;
        BindingContext = _cityDetailsViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        _cityDetailsViewModel.LoadWeatherForecast();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        _cityDetailsViewModel.DailyForecasts.Clear();
    }
}