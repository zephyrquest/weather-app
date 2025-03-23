using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class CityDetailsViewModel : BaseViewModel
{
    private City _city;
    private Weather _weather;
    
    public City City => _city;

    public Weather Weather
    {
        get => _weather;
        set
        {
            _weather = value;
            OnPropertyChanged();
        }
    }

    public CityDetailsViewModel(City city)
    {
        _city = city;
        _weather = new();
    }
    
    public async void LoadWeatherInfo()
    {
        var weather = await _weatherService.GetCurrentWeatherByCityName(_city.Name, 
            _userConfigService.GetConfiguration("api_key"));

        if (weather != null)
        {
            Weather = weather;
        }
    }
}