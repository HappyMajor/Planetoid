using UnityEngine;

public class BuildingControllerResolver
{
    public static void SetBuildingData(GameObject buildingObj, Building building)
    {
        if(building is Construction)
        {
            UnityMP.ConstructionController controller = buildingObj.GetComponent<UnityMP.ConstructionController>();
            if(controller != null)
            {
                controller.Construction = (Construction)building;
            }
        }
    }
}