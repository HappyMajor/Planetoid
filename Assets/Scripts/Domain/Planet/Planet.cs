using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Planetoid.Livestock;

[Serializable]
public class Planet : StandardPhysicalEntity
{
    private Dictionary<PolarPosition, Building> buildings = new Dictionary<PolarPosition, Building>();
    private Builder builder;
    private List<Livestock> livestocks = new();
    private float angleSteps = 0;
    private float surface = 0;
    private OnBuildBuildingDelegate onBuildBuilding;

    [SerializeField]
    private float gravity = 0;

    [SerializeField]
    private float spin = 0;

    [SerializeField]
    private float pollution = 0;

    [SerializeField]
    private float oxygen = 0;

    [SerializeField]
    private float co2 = 0;

    public float Gravity { get => gravity; set => gravity = value; }
    public float Spin { get => spin; set => spin = value; }
    public float Pollution { get => pollution; set => pollution = value; }
    public float Oxygen { get => oxygen; set => oxygen = value; }
    public float Co2 { get => co2; set => co2 = value; }
    public float Surface { get => surface; set => surface = value; }
    public float AngleSteps { get => angleSteps; set => angleSteps = value; }
    public Builder Builder { get => builder; set => builder = value; }
    public OnBuildBuildingDelegate OnBuildBuilding { get => onBuildBuilding; set => onBuildBuilding = value; }

    public delegate void OnBuildBuildingDelegate(OnBuildBuildingEvent ev);

    public Planet(Builder builder)
    {
        this.builder = builder;
    }

    public Planet() { }

    private void GenerateCoordinates()
    {
        float surface = this.Radius * 2f * Mathf.PI;
        float angleSteps = 1f / (surface / 360f);
        this.angleSteps = angleSteps;
        this.surface = surface; 
    }

    public void InitializePlanet()
    {
        this.GenerateCoordinates();
    }

    public void AddBuilding(Building building, PolarPosition polarPosition)
    {
        this.buildings.Add(polarPosition, building);
        Debug.Log("Building placed");
        this.OnBuildBuilding?.Invoke(new OnBuildBuildingEvent(building));
    }

    public PolarPosition FindFreeRect(float width, float height, float radius, float stepSize)
    {
        for(float phi = 0; phi < 360; phi += stepSize)
        {
            PolarPosition polarPosition = new PolarPosition(radius, phi);
            if(this.IsFreeRect(polarPosition, width, height)) return polarPosition;
        }

        return null;
    }

    public bool IsFreeRect(PolarPosition start, float width, float height)
    {
        PolarPosition topRight = new PolarPosition(start.R + height, start.Phi);
        PolarPosition topLeft = new PolarPosition(start.R + height, start.Phi + this.angleSteps * width);
        PolarPosition bottomRight = new PolarPosition(start.R, start.Phi);
        PolarPosition bottomLeft = new PolarPosition(start.R, start.Phi + this.angleSteps * width);

        if (this.BuildingAt(topRight).Count > 0) return false;
        if (this.BuildingAt(topLeft).Count > 0) return false;
        if (this.BuildingAt(bottomRight).Count > 0) return false;
        if (this.BuildingAt(bottomLeft).Count > 0) return false;

        return true;
    }
    public List<Building> BuildingAt(PolarPosition polarPosition)
    {
        List<Building> foundBuildings = new List<Building>();
        foreach(PolarPosition buildingPosition in this.buildings.Keys)
        {
            Building building = this.buildings[buildingPosition];
            //check if it is in between its height
            if(polarPosition.R >= buildingPosition.R && polarPosition.R <= buildingPosition.R + building.Height)
            {
                //check if it is inside its start and end angle
                if(polarPosition.Phi >= buildingPosition.Phi && polarPosition.Phi <= buildingPosition.Phi + building.Width * this.angleSteps)
                {
                    foundBuildings.Add(building);
                }
            }
        }
        return foundBuildings;
    }

    public bool AddBuildingSomewhere(Building building)
    {
        //find free position in 1 degree steps
        PolarPosition polarPosition = this.FindFreeRect(building.Width, building.Height, this.Radius, 1);
        Debug.Log("AddBuildingSomewhere polarposition: " + polarPosition.R + " " + polarPosition.Phi);
        if (polarPosition == null) {
            Debug.Log("No Free Space found for building");
            return false;
        } else
        {
            this.buildings[polarPosition] = building;
            Debug.Log("Building placed");
            this.OnBuildBuilding?.Invoke(new OnBuildBuildingEvent(building));
            return true;
        }
    }

    public PolarPosition GetPositionOfBuilding(Building building)
    {
        if(this.buildings.ContainsValue(building))
        {
            foreach(PolarPosition polarPosition in this.buildings.Keys)
            {
                if (buildings[polarPosition] == building)
                {
                    return polarPosition;
                }
            }
        }
        return null;
    }

    public void RemoveBuilding(Building building)
    {

    }

    public void AddLivestock(Livestock livestock)
    {
        this.livestocks.Add(livestock);
    }

    public void RemoveLivestock(Livestock livestock)
    {
        this.livestocks.Remove(livestock);
    }

    public List<Livestock> GetLivestock()
    {
        return this.livestocks;
    }

    public override void OnCollide(CollisionEvent collisionEvent)
    {
        throw new NotImplementedException();
    }
}
