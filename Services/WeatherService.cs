using Newtonsoft.Json.Linq;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherService
{
    private HttpClient _httpClient = new HttpClient();
    
    public async Task<Weather?> GetCurrentWeatherByCityLocation(double latitude, double longitude, string apiKey)
    {
        var response = await _httpClient.GetAsync(
            @$"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}"
            );
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        JObject jsonObject = JObject.Parse(content);
        
        var weather = new Weather
        {
            Main = (string?)jsonObject["weather"]?[0]?["main"] ?? "Unknown",
            Description = (string?)jsonObject["weather"]?[0]?["description"] ?? "No description",
            Icon = (string?)jsonObject["weather"]?[0]?["icon"],

            Temperature = (double?)jsonObject["main"]?["temp"] ?? -999.0,
            MinTemperature = (double?)jsonObject["main"]?["temp_min"] ?? -999.0,
            MaxTemperature = (double?)jsonObject["main"]?["temp_max"] ?? -999.0,
            Pressure = (double?)jsonObject["main"]?["pressure"] ?? -999.0,
            Humidity = (double?)jsonObject["main"]?["humidity"] ?? -999.0,

            WindSpeed = (double?)jsonObject["wind"]?["speed"] ?? -999.0,
            Cloudiness = (double?)jsonObject["clouds"]?["all"] ?? -999.0,
            Precipitation = (double?) jsonObject["rain"]?["1h"] ?? -999.0
        };

        return weather;
    }
    
    public async Task<Weather?> GetCurrentWeatherByCityName(string name, string apiKey)
    {
        var response = await _httpClient.GetAsync(
            $"https://api.openweathermap.org/data/2.5/weather?q={name}&appid={apiKey}"
            );

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        JObject jsonObject = JObject.Parse(content);
        
        var weather = new Weather
        {
            Main = (string?)jsonObject["weather"]?[0]?["main"] ?? "Unknown",
            Description = (string?)jsonObject["weather"]?[0]?["description"] ?? "No description",
            Icon = (string?)jsonObject["weather"]?[0]?["icon"],

            Temperature = (double?)jsonObject["main"]?["temp"] ?? -999.0,
            MinTemperature = (double?)jsonObject["main"]?["temp_min"] ?? -999.0,
            MaxTemperature = (double?)jsonObject["main"]?["temp_max"] ?? -999.0,
            Pressure = (double?)jsonObject["main"]?["pressure"] ?? -999.0,
            Humidity = (double?)jsonObject["main"]?["humidity"] ?? -999.0,

            WindSpeed = (double?)jsonObject["wind"]?["speed"] ?? -999.0,
            Cloudiness = (double?)jsonObject["clouds"]?["all"] ?? -999.0,
            Precipitation = (double?) jsonObject["rain"]?["1h"] ?? -999.0
        };

        return weather;
    }
}