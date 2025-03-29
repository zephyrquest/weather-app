using WeatherApp.Models;

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
        var weather = await _weatherService.GetCurrentWeatherByCityLocation(_city.Latitude, _city.Longitude,
            _userConfigService.GetConfiguration("openweathermap_apikey"));

        if (weather != null)
        {
            Weather = weather;
        }
    }
}