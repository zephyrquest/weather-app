using System.Reflection;
using System.Text.Json;

namespace WeatherApp.Services;

public class UserConfigService
{
    private Dictionary<string, string>? _configurations;

    
    public bool UserConfigFileExist()
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory,
            "resources",
            "user_config.json");

        return File.Exists(filePath);
    }

    public void ReadDefaultUserConfig()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyName = assembly.GetName().Name;
        Stream? stream = assembly.GetManifestResourceStream($"{assemblyName}.Resources.default_user_config_tmp.json");

        if (stream != null)
        {
            StreamReader streamReader = new StreamReader(stream);
            string jsonContent = streamReader.ReadToEnd();
            _configurations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent) 
                              ?? new Dictionary<string, string>();
        }
        else
        {
            _configurations = new Dictionary<string, string>();
        }
    }

    public void ReadUserConfig()
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory,
            "resources",
            "user_config.json");

        if (!File.Exists(filePath))
        {
            _configurations = new Dictionary<string, string>();
            throw new FileNotFoundException("The user config file has not been found.");
        }

        var jsonContent = File.ReadAllText(filePath);

        try
        {
            _configurations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent) 
                              ?? new Dictionary<string, string>();
        }
        catch
        {
            _configurations = new Dictionary<string, string>();
            throw new FileNotFoundException("The user config file is invalid.");
        }
        
    }
    
    public void WriteUserConfig()
    {
        var jsonContent = JsonSerializer.Serialize(_configurations, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        var directoryPath = Path.Combine(FileSystem.AppDataDirectory, "resources");
        var filePath = Path.Combine(directoryPath, "user_config.json");
        
        Directory.CreateDirectory(directoryPath);
        File.WriteAllText(filePath, jsonContent);
    }

    public string GetConfiguration(string key)
    {
        if (_configurations == null)
        {
            throw new InvalidOperationException("The configurations have not been initialized.");
        }

        var value = "";
        
        try
        {
            value = _configurations[key];
        }
        catch
        {
            throw new KeyNotFoundException($"The configuration {key} was not found.");
        }

        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException($"The configuration value for key {key}  cannot be null or empty.");
        }

        return value;
    }

    public void SetConfiguration(string key, string value)
    {
        if (_configurations == null)
        {
            throw new InvalidOperationException("The configurations have not been initialized.");
        }

        if (!_configurations.Keys.Contains(key))
        {
            throw new KeyNotFoundException($"The resource key '{key}' was not found."); 
        }
        
        _configurations[key] = value;
    }
}