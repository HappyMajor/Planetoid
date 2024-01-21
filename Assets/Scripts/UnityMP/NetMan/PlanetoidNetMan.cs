using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlanetoidNetMan : NetworkManager
{
    public delegate void OnServerAddPlayerDelegate(GameObject player);
    public event OnServerAddPlayerDelegate onServerAddPlayerEvent;

    public PrefabSpawnHandler[] prefabSpawnHandlers;

    public override void Awake()
    {
        this.RegisterSpawnHandler();
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);

        Debug.Log("[NetMan] On Player Add");

        onServerAddPlayerEvent?.Invoke(player);
    }

    private void RegisterSpawnHandler()
    {
        SpawnHandlerFactory factory = new SpawnHandlerFactory();
        foreach(var handler in prefabSpawnHandlers)
        {
            factory.GetSpawnHandler(handler.spawnHandlerId).AttachToPrefab(handler.prefab);
        }
    }
}

[Serializable]
public struct PrefabSpawnHandler
{
    public GameObject prefab;
    public string spawnHandlerId;
}
