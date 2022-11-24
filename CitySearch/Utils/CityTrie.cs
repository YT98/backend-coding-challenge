namespace CitySearch.Utils;

using System.Collections.Generic;
using CitySearch.Models;

public class CityTrie : ICityTrie
{
    private static readonly Node root = new (' ');

    public CityTrie(IDataLoader dataLoader)
    {
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
            if (children.TryGetValue(key, out Node? value))
            {
                return value;
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
                Node newNode = new (key);
                AddChild(newNode);
                child = newNode;
            }
            return child;
        }

    }

    public List<ScoredCity> FindMatches(string query)
    {
        Node? node = root;
        foreach (char character in query)
        {
            node = node.GetChildIfKeyExists(character);
            if (node == null) { return new List<ScoredCity>(); }
        }
        return FindAllSubTreeCities(node, 1);
    }

    private static void InsertCity(City city)
    {
        Node node = root;
        foreach (char character in city.name)
        {
            node = node.GetChildIfKeyExistsElseAddChildAndReturn(character);
        }
        node.city = city;
    }

    private List<ScoredCity> FindAllSubTreeCities(Node node, int startingDepth)
    {
        // Throw error if depth < 1
        List<ScoredCity> cities = new ();
        int depth = startingDepth;

        if (node.city != null) 
        {
            float resultScore = 1 / (float)depth;
            ScoredCity city = new(node.city, resultScore);
            cities.Add(city);
        }
        if (node.children.Count > 0)
        {
            depth++;
            foreach (KeyValuePair<char, Node> entry in node.children)
            {
                cities.AddRange(FindAllSubTreeCities(entry.Value, depth));
            }
        }

        return cities;
    }
}