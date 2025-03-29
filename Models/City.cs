namespace WeatherApp.Models;

public class City
{
    public required string Name { get; set; }
    public string? Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}