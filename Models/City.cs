namespace WeatherApp.Models;

public class City
{
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
}