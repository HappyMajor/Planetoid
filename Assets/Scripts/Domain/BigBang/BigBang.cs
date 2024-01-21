using Planetoid.Livestock;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BigBang
{
    private float universeSize = 10f;
    private float densityOfPlanets = 10f;
    private int planetAmount = 12;
    private float solarSystemsAmount = 2f;
    private Universe universe;
    private FactoryManager factoryManager;
    public Universe Universe { get => universe; set => universe = value; }
    public float SolarSystemsAmount { get => solarSystemsAmount; set => solarSystemsAmount = value; }
    public int PlanetAmount { get => planetAmount; set => planetAmount = value; }
    public float DensityOfPlanets { get => densityOfPlanets; set => densityOfPlanets = value; }
    public float UniverseSize { get => universeSize; set => universeSize = value; }
    public FactoryManager FactoryManager { get => factoryManager; private set => factoryManager = value; }

    public BigBang()
    {
        this.factoryManager = new FactoryManager();
    }
    public void StartBigBang()
    {
        this.universe = new Universe();
        Debug.Log("BigBang: StartBigBang");
        this.CreateSolarSystems();
        this.StartSimulation();
    }

    public void CreateSolarSystems()
    {
        Debug.Log("Create Solar Systems...");
        List<Vector3> weightedCoordinates = new List<Vector3>();
        List<Planet> createdPlanets = new List<Planet>();
        List<Sun> createdSuns = new List<Sun>();
        SolarSystem solarSystem = new SolarSystem();
        Debug.Log("Create Suns...");
        for (int x = 0; x < this.universeSize; x++)
        {
            for (int y = 0; y < this.universeSize; y++)
            {
                Vector3 weightedCoordinate = new Vector3(x,y,1);
                weightedCoordinates.Add(weightedCoordinate);
            }
        }
        for(int i = 0; i < this.solarSystemsAmount; i++)
        {
            //random value between 0 and 1 times the sum of all weights
            float randomValue = UnityEngine.Random.value * (weightedCoordinates.Sum((coordinate) => coordinate.z));
            Vector3 selectedWeightedCoordinate = new Vector3(0, 0, 0);
            foreach(Vector3 weightedCoordinate in weightedCoordinates)
            {
                randomValue -= weightedCoordinate.z;
                if(randomValue <= 0)
                {
                    selectedWeightedCoordinate = weightedCoordinate;
                    break;
                }
            }
            Sun sun = this.CreateRandomSun();
            sun.Position = new Vector2(selectedWeightedCoordinate.x, selectedWeightedCoordinate.y);
            createdSuns.Add(sun);
        }

        Debug.Log(this.universe.Suns.Count + " Suns Created");
        Debug.Log("Create Planets Around Suns...");
        
        //create planets around suns
        int planetsToDistribute = this.planetAmount;
        while(planetsToDistribute > 0)
        {
            foreach(Sun sun in createdSuns)
            {
                float randomValue = UnityEngine.Random.value;
                if(randomValue >= 0.5)
                {
                    planetsToDistribute--;
                    Planet planet = this.CreateRandomPlanetAroundSun(sun);
                    sun.OrbitingPlanets.Add(planet);
                    createdPlanets.Add(planet);
                }
            }
        }
        Debug.Log(this.universe.Planets.Count + " Planets Created");
        
        this.universe.Suns.AddRange(createdSuns);
        this.universe.Planets.AddRange(createdPlanets);
        this.universe.SolarSystems.Add(solarSystem);
        solarSystem.Planets = createdPlanets;
        solarSystem.Suns = createdSuns;
    }

    public Planet CreateRandomPlanetAroundSun(Sun sun)
    {
        Planet planet = this.CreateRandomPlanet();

        // Random distance between 10 and 100 in million Km * 10
        float distance = UnityEngine.Random.Range(10f, 600f);

        // Random angle in radians
        float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);

        // Calculate the planet's position relative to the sun
        float planetX = sun.Position.x + distance * Mathf.Cos(angle);
        float planetY = sun.Position.y + distance * Mathf.Sin(angle);

        // Set the planet's position
        planet.Position = new Vector2(planetX, planetY);


        Livestock livestock = this.CreateRandomLivestock();
        livestock.Position = planet.Position + planet.FindFreeRect(1, 1, 2f, 1f).Cartesian();
        planet.AddLivestock(livestock);

        return planet;
    }

    public void CreatePlanets()
    {
        for(int i = 0; i < planetAmount; i++)
        {
            Planet planet = this.CreateRandomPlanet();
            this.universe.Planets.Add(planet);
        }
    }

    public void StartSimulation()
    {

    }

    public Sun CreateRandomSun()
    {
        Sun sun = new Sun();
        sun.Mass = RandomUtil.RangeTriangularDistribution(333000, 666000);
        sun.Velocity = new Vector2 (69, 1);
        return sun;
    }

    public Planet CreateRandomPlanet()
    {
        Planet planet = this.factoryManager.PlanetFactory.CreatePlanet();
        planet.Oxygen = RandomUtil.RangeTriangularDistribution(30, 80);
        planet.Co2 = 100 - planet.Oxygen;
        planet.Pollution = 0;
        planet.Radius = RandomUtil.RangeTriangularDistribution(5, 13);
        planet.Gravity = RandomUtil.RangeTriangularDistribution(3, 10) * (planet.Radius/10);
        planet.Spin = RandomUtil.RangeTriangularDistribution(1, 8);
        planet.Temperature = 273;
        planet.Mass = RandomUtil.RangeTriangularDistribution(1, 5);
        planet.SpecificHeatCapacity = Constants.HEAT_CAPACITY_WATER;
        planet.Emissivity = 0.7f;
        planet.InitializePlanet();

        return planet;
    }

    public Livestock CreateRandomLivestock()
    {
        Livestock livestock = new Livestock();
        livestock.AddAttribute(new Attribute(AttributeType.CRAFT, 1f));
        livestock.Name = "Jack";
        livestock.Description = "No Description";
        return livestock;
    }
}
