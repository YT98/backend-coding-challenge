using CitySearch.Models;
using CitySearch.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CitySearch.Controllers;

[ApiController]
[Route("suggestions")]
public class SuggestionsController : ControllerBase
{
    private readonly ICityTrie _cityTrie;
    
    public SuggestionsController(ICityTrie cityTrie)
    {
        _cityTrie = cityTrie;
    }

    private static List<JsonObject> ConvertScoredCityListToJson(List<ScoredCity> scoredCityList)
    {
        List<JsonObject> result = new();
        foreach (ScoredCity city in scoredCityList)
        {
            result.Add(city.ToJson());
        }
        return result;
    }

    private static List<ScoredCity> ScoreAndSortCityList(List<City> cityList, float latitude, float longitude)
    {
        List<ScoredCity> scoredCityJsonArray = new();
        foreach (City city in cityList)
        {
            scoredCityJsonArray.Add(ScoreCity(city: city, latitude: latitude, longitude: longitude));
        }
        scoredCityJsonArray.Sort((x, y) => x.score.CompareTo(y.score));
        scoredCityJsonArray.Reverse();
        return scoredCityJsonArray;
    }

    private static ScoredCity ScoreCity(City city, float latitude, float longitude)
    {
        return new ScoredCity(city: city, latitude: latitude, longitude: longitude);
    }

    [HttpGet(Name="Suggestions")]
    public List<JsonObject> Get(string q, float latitude, float longitude)
    {
        List<City> cityList = _cityTrie.FindMatches(q);
        List<ScoredCity> scoredCityList = ScoreAndSortCityList(cityList, latitude, longitude);
        return ConvertScoredCityListToJson(scoredCityList);
    } 
}