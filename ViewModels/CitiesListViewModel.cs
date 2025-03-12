using System.Collections.ObjectModel;
using WeatherApp.Models;

namespace WeatherApp.ViewModels;

public class CitiesListViewModel : BaseViewModel
{
    private ObservableCollection<City> _cities;
    private bool _initialied;
    
    public ObservableCollection<City> Cities
    {
        get => _cities;
        set
        {
            _cities = value;
            OnPropertyChanged();
        }
    }

    public bool Initialied
    {
        get => _initialied;
        set => _initialied = value;
    }

    public CitiesListViewModel()
    {
        Cities = new ObservableCollection<City>();
        _initialied = false;
    }
    
    public void InitializeList()
    {
        AddCity(new City
        {
            Name = "Malvaglia"
        });
        
        AddCity(new City
        {
            Name = "Biasca"
        });
    }

    public void AddCity(City city)
    {
        Cities.Add(city);
    }
}