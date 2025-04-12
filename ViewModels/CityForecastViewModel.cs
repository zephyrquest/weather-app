using System.Collections.ObjectModel;
using WeatherApp.Models;

namespace WeatherApp.ViewModels;

public class CityForecastViewModel : BaseViewModel
{
    private City? _city;
    private ObservableCollection<DailyForecast> _dailyForecasts;
    
    public City? City
    {
        get => _city;
        set
        {
            _city = value;
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<DailyForecast> DailyForecasts
    {
        get => _dailyForecasts;
        set
        {
            _dailyForecasts = value;
            OnPropertyChanged();
        }
    }

    public CityForecastViewModel(City city)
    {
        City = city;
        DailyForecasts = new ObservableCollection<DailyForecast>();
    }
    
    public async void LoadWeatherForecast()
    {
        if (City == null)
        {
            return;
        }
        
        var weatherForecasts = await _weatherService.GetDailyWeatherForecastsByCityLocation(City.Latitude, City.Longitude,
            _userConfigService.GetConfiguration("openweathermap_apikey"));

        if (weatherForecasts != null)
        {
            foreach (var weather in weatherForecasts)
            {
                weather.City = City;
                DailyForecasts.Add(weather);
            }
        }
    }
}