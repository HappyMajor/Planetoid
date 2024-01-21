using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Player : Entity
{
    private BuilderService builderService;
    private HQ hQ;
    [SerializeField]
    private string name;
    [SerializeField]
    private List<Planet> ownedPlanets = new List<Planet>();
    public List<Planet> OwnedPlanets { get => ownedPlanets; set => ownedPlanets = value; }
    public string Name { get => name; set => name = value; }
    public HQ HQ { get => hQ; set { 
            this.hQ = value;
        }
    }

    public delegate void PlayerHQChangedDelegate(PlayerHQChangedEvent ev);
    public PlayerHQChangedDelegate OnPlayerHQChanged { get; set; }

    public Player()
    {
    }

    public Player(BuilderService builderService)
    {
        this.builderService = builderService;
    }

    public void TryBuildOnPlanet(Blueprint blueprint, Planet planet)
    {
        this.builderService.TryBuildOnPlanet(this, planet, blueprint);
    }
}
