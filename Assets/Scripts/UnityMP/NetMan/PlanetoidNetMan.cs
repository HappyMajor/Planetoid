using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlanetoidNetMan : NetworkManager
{
    public delegate void OnServerAddPlayerDelegate(GameObject player);
    public event OnServerAddPlayerDelegate onServerAddPlayerEvent;
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
}