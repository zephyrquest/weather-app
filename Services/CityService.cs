using Newtonsoft.Json.Linq;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class CityService
{
    private HttpClient _httpClient = new HttpClient();

    public async Task<List<City>?> GetCitiesByNameInitials(string nameInitials, int maxResults, string username)
    {
        var response = await _httpClient.GetAsync(
            @$"https://secure.geonames.org/search?name_startsWith={nameInitials}&maxRows={maxResults}&type=json&username={username}"
        );
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        JObject jsonObject = JObject.Parse(content);

        var totalCount = (int?)jsonObject["totalResultsCount"] ?? -1;
        var count = totalCount >= 0 && totalCount < maxResults ? totalCount : maxResults;

        var cities = new List<City>();

        for (int i = 0; i < count; i++)
        {
            var name = (string?)jsonObject["geonames"]?[i]?["name"];
            var country = (string?)jsonObject["geonames"]?[i]?["countryName"];
            var latitude = (double?)jsonObject["geonames"]?[i]?["lat"];
            var longitude = (double?)jsonObject["geonames"]?[i]?["lng"];

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(country) && latitude != null && longitude != null)
            {
                cities.Add(new City
                {
                    Name = name,
                    Country = country,
                    Latitude = latitude.Value,
                    Longitude = longitude.Value
                });
            }
        }

        return cities;
    }

    public async Task<string?> GetCountryName(double latitude, double longitude, string username)
    {
        var response = await _httpClient.GetAsync(
            $"https://secure.geonames.org/countryCode?lat={latitude}&lng={longitude}&type=json&username={username}"
            );
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        JObject jsonObject = JObject.Parse(content);

        var countryName = (string?)jsonObject["countryName"];

        return countryName;
    }
    
    public async Task<string?> GetCityName(double latitude, double longitude, string apikey)
    {
        var response = await _httpClient.GetAsync(
            $"https://api.openweathermap.org/geo/1.0/reverse?lat={latitude}&lon={longitude}&appid={apikey}"
        );
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        JArray jsonArray = JArray.Parse(content);
        if (jsonArray.Count == 0)
        {
            return null;
        }
        
        JObject jsonObject = (JObject)jsonArray[0];
        var cityName = (string?)jsonObject["name"];

        return cityName;
    }
}