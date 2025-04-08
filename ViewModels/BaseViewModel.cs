using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Repository;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    protected CityRepository _cityRepository = new();
    
    protected UserConfigService _userConfigService = new();
    protected WeatherService _weatherService = new();
    protected CityService _cityService = new();
    protected LocalisationService _localisationService = new();

    
    public BaseViewModel()
    {
        InitRepository();
        InitUserConfig();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void InitRepository()
    {
        await _cityRepository.Init();
    }

    private void InitUserConfig()
    {
        if (!_userConfigService.UserConfigFileExist())
        {
            _userConfigService.ReadDefaultUserConfig();
            _userConfigService.WriteUserConfig();
        }
        else
        {
            _userConfigService.ReadUserConfig();
        }
    }
}