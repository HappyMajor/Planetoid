using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Planetoid;
public class GameModeController : Controller<Planetoid.GameMode>
{
    List<PlayerController> playerControllers = new List<PlayerController>();
    public GameObject playerPrefab;
    public override void OnModelChange(Planetoid.GameMode gameMode)
    {

    }

    public void StartGame(UniverseController universe)
    {
        Planetoid.GameMode gameMode = this.ModelComponent.DomainModel;
        gameMode.AddPlayer("mc hammergeil");
        gameMode.StartGame(universe.Universe);
        foreach(Player player in gameMode.Players)
        {
            Debug.Log("player: " + player.Name);
            this.CreatePlayerObj(player);
        }
    }


    public void CreatePlayerObj(Player player)
    {
        GameObject playerObj = GameObject.Instantiate(playerPrefab);
        this.playerControllers.Add(playerObj.GetComponent<PlayerController>());
        playerObj.GetComponent<PlayerModel>().SetModel(player);
    }
}
