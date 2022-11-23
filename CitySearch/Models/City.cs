using CitySearch.Utils;
using System.Text.Json.Nodes;

namespace CitySearch.Models;

public class City
{
    public string name;
    public string country;
    public float latitude;
    public float longitude;

    public City(string name, string country, float latitude, float longitude)
    {
        this.name = name;
        this.country = country;
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public JsonObject ToJson()
    {
        return new JsonObject()
        {
            { "name", name },
            { "latitude", latitude },
            { "longitude", longitude }
        };
    }
}

public class ScoredCity : City
{
    public float score;

    public ScoredCity(City city, float latitude, float longitude) : base(city.name, city.country, city.latitude, city.longitude)
    {
        score = CityScorer.GetScore(
            this.latitude, this.longitude,
            latitude, longitude);
    }

    public new JsonObject ToJson()
    {
        return new JsonObject()
        {
            { "name", name },
            { "latitude", latitude },
            { "longitude", longitude },
            { "score", score }
        };
    }
}