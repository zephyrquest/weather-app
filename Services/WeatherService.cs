using Newtonsoft.Json.Linq;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherService
{
    private HttpClient _httpClient = new HttpClient();

    public async Task<Weather?> GetCurrentWeatherByCityLocation(double latitude, double longitude, string apiKey)
    {
        var response = await _httpClient.GetAsync(
            @$"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid={apiKey}"
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
            $"https://api.openweathermap.org/data/2.5/weather?q={name}&units=metric&appid={apiKey}"
        );

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        JObject jsonObject = JObject.Parse(content);

        return CreateWeatherObject(jsonObject);
    }

    public async Task<List<DailyForecast>?> GetDailyWeatherForecastsByCityLocation(double latitude, double longitude, string apiKey)
    {
        var response = await _httpClient.GetAsync(
            @$"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&appid={apiKey}"
        );

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        JObject jsonObject = JObject.Parse(content);

        JArray? jsonArray = (JArray?) jsonObject["list"];
        
        if (jsonArray == null || jsonArray.Count == 0)
        {
            return null;
        }

        var weathers = new List<Weather>();
        foreach (var element in jsonArray)
        {
            weathers.Add(CreateWeatherObject((JObject)element));
        }

        // Group weathers by Date
        var weatherForecasts = weathers
            .GroupBy(w => w.LocalTime.Date)
            .Select(g => new DailyForecast 
            {
                Date = g.Key,
                Forecasts = g.ToList()
            })
            .ToList();

        return weatherForecasts;
    }


    private Weather CreateWeatherObject(JObject weatherJson)
    {
        long? seconds = (long?)weatherJson["dt"];
        
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
            Precipitation = (double?) weatherJson["rain"]?["1h"] ?? null,
            
            LocalTime = seconds != null ? DateTimeOffset.FromUnixTimeSeconds(seconds.Value).LocalDateTime : DateTime.MinValue
        };

        return weather;
    }
}