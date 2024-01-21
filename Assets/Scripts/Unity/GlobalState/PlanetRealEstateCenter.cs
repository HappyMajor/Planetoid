using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRealEstateCenter
{
    private Dictionary<Planet, Player> planetOwnerDic = new Dictionary<Planet, Player>();

    public Dictionary<Planet, Player> PlanetOwnerDic { get => planetOwnerDic; set => planetOwnerDic = value; }

    public void AddPlanetToPlayer(Planet planet, Player player)
    {
        if(planetOwnerDic.ContainsKey(planet))
        {
            planetOwnerDic[planet] = player;
        }
        else
        {
             planetOwnerDic.Add(planet, player);
        }
    }

    public List<Planet> GetPlanetsOfPlayer(Player player)
    {
        List<Planet> planets = new List<Planet>();
        foreach(Planet planet in planetOwnerDic.Keys)
        {
            Player owner = planetOwnerDic[planet];
            if(owner == player)
            {
                planets.Add(planet);    
            }
        }
        return planets;
    }
}
