using System.Collections.ObjectModel;
using WeatherApp.Models;

namespace WeatherApp.ViewModels;

public class CitiesListViewModel : BaseViewModel
{
    private ObservableCollection<City> _cities;
    private bool _initialied;
    private City? _currentLocationCity;
    private bool _hasCurrentLocationCity;
    
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

    public City? CurrentLocationCity
    {
        get => _currentLocationCity;
        set
        {
            _currentLocationCity = value;
            OnPropertyChanged();
        }
    }

    public bool HasCurrentLocationCity
    {
        get => _hasCurrentLocationCity;
        set
        {
            _hasCurrentLocationCity = value;
            OnPropertyChanged();
        }
    }

    public CitiesListViewModel()
    {
        Cities = new ObservableCollection<City>();
        _initialied = false;
        HasCurrentLocationCity = false;
    }

    public async void SetCurrentLocationCity()
    {
        if (!await _localisationService.HasLocationPermission())
        {
            return;
        }
        
        var location = await _localisationService.GetCurrentLocation();
        if (location == null)
        {
            return;
        }

        var cityName = await _cityService.GetCityName(location.Latitude, location.Longitude, 
            _userConfigService.GetConfiguration("openweathermap_apikey"));
        if (cityName == null)
        {
            return;
        }

        var countryName = await _cityService.GetCountryName(location.Latitude, location.Longitude,
            _userConfigService.GetConfiguration("geonames_username"));
        
        CurrentLocationCity = new City
        {
            Name = cityName,
            Country = countryName ?? "No country found",
            Latitude = location.Latitude,
            Longitude = location.Longitude
        };

        HasCurrentLocationCity = true;
    }
    
    public async void RetrieveSavedCities()
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