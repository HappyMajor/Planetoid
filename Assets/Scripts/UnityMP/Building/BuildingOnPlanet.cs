using System;

[Serializable]
public class BuildingOnPlanet
{
    public Building building;
    public float deg;
    public float radius;


    public BuildingOnPlanet(Building building, float deg, float radius)
    {
        this.deg = deg;
        this.radius = radius;
        this.building = building;
    }
    public BuildingOnPlanet()
    {

    }
}