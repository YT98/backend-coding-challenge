namespace CitySearch.Utils;

public static class CityScorer
{
    public static float GetScore(float cityLat, float cityLon, float locationLat, float locationLon)
    {
        float maxLat = 90f;
        float latDelta = Math.Abs(cityLat - locationLat);
        float latScore = latDelta / (maxLat*2) / 2;

        float maxLon = 180f;
        float lonDelta = Math.Abs(cityLon - locationLon);
        float lonScore = lonDelta / (maxLon*2) / 2;

        return Math.Abs(latScore + lonScore - 1);
    }
}
