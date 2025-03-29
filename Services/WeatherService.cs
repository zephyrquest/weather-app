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

        return CreateWeatherObject(jsonObject);
    }
    
    public async Task<Weather?> GetCurrentWeatherByCityName(string name, string apiKey)
    {
        var response = await _httpClient.GetAsync(
            $"https://api.openweathermap.org/data/2.5/weather?q={name}&appid={apiKey}&units=metric"
            );

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        JObject jsonObject = JObject.Parse(content);

        return CreateWeatherObject(jsonObject);
    }

    private Weather CreateWeatherObject(JObject weatherJson)
    {
        var weather = new Weather
        {
            Main = (string?)weatherJson["weather"]?[0]?["main"] ?? "Unknown",
            Description = (string?)weatherJson["weather"]?[0]?["description"] ?? "No description",
            Icon = $"https://openweathermap.org/img/wn/{(string?)weatherJson["weather"]?[0]?["icon"]}@2x.png",

            Temperature = (double?)weatherJson["main"]?["temp"] ?? null,
            MinTemperature = (double?)weatherJson["main"]?["temp_min"] ?? null,
            MaxTemperature = (double?)weatherJson["main"]?["temp_max"] ?? null,
            Pressure = (double?)weatherJson["main"]?["pressure"] ?? null,
            Humidity = (double?)weatherJson["main"]?["humidity"] ?? null,

            WindSpeed = (double?)weatherJson["wind"]?["speed"] ?? null,
            Cloudiness = (double?)weatherJson["clouds"]?["all"] ?? null,
            Precipitation = (double?) weatherJson["rain"]?["1h"] ?? null
        };

        return weather;
    }
}