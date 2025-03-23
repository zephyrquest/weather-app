using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    protected UserConfigService _userConfigService = new();
    protected WeatherService _weatherService = new();

    public BaseViewModel()
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
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}