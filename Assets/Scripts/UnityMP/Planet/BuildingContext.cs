using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMP;

[RequireComponent(typeof(CircleCollider2D))]
public class BuildingContext : NetworkBehaviour
{
    public SyncList<Building> buildings = new SyncList<Building>();
    private BuildingPrefabResolver buildingPrefabResolver;
    private CircleCollider2D circleCollider;

    public override void OnStartServer()
    {
        this.buildingPrefabResolver = Component.FindAnyObjectByType<BuildingPrefabResolver>();
        this.circleCollider = GetComponent<CircleCollider2D>();
    }

    [Server]
    public void CreateBuilding(Building building, float deg, float radius)
    {
        GameObject buildingPrefab = this.buildingPrefabResolver.Resolve(building);

        if(buildingPrefab != null )
        {
            GameObject buildingObj = GameObject.Instantiate(buildingPrefab, transform);
            NetworkServer.Spawn(buildingObj);
            buildingObj.transform.localPosition = transform.InverseTransformPoint(MathUtils.CircleLengthDir(this.circleCollider.bounds.center, deg, radius));
            buildingObj.transform.eulerAngles = new Vector3(0, 0, deg);
            if(building is Construction)
            {
                BuildingControllerResolver.SetBuildingData(buildingObj, building);
            } else
            {
                throw new System.Exception("Building is not of type construction " + building.GetType().ToString());
            }
        } else
        {
            throw new System.Exception("No Building Prefab Found For Type " + building.GetType().ToString());
        }
    }
}
