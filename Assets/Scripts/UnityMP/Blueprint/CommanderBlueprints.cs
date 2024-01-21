using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderBlueprints : NetworkBehaviour
{
    public SyncList<string> availableBlueprints = new SyncList<string>();

    [Server]
    public void AddBlueprint(string id)
    {
        this.availableBlueprints.Add(id);
    }
}
