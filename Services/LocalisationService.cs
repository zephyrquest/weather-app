namespace WeatherApp.Services;

public class LocalisationService
{
    public async Task<bool> HasLocationPermission()
    {
        var permissions = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        return permissions == PermissionStatus.Granted;
    }

    public async Task<Location?> GetCurrentLocation()
    {
        var locationRequest = new GeolocationRequest(GeolocationAccuracy.Best);
        var location = await Geolocation.GetLocationAsync(locationRequest);

        return location;
    }
}