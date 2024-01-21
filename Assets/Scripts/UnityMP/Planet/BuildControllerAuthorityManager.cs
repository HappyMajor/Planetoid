using Mirror;
using PlanetoidMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ownable))]
[RequireComponent (typeof(BuildController))]
public class BuildControllerAuthorityManager : NetworkBehaviour
{
    private PlanetoidLogger logger = new PlanetoidLogger(typeof(BuildControllerAuthorityManager), LogLevel.DEBUG);
    private BuildController buildController;
    private PlanetoidNetMan netMan;
    private Ownable ownable;
    public override void OnStartServer()
    {
        logger.Log("OnStartServer");
        netMan = Component.FindAnyObjectByType<PlanetoidNetMan>();
        ownable = GetComponent<Ownable>();
        buildController = GetComponent<BuildController>();

        ownable.onOwnerChangeEvent += OnOwnerChange;
    }

    [Server]
    public void OnOwnerChange(Commander oldCommander , Commander newCommander)
    {
        newCommander.AssignAuthority(netIdentity);
        logger.Log($"OnOwnerChange: old {oldCommander?.commanderName} new {newCommander.commanderName}");
    }
}
