using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Pages;

public partial class AddCityPage : ContentPage
{
    private AddCityViewModel _addCityViewModel = new();
    private CitiesListViewModel _citiesListViewModel;
    
    public AddCityPage(CitiesListViewModel citiesListViewModel)
    {
        InitializeComponent();

        _citiesListViewModel = citiesListViewModel;
        BindingContext = _addCityViewModel;
    }

    private void OnCitySearchBarTextChanged(object? sender, TextChangedEventArgs eventArgs)
    {
        if (_addCityViewModel.CurrentSelectedCity != null)
        {
            _addCityViewModel.CurrentSelectedCity = null;
        }
        
        _addCityViewModel.CurrentSelectedCity = null;
        _addCityViewModel.IsAddCityButtonEnabled = false;
        
        var cityNameInitials = CitySearchBar.Text.Trim();

        if (string.IsNullOrEmpty(cityNameInitials))
        {
            return;
        }

        _addCityViewModel.UpdateCitySearchBar(cityNameInitials);
    }

    private void OnCitySearchBarListClicked(object? sender, SelectionChangedEventArgs eventArgs)
    {
        if (eventArgs.CurrentSelection.FirstOrDefault() is City selectedCity)
        {
            _addCityViewModel.FilteredCities.Clear();
            CitySearchBar.Text = $"{selectedCity.Name}, {selectedCity.Country}";
            _addCityViewModel.CurrentSelectedCity = selectedCity;
            _addCityViewModel.IsAddCityButtonEnabled = true;
        }
    }

    private async void OnAddClicked(object? sender, EventArgs eventArgs)
    {
        if (_addCityViewModel.CurrentSelectedCity != null)
        {
            _citiesListViewModel.AddCity(_addCityViewModel.CurrentSelectedCity);
            _addCityViewModel.CurrentSelectedCity = null;
        
            await Navigation.PopModalAsync();
        }
    }

    private async void OnCancelClicked(object? sender, EventArgs eventArgs)
    {
        _addCityViewModel.CurrentSelectedCity = null;
        _addCityViewModel.IsAddCityButtonEnabled = false;
        
        await Navigation.PopModalAsync();
    }
}