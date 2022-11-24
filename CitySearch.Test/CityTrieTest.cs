namespace CitySearch.Test;

using CitySearch.Utils;
using CitySearch.Models;
using Moq;
using Castle.Components.DictionaryAdapter;

public class CityTrieTest
{
    private readonly Mock<IDataLoader> dataLoaderMock;

    public CityTrieTest()
    {
        dataLoaderMock = new Mock<IDataLoader>();
    }

    private List<string> CityListAsStrings(List<ScoredCity> cityList)
    {
        List<string> stringList = new();
        foreach (ScoredCity city in cityList)
        {
            stringList.Add(city.ToString());
        }
        return stringList;
    }

    [Fact]
    public void FindMatches_GivenTwoCitiesOneBeingEqualToQueryAndSubstringOfTheOther_ReturnBoth()
    {
        City city1 = new City("Montreal", "CA", 1.0f, 1.0f);
        City city2 = new City("Mont", "CA", 1.0f, 1.0f);
        List<City> cityList = new List<City> { city1, city2 };

        dataLoaderMock.Setup(x => x.LoadCities()).Returns(cityList);

        CityTrie trie = new CityTrie(dataLoaderMock.Object);

        List<ScoredCity> matches = trie.FindMatches("Mont");
        List<string> matchesAsStrings = CityListAsStrings(matches);

        ScoredCity scoredCity1 = new(city1, 1 / 5);
        ScoredCity scoredCity2 = new(city2, 1f);

        Assert.Contains(scoredCity1.ToString(), matchesAsStrings);
        Assert.Contains(scoredCity2.ToString(), matchesAsStrings);
    }

    [Fact]
    public void FindMatches_GivenMultipleCitiesWhichContainQuery_ReturnAllCities()
    {
        City city1 = new City("Montr", "CA", 1.0f, 1.0f);
        City city2 = new City("Monta", "CA", 1.0f, 1.0f);
        List<City> cityList = new List<City> { city1, city2 };

        dataLoaderMock.Setup(x => x.LoadCities()).Returns(cityList);

        CityTrie trie = new CityTrie(dataLoaderMock.Object);

        List<ScoredCity> matches = trie.FindMatches("Mont");
        List<String> matchesAsStrings = CityListAsStrings(matches);

        ScoredCity scoredCity1 = new(city1, 1f);
        ScoredCity scoredCity2 = new(city2, 1f);

        Assert.Contains(scoredCity1.ToString(), matchesAsStrings);
        Assert.Contains(scoredCity2.ToString(), matchesAsStrings);
    }

    [Fact]
    public void FindMatches_GivenCitiesWhichDoNotContainQuery_ReturnEmptyList()
    {
        City city1 = new City("Montreal", "CA", 1.0f, 1.0f);
        City city2 = new City("Mont-Tremblant", "CA", 1.0f, 1.0f);
        List<City> cityList = new List<City> { city1, city2 };

        dataLoaderMock.Setup(x => x.LoadCities()).Returns(cityList);

        CityTrie trie = new CityTrie(dataLoaderMock.Object);

        List<ScoredCity> matches = trie.FindMatches("Lon");

        Assert.Empty(matches);
    }

    [Fact]
    public void FindMatches_GivenCityEqualToQuery_ReturnCity()
    {
        City city1 = new City("Montreal", "CA", 1.0f, 1.0f);
        List<City> cityList = new List<City> { city1 };

        dataLoaderMock.Setup(x => x.LoadCities()).Returns(cityList);

        CityTrie trie = new CityTrie(dataLoaderMock.Object);

        List<ScoredCity> matches = trie.FindMatches("Montreal");
        List<String> matchesAsStrings = CityListAsStrings(matches);

        ScoredCity scoredCity1 = new(city1, 1);

        Assert.Contains(scoredCity1.ToString(), matchesAsStrings);
    }
}