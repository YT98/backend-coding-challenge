namespace CitySearch.Utils;

using System.IO;
using System.Collections.Generic;
using CitySearch.Models;

public class DataLoader : IDataLoader
{
    private string FILE_PATH;
    private int NAME_INDEX;
    private int COUNTRY_INDEX;
    private int LATITUDE_INDEX;
    private int LONGITUDE_INDEX;
    
    public List<City> cityList;

    public DataLoader(IConfiguration configuration)
    {
        FILE_PATH = configuration.GetValue<string>("DATA_FILE_PATH");
        NAME_INDEX = configuration.GetValue<int>("DATA_NAME_INDEX");
        COUNTRY_INDEX = configuration.GetValue<int>("DATA_COUNTRY_INDEX");
        LATITUDE_INDEX = configuration.GetValue<int>("DATA_LATITUDE_INDEX");
        LONGITUDE_INDEX = configuration.GetValue<int>("DATA_LONGITUDE_INDEX");
        cityList = LoadCities();
    }

    public List<City> LoadCities()
    {
        List<City> cityList = new List<City>();
        IEnumerable<string[]> dataFileLines = ReadDataFileValues();
        foreach (string[] values in dataFileLines)
        {
            cityList.Add(InstanciateCityFromValues(values));
        }
        return cityList;
    }

    private City InstanciateCityFromValues(string[] values)
    {
        return new City(
            name: values[NAME_INDEX],
            country: values[COUNTRY_INDEX],
            latitude: float.Parse(values[LATITUDE_INDEX]),
            longitude: float.Parse(values[LONGITUDE_INDEX])
            );
    }

    private IEnumerable<string[]> ReadDataFileValues()
    {
        using (var reader = new StreamReader(FILE_PATH))
        {
            var firstLine = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                // line may be null
                var values = line.Split("\t");
                yield return values;
            }
        }
    }
}