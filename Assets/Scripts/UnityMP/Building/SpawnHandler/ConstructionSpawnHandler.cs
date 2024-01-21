using Mirror;
using UnityEngine;
using UnityMP;

public class ConstructionSpawnHandler : ISpawnHandler
{
    private GameObject prefab;

    public void AttachToPrefab(GameObject prefab)
    {
        this.prefab = prefab;
        if (!prefab.TryGetComponent(out NetworkIdentity identity))
        {
            Debug.LogError($"Could not register handler for '{prefab.name}' since it contains no NetworkIdentity component");
            return;
        }
        if (identity.assetId == 0)
        {
            Debug.LogError($"Can not Register handler for '{prefab.name}' because it had empty assetid. If this is a scene Object use RegisterSpawnHandler instead");
            return;
        }
        NetworkClient.RegisterSpawnHandler(identity.assetId, SpawnHandler, UnspawnHandler);
    }


    private GameObject SpawnHandler(SpawnMessage msg)
    {
        GameObject construction = GameObject.Instantiate(prefab);
        construction.transform.position = msg.position;
        construction.transform.rotation = msg.rotation;

        ConstructionController constructionController = construction.GetComponent<ConstructionController>();
        return construction;
    }

    private void UnspawnHandler(GameObject obj)
    {
        GameObject.Destroy(obj);
    }
}