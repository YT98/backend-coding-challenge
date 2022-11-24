using CitySearch.Models;
using CitySearch.Utils;
using Microsoft.AspNetCore.Mvc;
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

    private static List<ScoredCity> SortCityList(List<ScoredCity> cityList)
    {
        List<ScoredCity> scoredCityList = new();
        foreach (ScoredCity city in cityList)
        {
            scoredCityList.Add(city);
        }
        scoredCityList.Sort((x, y) => x.GetScore().CompareTo(y.GetScore()));
        scoredCityList.Reverse();
        return scoredCityList;
    }

    private static void SetLocationScoreCityList(List<ScoredCity> cityList, float latitude, float longitude)
    {
        foreach (ScoredCity city in cityList)
        {
            city.SetLocationScore(latitude, longitude);
        }
    }

    [HttpGet(Name="Suggestions")]
    public List<JsonObject> Get(string q, float? latitude = null, float? longitude = null)
    {
        List<ScoredCity> cityList = _cityTrie.FindMatches(q);
        if (latitude != null && longitude != null)
        {
            SetLocationScoreCityList(cityList, (float)latitude, (float)longitude);
        }
        List<ScoredCity> scoredCityList = SortCityList(cityList);

        return ConvertScoredCityListToJson(scoredCityList);
    }
}