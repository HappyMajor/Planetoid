using Mirror;
using PlanetoidMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : NetworkBehaviour
{
    private PlanetoidLogger logger = new PlanetoidLogger(typeof(Commander), LogLevel.DEBUG);

    [SyncVar]
    public string commanderName = "TestPlsIgnore";

    public SyncList<GameObject> ownedPlanets = new SyncList<GameObject>();

    private static Commander localCommander;
    public override void OnStartLocalPlayer()
    {
        localCommander = this;
    }
    public static Commander GetLocalCommander()
    {
        return localCommander;
    }

    [Server]
    public void AssignAuthority(NetworkIdentity target)
    {
        logger.Log($"Assign Authority to {commanderName} over {target.name}");
        target.AssignClientAuthority(netIdentity.connectionToClient);
    }
}
