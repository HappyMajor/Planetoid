using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using PlanetoidMP;
using UnityEngine;

public class GameMode : NetworkBehaviour
{
    public PlanetoidLogger logger = new PlanetoidLogger(typeof(GameMode), LogLevel.DEBUG);
    public PlanetoidNetMan NetworkManager;

    public GameObject[] planets;

    private Blueprints blueprints;

    public List<Commander> commanders = new List<Commander>();

    [SerializeField]
    public string[] startBlueprints;

    public override void OnStartClient()
    {
        blueprints = Blueprints.GetInstance();
    }

    public override void OnStartServer()
    {
        NetworkManager.onServerAddPlayerEvent += OnServerAddPlayer;
    }

    public void OnServerAddPlayer(GameObject player)
    {
        this.InitPlayer(player);
        this.commanders.Add(player.GetComponent<Commander>());
        this.AssignStartPlanets();
    }

    public void InitPlayer(GameObject player)
    {
        logger.Log("Init player " + player.name + "...");
        logger.Log("Add start blueprints...");
        CommanderBlueprints commanderBlueprints = player.GetComponent<CommanderBlueprints>();
        foreach (string id in startBlueprints)
        {
            logger.Log("Add " + id);
            commanderBlueprints.AddBlueprint(id);
        }
        logger.Log("start blueprints added to " + player.name);
        logger.Log("set names...");
        Commander commander = player.GetComponent<Commander>();
        commander.commanderName = player.name;
        logger.Log($"{commander.commanderName} set");
    }

    public void AssignStartPlanets()
    {
        List<GameObject> freePlanets = new List<GameObject>(planets);
        foreach (Commander commander in commanders)
        {
            GameObject choosenPlanet = null;
            //choose random planet
            foreach (GameObject planet in freePlanets)
            {
                planet.GetComponent<Ownable>().SetOwner(commander);
                commander.ownedPlanets.Add(planet);
                ShowCommanderItsPlanet(planet);
                choosenPlanet = planet;
                break;
            }
            if (choosenPlanet != null)
            {
                freePlanets.Remove(choosenPlanet);
            }
        }
    }

    [ClientRpc]
    public void ShowCommanderItsPlanet(GameObject planet)
    {
        planet.GetComponent<ISelectable>().Selected = true;
    }

}
