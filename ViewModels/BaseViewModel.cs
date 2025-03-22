using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    protected WeatherService _weatherService = new();

    public BaseViewModel()
    {
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}