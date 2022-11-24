namespace CitySearch.Utils;

using  CitySearch.Models;

public interface ICityTrie
{
    public List<ScoredCity> FindMatches(string query);
}
