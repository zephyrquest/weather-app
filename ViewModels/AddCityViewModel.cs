using System.Collections.ObjectModel;
using WeatherApp.Models;

namespace WeatherApp.ViewModels;

public class AddCityViewModel : BaseViewModel
{
    private ObservableCollection<City> _filteredCities;
    private City? _currentSelectedCity;
    private bool _isAddCityButtonEnabled;
    
    public ObservableCollection<City> FilteredCities
    {
        get => _filteredCities;
        set
        {
            _filteredCities = value;
            OnPropertyChanged();
        }
    }

    public City? CurrentSelectedCity
    {
        get => _currentSelectedCity;
        set
        {
            _currentSelectedCity = value;
            OnPropertyChanged();
        }
    }

    public bool IsAddCityButtonEnabled
    {
        get => _isAddCityButtonEnabled;
        set
        {
            _isAddCityButtonEnabled = value;
            OnPropertyChanged();
        }
    }

    public AddCityViewModel()
    {
        FilteredCities = new ObservableCollection<City>();
        IsAddCityButtonEnabled = false;
    }
    
    public async void UpdateCitySearchBar(string nameInitials)
    {
        if (string.IsNullOrEmpty(nameInitials))
        {
            return;
        }

        var cities = await _cityService.GetCitiesByNameInitials(nameInitials, 4,
            _userConfigService.GetConfiguration("geonames_username"));
        
        if (cities != null)
        {
            FilteredCities.Clear();
            foreach (var city in cities)
            {
                if (FilteredCities.FirstOrDefault(c => c.Name == city.Name && c.Country == city.Country) == null)
                {
                    FilteredCities.Add(city);
                }
            }
        }
    }
}