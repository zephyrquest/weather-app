namespace WeatherApp.Models;

public class Weather
{
    
    public required string? Main { get; set; }
    public required string Description { get; set; }
    public string? Icon { get; set; }
    public double Temperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double Pressure { get; set; }
    public double Humidity { get; set; }
    public double WindSpeed { get; set; }
    public double Cloudiness { get; set; }
    public double Precipitation { get; set; }
}