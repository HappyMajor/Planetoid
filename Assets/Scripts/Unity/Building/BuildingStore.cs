using System.Collections.Generic;

public class BuildingStore
{
    private Dictionary<BuildingType, AbstractBuildingFactory> buildingTypeFactoryDic = new Dictionary<BuildingType, AbstractBuildingFactory>();

    public void RegisterFactory(AbstractBuildingFactory factory, BuildingType buildingType)
    {
        if(this.buildingTypeFactoryDic.ContainsKey(buildingType))
        {
            throw new System.Exception("BuildingType is already registered");
        }

        this.buildingTypeFactoryDic[buildingType] = factory;
    }

    public Building CreateBuilding(BuildingType buildingType)
    {
        if(buildingTypeFactoryDic.ContainsKey(buildingType))
        {
            return buildingTypeFactoryDic[buildingType].GetBuilding();
        } else
        {
            throw new System.Exception("No Factory registered for buildingType: " + buildingType.ToString());
        }
    }
}