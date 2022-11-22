namespace CitySearch.Utils;

using CitySearch.Models;

public interface IDataLoader
{
    public List<City> LoadCities();
}
