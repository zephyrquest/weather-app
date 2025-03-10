using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Models;

namespace WeatherApp;

public partial class CitiesListPage : ContentPage, INotifyPropertyChanged
{
    private ObservableCollection<City> _cities;

    public ObservableCollection<City> Cities
    {
        get => _cities;
        set
        {
            _cities = value;
            OnPropertyChanged();
        }
    }
    
    public CitiesListPage()
    {
        InitializeComponent();
        
        BindingContext = this;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Cities = new ObservableCollection<City>()
        {
            new City
            {
                Name = "Malvaglia"
            }
        };
    }
    
    protected override void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}