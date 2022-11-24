using CitySearch.Controllers;
using CitySearch.Models;
using CitySearch.Utils;
using Moq;
using System.Text.Json.Nodes;

namespace CitySearch.Test;

public class SuggestionsControllerTest
{
    private readonly Mock<ICityTrie> _mockCityTrie = new();

    [Fact]
    public void Get_ReturnListOrderedByDescendingLocationScore()
    {
        (float, float) location = (0.0f, 0.0f);
        City city1 = new ("Montr", "CA", 1.0f, 1.0f);
        City city2 = new ("Monta", "CA", 2.0f, 2.0f);
        City city3 = new ("Montb", "CA", 3.0f, 3.0f);
        ScoredCity scoredCity1 = new (city1, 0.5f, location.Item1, location.Item2);
        ScoredCity scoredCity2 = new (city2, 0.5f, location.Item1, location.Item2);
        ScoredCity scoredCity3 = new (city3, 0.5f, location.Item1, location.Item2);

        _mockCityTrie.Setup(x => x.FindMatches(It.IsAny<string>())).Returns(
                new List<ScoredCity>() { scoredCity2, scoredCity3, scoredCity1 }
            );

        SuggestionsController suggestionsController = new (_mockCityTrie.Object);

        List<JsonObject> result = suggestionsController.Get("Mont", location.Item1, location.Item2);

        string scoredCity1String = scoredCity1.ToJson().ToString();
        string scoredCity2String = scoredCity2.ToJson().ToString();
        string scoredCity3String = scoredCity3.ToJson().ToString();

        string result1 = result[0].ToString();
        string result2 = result[1].ToString();
        string result3 = result[2].ToString();

        Assert.Equal(3, result.Count());
        Assert.Equal(scoredCity1String, result1);
        Assert.Equal(scoredCity2String, result2);
        Assert.Equal(scoredCity3String, result3);
    }
}