using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMP;
using static UnityEditor.Progress;

[RequireComponent(typeof(CircleCollider2D))]
public class BuildingContext : NetworkBehaviour
{
    public readonly SyncList<BuildingOnPlanet> buildings = new SyncList<BuildingOnPlanet>();
    public Dictionary<BuildingOnPlanet, GameObject> buildingObjects = new Dictionary<BuildingOnPlanet, GameObject>();
    private BuildingPrefabResolver buildingPrefabResolver;
    private CircleCollider2D circleCollider;

    public void Start()
    {
        this.buildingPrefabResolver = Component.FindAnyObjectByType<BuildingPrefabResolver>();
        this.circleCollider = GetComponent<CircleCollider2D>();
    }
    public override void OnStartServer()
    {

    }

    public override void OnStartClient()
    {
        this.buildings.Callback += OnBuildingUpdated;
    }

    public PolarPosition GetPolarpositionOfBuilding(Building building)
    {
        BuildingOnPlanet buildingOnPlanet = (this.buildings.Find(b => b.building == building));
        if(buildingOnPlanet != null)
        {
            return new PolarPosition(buildingOnPlanet.radius, buildingOnPlanet.deg);
        } else
        {
            return null;
        }
    }

    /*
     * For spawning buildings with absolute degrees. This means the world space angle compared to the planet 
     * without considering its rotation. (e.g top of planet 90 deg, left to the planet 180 etc. no matter the rotation)
     */
    [Server]
    public void SpawnBuildingAbsoluteDeg(Building building, float deg, float radius)
    {
        Debug.Log("abs deg: " + deg);
        float relativeAngle = this.GetRelativeAngle(deg);
        Debug.Log("relative deg: " + relativeAngle);
        Debug.Log("transform angle: " + this.transform.rotation.z);
        this.SpawnBuilding(building, relativeAngle, radius);
    }

    [Server]
    public void SpawnBuilding(Building building, float relativeAngle, float radius)
    {
        BuildingOnPlanet buildingOnPlanet = new BuildingOnPlanet(building, relativeAngle, radius);
        this.buildings.Add(buildingOnPlanet);
        this.CreateBuilding(buildingOnPlanet, relativeAngle, radius);
    }

    public BuildingOnPlanet GetBuildingOnPlanet(Building building)
    {
        return this.buildings.Find(b => b.building == building);
    }

    [Server]
    public void UnspawnBuilding(Building building)
    {
        BuildingOnPlanet buildingOnPlanet = this.GetBuildingOnPlanet((Building)building);
        if(buildingOnPlanet != null)
        {
            this.UnspawnBuilding(buildingOnPlanet);
        } else
        {
            throw new System.Exception("No Building On Planet found for the building to unspawn");
        }
    }

    [Server]
    public void UnspawnBuilding(BuildingOnPlanet buildingOnPlanet)
    {
        //destroy on server
        this.DestroyBuilding(buildingOnPlanet);
        //will call Destroy building on client (onBuildingUpdated)
        this.buildings.Remove(buildingOnPlanet);
    }

    public void DestroyBuilding(BuildingOnPlanet buildingOnPlanet)
    {
        GameObject buildingObj;
        if(this.buildingObjects.TryGetValue(buildingOnPlanet, out buildingObj))
        {
            GameObject.Destroy(this.buildingObjects[buildingOnPlanet]);
            this.buildingObjects.Remove(buildingOnPlanet);
        } else
        {
            throw new System.Exception("No Building found to destroy!");
        }
    }

    public void CreateBuilding(BuildingOnPlanet buildingOnPlanet, float relativeAngle, float radius)
    {
        Building building = buildingOnPlanet.building;
        GameObject buildingPrefab = this.buildingPrefabResolver.Resolve(building);

        if (buildingPrefab != null)
        {
            float angle = this.RelativeToAbsolute(relativeAngle);
            GameObject buildingObj = GameObject.Instantiate(buildingPrefab, transform);
            buildingObj.transform.localPosition = transform.InverseTransformPoint(MathUtils.CircleLengthDir(this.circleCollider.bounds.center, angle, radius));
            buildingObj.transform.eulerAngles = new Vector3(0, 0, angle);
            this.buildingObjects.Add(buildingOnPlanet, buildingObj);
            this.SetBuildingDataOfBuildingController(buildingObj, building);
        }
        else
        {
            throw new System.Exception("No Building Prefab Found For Type " + building.GetType().ToString());
        }
    }

    public void SetBuildingDataOfBuildingController(GameObject obj, Building building)
    {
        IBuildingController controller;
        if(obj.TryGetComponent<IBuildingController>(out controller))
        {
            controller.SetBuilding(building);
        } else
        {
            throw new System.Exception("No Building Controller on building prefab");
        }
    }

    /*
     * no idea why this works. but remember that unity works in weird -180 to 180 deg. from right top left is 180. from right bottom left is -180
     */
    public float GetRelativeAngle(float deg)
    {
        return (deg - this.transform.rotation.eulerAngles.z) % 360;
    }

    public float RelativeToAbsolute(float relativeAngle)
    {
        return (this.transform.rotation.eulerAngles.z + relativeAngle) % 360;
    }

    void OnBuildingUpdated(SyncList<BuildingOnPlanet>.Operation op, int index, BuildingOnPlanet oldBuildingOnPlanet, BuildingOnPlanet newBuildingOnPlanet)
    {
        if (!this.isClientOnly) return;
       
        switch (op)
        {
            case SyncList<BuildingOnPlanet>.Operation.OP_ADD:
                // index is where it was added into the list
                // newItem is the new item
                if(this.isClientOnly)
                {
                    Debug.Log("SyncList BuildingOnPlanet added sync: " + newBuildingOnPlanet.GetType().ToString());
                    if (newBuildingOnPlanet.building == null) throw new System.Exception("SyncList Building is null: bad data provided!");
                    this.CreateBuilding(newBuildingOnPlanet, newBuildingOnPlanet.deg, newBuildingOnPlanet.radius);
                }
                break;
            case SyncList<BuildingOnPlanet>.Operation.OP_INSERT:
                // index is where it was inserted into the list
                // newItem is the new item
                break;
            case SyncList<BuildingOnPlanet>.Operation.OP_REMOVEAT:
                // index is where it was removed from the list
                // oldItem is the item that was removed
                if(this.isClientOnly)
                {
                    Debug.Log("SyncList BuildingOnPlanet removed sync");
                    this.DestroyBuilding(oldBuildingOnPlanet);
                }
                break;
            case SyncList<BuildingOnPlanet>.Operation.OP_SET:
                // index is of the item that was changed
                // oldItem is the previous value for the item at the index
                // newItem is the new value for the item at the index
                break;
            case SyncList<BuildingOnPlanet>.Operation.OP_CLEAR:
                // list got cleared
                break;
        }
    }
}
