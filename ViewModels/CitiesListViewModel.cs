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
    
    public async void RetrieveCities()
    {
        var cities = await _cityRepository.GetCities();

        if (cities != null)
        {
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }
    }

    public async void AddCity(City city)
    {
        Cities.Add(city);

        await _cityRepository.SaveCity(city);
    }

    public async void DeleteCity(City city)
    {
        Cities.Remove(city);

        await _cityRepository.DeleteCity(city);
    }
}