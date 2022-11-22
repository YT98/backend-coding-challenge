namespace CitySearch.Test;

using CitySearch.Utils;
using CitySearch.Models;
using Moq;

public class CityTrieTest
{
    private readonly Mock<IDataLoader> dataLoaderMock;

    public CityTrieTest()
    {
        dataLoaderMock = new Mock<IDataLoader>();
    }

    [Fact]
    public void FindMatches_GivenTwoCitiesOneBeingEqualToQueryAndSubstringOfTheOther_ReturnBoth()
    {
        City city1 = new City("Montreal", "CA", 1.0f, 1.0f);
        City city2 = new City("Mont", "CA", 1.0f, 1.0f);
        List<City> cityList = new List<City> { city1, city2 };

        dataLoaderMock.Setup(x => x.LoadCities()).Returns(cityList);

        CityTrie trie = new CityTrie(dataLoaderMock.Object);

        List<City> matches = trie.FindMatches("Mont");

        Assert.Contains<City>(city1, matches);
        Assert.Contains<City>(city2, matches);
    }

    [Fact]
    public void TestFindMatches_GivenMultipleCitiesWhichContainQuery_ReturnAllCities()
    {
        City city1 = new City("Montreal", "CA", 1.0f, 1.0f);
        City city2 = new City("Mont-Tremblant", "CA", 1.0f, 1.0f);
        List<City> cityList = new List<City> { city1, city2 };

        dataLoaderMock.Setup(x => x.LoadCities()).Returns(cityList);

        CityTrie trie = new CityTrie(dataLoaderMock.Object);

        List<City> matches = trie.FindMatches("Mont");

        Assert.Contains<City>(city1, matches);
        Assert.Contains<City>(city2, matches);
    }

    [Fact]
    public void TestFindMatches_GivenCitiesWhichDoNotContainQuery_ReturnEmptyList()
    {
        City city1 = new City("Montreal", "CA", 1.0f, 1.0f);
        City city2 = new City("Mont-Tremblant", "CA", 1.0f, 1.0f);
        List<City> cityList = new List<City> { city1, city2 };

        dataLoaderMock.Setup(x => x.LoadCities()).Returns(cityList);

        CityTrie trie = new CityTrie(dataLoaderMock.Object);

        List<City> matches = trie.FindMatches("Lon");

        Assert.Empty(matches);
    }

    [Fact]
    public void TestFindMatches_GivenCityEqualToQuery_ReturnCity()
    {
        City city1 = new City("Montreal", "CA", 1.0f, 1.0f);
        List<City> cityList = new List<City> { city1 };

        dataLoaderMock.Setup(x => x.LoadCities()).Returns(cityList);

        CityTrie trie = new CityTrie(dataLoaderMock.Object);

        List<City> matches = trie.FindMatches("Montreal");

        Assert.Contains<City>(city1, matches);
    }
}