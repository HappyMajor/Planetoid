using Mirror;
using PlanetoidMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ownable : NetworkBehaviour
{
    PlanetoidLogger logger = new PlanetoidLogger(typeof(Ownable),LogLevel.DEBUG);
    public delegate void OnOwnerChangeDelegate(Commander oldVal, Commander newVal);
    public event OnOwnerChangeDelegate onOwnerChangeEvent;

    [SyncVar]
    public string ownedBy;

    [SyncVar]
    public Commander ownedByCommander;

    [Server]
    public void SetOwner(Commander commander)
    {
        logger.Log($"SetOwner: new {commander.commanderName}");
        this.ownedBy = commander.commanderName;
        this.ownedByCommander = commander;
        onOwnerChangeEvent?.Invoke(ownedByCommander, commander);
    }

    public bool IsOwnedByMe()
    {
        if (ownedByCommander == null) return false;
        if(Commander.GetLocalCommander() == ownedByCommander.GetComponent<Commander>())
        {
            return true;
        } else
        {
            return false;
        }
    }
}
