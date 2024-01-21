using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionController : Controller<Construction>
{
    public override void OnModelChange(Construction model)
    {
        BuildingPrefabResolver buildingPrefabResolver = Component.FindAnyObjectByType<BuildingPrefabResolver>();
        if (buildingPrefabResolver == null) throw new System.Exception("No BuildingPrefabResolver found in scene");

    }

}
