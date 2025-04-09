namespace WeatherApp.Models;

public class DailyForecast
{
    public DateTime Date { get; set; }
    public List<Weather> Forecasts { get; set; } = new();
}