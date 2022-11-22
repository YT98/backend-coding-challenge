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
}