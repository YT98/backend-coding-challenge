namespace CitySearch.Utils;

using System.Collections.Generic;
using CitySearch.Models;

public class CityTrie : ICityTrie
{
    static Node root = new Node(' ');
    private readonly IDataLoader _dataLoader;

    public CityTrie(IDataLoader dataLoader)
    {
        _dataLoader = dataLoader;
        foreach (City city in dataLoader.LoadCities())
        {
            InsertCity(city);
        }
    }

    class Node
    {
        public char character;
        public Dictionary<char, Node> children;
        public City? city;

        public Node(char character)
        {
            this.character = character;
            children = new Dictionary<char, Node>();
        }

        public void AddChild(Node child)
        {
            children.Add(child.character, child);
        }

        public Node? GetChildIfKeyExists(char key)
        {
            if (children.ContainsKey(key))
            {
                return children[key];
            }
            else
            {
                return null;
            }
        }

        public Node GetChildIfKeyExistsElseAddChildAndReturn(char key)
        {
            Node? child = GetChildIfKeyExists(key);
            if (child == null)
            {
                Node newNode = new Node(key);
                AddChild(newNode);
                child = newNode;
            }
            return child;
        }

    }

    public List<City> FindMatches(string query)
    {
        Node? node = root;
        foreach (char character in query)
        {
            node = node.GetChildIfKeyExists(character);
            if (node == null) { return new List<City>(); }
        }
        return FindAllSubTreeCities(node);
    }

    private void InsertCity(City city)
    {
        Node node = root;
        foreach (char character in city.name)
        {
            node = node.GetChildIfKeyExistsElseAddChildAndReturn(character);
        }
        node.city = city;
    }

    private List<City> FindAllSubTreeCities(Node node)
    {
        List<City> cities = new List<City>();

        if (node.city != null) { cities.Add(node.city); }
        if (node.children.Count > 0)
        {
            foreach (KeyValuePair<char, Node> entry in node.children)
            {
                cities.AddRange(FindAllSubTreeCities(entry.Value));
            }
        }

        return cities;
    }
}