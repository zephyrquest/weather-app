using SQLite;

namespace WeatherApp.Models;

public class City
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }

    public string Name { get; set; } = "";
    public string Country { get; set; } = "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}