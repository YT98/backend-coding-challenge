namespace CitySearch.Utils;

using  CitySearch.Models;

public interface ICityTrie
{
    public List<City> FindMatches(string query);
}
