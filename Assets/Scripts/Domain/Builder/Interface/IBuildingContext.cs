using System.Collections.Generic;

public interface IBuildingContext
{
    public void BuildSomewhere(Building building);
    public void Build(Building building,float x, float y);
    public List<Building> GetAllBuildings();
    public void RemoveBuilding(Building building);
    public void OnConstructionFinished(Construction construction);
}