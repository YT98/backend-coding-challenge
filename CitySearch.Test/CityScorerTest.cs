using CitySearch.Utils;
using CitySearch.Models;

namespace CitySearch.Test;

public class CityScorerTest
{
    [Fact]
    public void GetScore_GivenTwoCitiesDifferentDistancesToLocation_ReturnHigherScoreForClosest()
    {
        (float, float) location = (0f, 0f);
        City city1 = new City("Montreal", "CA", 1.0f, 1.0f);
        City city2 = new City("Montreal", "CA", 2.0f, 2.0f);

        float city1Score = CityScorer.GetScore(
            cityLat: city1.latitude, cityLon: city1.longitude,
            locationLat: location.Item1, locationLon: location.Item2
            );
        float city2Score = CityScorer.GetScore(
            cityLat: city2.latitude, cityLon: city2.longitude,
            locationLat: location.Item1, locationLon: location.Item2
            );

        Assert.True(city1Score > city2Score);
    }

    [Fact]
    public void GetScore_GivenTwoCitiesSameDistanceToLocation_ReturnSameScore()
    {
        (float, float) location = (1.0f, 1.0f);
        City city1 = new City("Montreal", "CA", 0f, 0f);
        City city2 = new City("Montreal", "CA", 2.0f, 2.0f);
        List<City> cityList = new List<City> { city1, city2 };

        float city1Score = CityScorer.GetScore(
            cityLat: city1.latitude, cityLon: city1.longitude,
            locationLat: location.Item1, locationLon: location.Item2
            );
        float city2Score = CityScorer.GetScore(
            cityLat: city2.latitude, cityLon: city2.longitude,
            locationLat: location.Item1, locationLon: location.Item2
            );

        Assert.True(city1Score == city2Score);
    }
}