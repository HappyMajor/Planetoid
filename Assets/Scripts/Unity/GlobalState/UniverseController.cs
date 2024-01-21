using Mirror;
using Planetoid.Livestock;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UniverseController : NetworkBehaviour
{
    public GameObject planetPrefab;
    public GameObject sunPrefab;
    public GameObject livestockPrefab;

    //Gravitational Constant
    public static float G()
    {
        return 0.001f;
    }

    [SerializeField]
    private Universe universe;
    public Universe Universe { get => universe; set => universe = value; }

    public void Update()
    {
        if(this.universe != null)
        {
            this.universe.Tick();
        }
    }
    public void SetModel(Universe universe)
    {
        this.universe = universe;
        this.CreatePlanets();
        this.CreateSuns();
    }

    public void CreatePlanets()
    {
        foreach(Planet planet in this.universe.Planets)
        {
            this.InstantiatePlanet(planet);
            foreach(Livestock livestock in planet.GetLivestock())
            {
                this.InstantiateLivestock(livestock);
            }
        }
    }

    public void CreateSuns()
    {
        foreach(Sun sun in this.universe.Suns)
        {
            this.InstantiateSun(sun);
        }
    }

    public GameObject InstantiatePlanet(Planet planet)
    {
        GameObject planetObj = GameObject.Instantiate(planetPrefab);
        planetObj.transform.position = new Vector3(planet.Position.x, planet.Position.y, 0);
        planetObj.GetComponent<PlanetModel>().DomainModel = planet;
        planetObj.GetComponent<BuilderModel>().DomainModel = planet.Builder;
        return planetObj;
    }

    public GameObject InstantiateLivestock(Livestock livestock)
    {
        GameObject livestockObj = GameObject.Instantiate(this.livestockPrefab);
        livestockObj.transform.position = new Vector3(livestock.Position.x, livestock.Position.y,0);
        livestockObj.GetComponent<LivestockModel>().DomainModel = livestock;
        return livestockObj;
    }

    public GameObject InstantiateSun(Sun sun)
    {
        GameObject sunObj = GameObject.Instantiate(sunPrefab);
        sunObj.transform.position = new Vector3(sun.Position.x, sun.Position.y, 0);
        sunObj.GetComponent<SunModel>().DomainModel = sun;
        return sunObj;
    }
}
