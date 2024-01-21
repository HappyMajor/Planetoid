using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMP;
using static UnityEditor.Progress;

[RequireComponent(typeof(CircleCollider2D))]
public class BuildingContext : NetworkBehaviour
{
    public readonly SyncList<Building> buildings = new SyncList<Building>();
    private BuildingPrefabResolver buildingPrefabResolver;
    private CircleCollider2D circleCollider;

    public override void OnStartServer()
    {
        this.buildingPrefabResolver = Component.FindAnyObjectByType<BuildingPrefabResolver>();
        this.circleCollider = GetComponent<CircleCollider2D>();
    }

    public override void OnStartClient()
    {
        this.buildings.Callback += OnBuildingUpdated;
    }

    [Server]
    public void SpawnBuilding(Building building, float deg, float radius)
    {
        this.buildings.Add(building);
        this.CreateBuilding(building, deg, radius);
    }

    public void CreateBuilding(Building building, float deg, float radius)
    {
        GameObject buildingPrefab = this.buildingPrefabResolver.Resolve(building);

        if (buildingPrefab != null)
        {
            GameObject buildingObj = GameObject.Instantiate(buildingPrefab, transform);
            buildingObj.transform.localPosition = transform.InverseTransformPoint(MathUtils.CircleLengthDir(this.circleCollider.bounds.center, deg, radius));
            buildingObj.transform.eulerAngles = new Vector3(0, 0, deg);

            if (building is Construction)
            {
                BuildingControllerResolver.SetBuildingData(buildingObj, building);
            }
            else
            {
                throw new System.Exception("Building is not of type construction " + building.GetType().ToString());
            }
        }
        else
        {
            throw new System.Exception("No Building Prefab Found For Type " + building.GetType().ToString());
        }
    }

    void OnBuildingUpdated(SyncList<Building>.Operation op, int index, Building oldBuilding, Building newBuilding)
    {
        Debug.Log("OnBuildingUpdated");
        switch (op)
        {
            case SyncList<Building>.Operation.OP_ADD:
                // index is where it was added into the list
                // newItem is the new item
                Debug.Log("Building added: " + newBuilding.GetType().ToString());
                break;
            case SyncList<Building>.Operation.OP_INSERT:
                // index is where it was inserted into the list
                // newItem is the new item
                break;
            case SyncList<Building>.Operation.OP_REMOVEAT:
                // index is where it was removed from the list
                // oldItem is the item that was removed
                break;
            case SyncList<Building>.Operation.OP_SET:
                // index is of the item that was changed
                // oldItem is the previous value for the item at the index
                // newItem is the new value for the item at the index
                break;
            case SyncList<Building>.Operation.OP_CLEAR:
                // list got cleared
                break;
        }
    }
}
