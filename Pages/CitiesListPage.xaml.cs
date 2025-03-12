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
            CitiesListViewModel.InitializeList();
            CitiesListViewModel.Initialied = true;
        }
    }

    private async void OnAddCityClicked(object? sender, EventArgs eventArgs)
    {
        await Navigation.PushModalAsync(new AddCityPage(CitiesListViewModel));
    }
}