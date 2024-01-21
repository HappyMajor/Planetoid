using UnityEngine;

public class BuildingPrefabResolver : MonoBehaviour
{
    public GameObject constructionPrefab;
    public GameObject shelterPrefab;
    public GameObject hqPrefab;

    public void Start()
    {
        if (constructionPrefab == null) throw new System.Exception("ConstructionPrefab not set");
        if (shelterPrefab == null) throw new System.Exception("ShelterPrefab not set");
        if (hqPrefab == null) throw new System.Exception("HqPrefab not set");
    }
    public GameObject Resolve(Building building)
    {
        if (building == null) throw new System.Exception("Building Is Null");
        
        if(building is Construction)
        {
            return constructionPrefab;
        }

        if(building is Shelter)
        {
            return shelterPrefab;
        }

        if(building is HQ)
        {
            return hqPrefab;
        }

        throw new System.Exception("No Building Prefab Found For Type " + building.GetType());
    }
}