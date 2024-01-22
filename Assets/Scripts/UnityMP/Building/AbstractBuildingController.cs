using UnityEngine;

public abstract class AbstractBuildingController<BUILDING_TYPE> : MonoBehaviour, IBuildingController
{
    protected BUILDING_TYPE building;
    public void SetBuilding(Building building)
    {
        if(building is BUILDING_TYPE buildingType)
        {
            this.building = buildingType;
            this.OnBuildingDataSet(buildingType);
        } else
        {
            throw new System.Exception("Bad building data provided to build controller. Required type is: " + typeof(BUILDING_TYPE) + " but is: " + building.GetType());
        }
    }
    public abstract void OnBuildingDataSet(BUILDING_TYPE building);
} 