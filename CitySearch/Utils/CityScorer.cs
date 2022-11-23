namespace CitySearch.Utils;

using CitySearch.Models;
using System.Text.Json.Nodes;

public static class CityScorer
{
    public static float GetScore(float cityLat, float cityLon, float locationLat, float locationLon)
    {
        float latDelta = Math.Abs(cityLat - locationLat);
        float latScore = latDelta / 90f / 2f;

        float lonDelta = Math.Abs(cityLon - locationLon);
        float lonScore = lonDelta / 90f / 2f;

        return Math.Abs(latScore + lonScore - 1);
    }
}
