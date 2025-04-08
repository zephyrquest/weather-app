using WeatherApp.Models;
using WeatherApp.Pages;
using WeatherApp.ViewModels;

namespace WeatherApp;

public partial class CitiesListPage : ContentPage
{
    public CitiesListViewModel CitiesListViewModel = new CitiesListViewModel();
    
    public CitiesListPage()
    {
        InitializeComponent();
        BindingContext = CitiesListViewModel;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!CitiesListViewModel.Initialied)
        {
            CitiesListViewModel.SetCurrentLocationCity();
            CitiesListViewModel.RetrieveSavedCities();
            CitiesListViewModel.Initialied = true;
        }
    }

    private async void OnAddCityClicked(object? sender, EventArgs eventArgs)
    {
        await Navigation.PushModalAsync(new AddCityPage(CitiesListViewModel));
    }

    private async void OnCurrentLocationCitySelected(object? sender, TappedEventArgs tappedEventArgs)
    {
        if (CitiesListViewModel.CurrentLocationCity != null)
        {
            await Navigation.PushModalAsync(new CityDetailsPage(new CityDetailsViewModel(CitiesListViewModel.CurrentLocationCity)));
        }
    }

    private async void OnListItemSelected(object? sender, SelectionChangedEventArgs eventArgs)
    {
        if (eventArgs.CurrentSelection.FirstOrDefault() is City selectedCity)
        {
            await Navigation.PushModalAsync(new CityDetailsPage(new CityDetailsViewModel(selectedCity)));
        }
    }

    private void OnDeleteCityClicked(object? sender, EventArgs eventArgs)
    {
        if (sender is SwipeItemView item)
        {
            if (item.BindingContext is City city)
            {
                CitiesListViewModel.DeleteCity(city);
            }
        }
    }
}