using WeatherApp.Views.Pages;

namespace WeatherApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new CitiesListPage();
    }
}