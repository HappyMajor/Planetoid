using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : Controller<Player>
{
    public GameObject cursor;
    private List<PlanetController> ownedPlanets = new List<PlanetController>();
    private PlanetController currentPlanet;
    public List<PlanetController> OwnedPlanets { get => ownedPlanets; set => ownedPlanets = value; }

    public override void OnModelChange(Player model)
    {
        model.OnPlayerHQChanged += this.OnPlayerHQChanged;
    }

    public void Start()
    {
        this.FindCursor();
    }

    private void FindCursor()
    {
        if (this.cursor == null) this.cursor = Component.FindObjectOfType<ArrowKeyCursor>().gameObject;
    }

    public void FocusPlanet(PlanetController planetController)
    {
        this.FindCursor();
        this.currentPlanet = planetController;
        CameraController.GetInstance().Follow(this.currentPlanet.transform);
    }

    private void Update()
    {
        bool spacePressed = Input.GetKeyDown(KeyCode.Space);
        bool buyPressed = Input.GetKeyDown(KeyCode.B);
        bool pressedBuild = Input.GetKeyDown(KeyCode.Q);

        if (spacePressed)
        {
            if(currentPlanet != null)
            {
                cursor.transform.position = this.currentPlanet.gameObject.transform.position; 
            } else
            {
                this.currentPlanet = null;
                this.cursor.GetComponent<ArrowKeyCursor>().ClearFollow();
            }
        }

        if(buyPressed)
        {
            if(this.currentPlanet.GetComponent<BuildController>())
            {
                BuildController buildController = this.currentPlanet.GetComponent<BuildController>();
                buildController.ShowMenu();
            } else
            {
                throw new System.Exception("No Build Controller Found For Currently Selected Planet");
            }
        }


        if (pressedBuild)
        {
            if (this.ModelComponent != null)
            {
                if(this.currentPlanet != null)
                {
                    // /this.ModelComponent.DomainModel.TryBuildOnPlanet(BlueprintFactory.GetBlueprint(BlueprintFactory.ID_HQ), this.currentPlanet.ModelComponent.DomainModel);
                }
            }
        }
    }

    public void OnPlayerHQChanged(PlayerHQChangedEvent ev)
    { 

    }
}
