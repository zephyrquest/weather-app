using System.Collections.ObjectModel;
using WeatherApp.Models;

namespace WeatherApp.ViewModels;

public class CityDetailsViewModel : BaseViewModel
{
    private City? _city;
    private Weather? _currentWeather;
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

    public Weather? CurrentWeather
    {
        get => _currentWeather;
        set
        {
            _currentWeather = value;
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

    public CityDetailsViewModel(City city)
    {
        City = city;
        CurrentWeather = null;
        DailyForecasts = new ObservableCollection<DailyForecast>();
    }
    
    public async void LoadCurrentWeather()
    {
        if (City == null)
        {
            return;
        }
        
        var weather = await _weatherService.GetCurrentWeatherByCityLocation(City.Latitude, City.Longitude,
            _userConfigService.GetConfiguration("openweathermap_apikey"));

        if (weather != null)
        {
            CurrentWeather = weather;
        }
    }
}