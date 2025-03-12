using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Pages;

public partial class AddCityPage : ContentPage
{
    private CitiesListViewModel _citiesListViewModel;
    
    public AddCityPage(CitiesListViewModel citiesListViewModel)
    {
        InitializeComponent();

        _citiesListViewModel = citiesListViewModel;
        BindingContext = _citiesListViewModel;
    }

    private async void OnAddClicked(object? sender, EventArgs eventArgs)
    {
        var input = InputName.Text.Trim();

        if (string.IsNullOrEmpty(input))
        {
            return;
        }

        City city = new City
        {
            Name = input
        };

        _citiesListViewModel.AddCity(city);
        
        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs eventArgs)
    {
        await Navigation.PopModalAsync();
    }
}