using SQLite;
using WeatherApp.Models;

namespace WeatherApp.Repository;

public class CityRepository
{
    private SQLiteAsyncConnection? _database;
    
    public async Task Init()
    {
        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<City>();
    }

    public async Task<List<City>?> GetCities()
    {
        if (_database == null)
        {
            return null;
        }
        
        return await _database.Table<City>().ToListAsync();
    }

    public async Task SaveCity(City city)
    {
        if (_database == null)
        {
            return;
        }
        
        await _database.InsertAsync(city);
    }

    public async Task DeleteCity(City city)
    {
        if (_database == null)
        {
            return;
        }

        await _database.DeleteAsync(city);
    }
}