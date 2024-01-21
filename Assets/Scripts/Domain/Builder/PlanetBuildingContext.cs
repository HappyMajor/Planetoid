using System.Collections.Generic;

public class PlanetBuildingContext : IBuildingContext
{

    private Planet planet;

    public delegate void OnBuildBuildingDelegate(OnBuildBuildingEvent ev);
    public event OnBuildBuildingDelegate onBuildBuildingEvent;
    public List<Building> Buildings { get; set; } = new List<Building>();
    public Planet Planet { get => planet; set => planet = value; }
    public PlanetBuildingContext(Planet planet)
    {
        this.Planet = planet;
    }

    public PlanetBuildingContext() { }

    public void Build(Building building, float x, float y)
    {
        if (building != null)
        {
            if(building is Construction construction)
            {
                construction.ConstructionFinishedEvent += OnConstructionFinished;
            }
            this.Planet.AddBuilding(building, new PolarPosition(x, y));
        } else
        {
            throw new System.Exception("Building is null");
        }
    }

    public void BuildSomewhere(Building building)
    {
        if(building != null)
        {
            if(this.Planet.AddBuildingSomewhere(building))
            {
                if (building is Construction construction)
                {
                    construction.ConstructionFinishedEvent += OnConstructionFinished;
                }
                this.Buildings.Add(building);
            } else
            {
                throw new System.Exception("Something went wrong trying to build on the planet...");
            }
            
        } else
        {
            throw new System.Exception("Building is null");
        }
    }

    public List<Building> GetAllBuildings()
    {
        return this.Buildings;
    }

    public void RemoveBuilding(Building building)
    {
        this.planet.RemoveBuilding(building);
    }

    public void OnConstructionFinished(Construction construction)
    {
        if(!this.Buildings.Contains(construction)) throw new System.Exception("Construction Does Not Exist On This Planet BuildingContext");
        if (construction.BuildsTo == null) throw new System.Exception("Construction Builds To Nothing");

        PolarPosition polarPosition = this.Planet.GetPositionOfBuilding(construction);
        this.Buildings.Remove(construction);
        this.RemoveBuilding(construction);
        this.Build(construction.BuildsTo, polarPosition.R, polarPosition.Phi);
    }
}