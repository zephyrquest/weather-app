namespace WeatherApp.Models;

public class DailyForecast
{
    public City? City { get; set; }
    public DateTime Date { get; set; }
    public List<Weather> Forecasts { get; set; } = new();
}