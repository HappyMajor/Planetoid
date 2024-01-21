using Frictionless;
using Mirror;
using PlanetoidMP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingContext))]
[RequireComponent(typeof(Ownable))]
[RequireComponent(typeof(ISelectable))]
[RequireComponent (typeof(CircleCollider2D))]
public class BuildController : NetworkBehaviour
{
    public TreeMenu builderMenu;
    public GameObject buildPlaceholder;
    private BuildPlanningMode buildPlanningMode;
    private CircleCollider2D circleCollider2D;
    private ISelectable selectable;
    public Ownable ownable;
    public PlanetoidLogger logger = new PlanetoidLogger(typeof(BuildController),LogLevel.DEBUG);

    [SerializeField]
    private BuildingContext buildingContext;

    [SerializeField]
    private CommanderBlueprints commanderBlueprints;

    public void Start()
    {
        this.builderMenu = GameObject.Find("BuilderMenu").GetComponent<TreeMenu>();
        if (this.builderMenu == null) throw new System.Exception("No Builder Menu Found");

        this.builderMenu.OnClickMenuItemEvent += this.OnClickBuildMenuItem;

        this.buildingContext = GetComponent<BuildingContext>();
        this.selectable = GetComponent<ISelectable>();
        this.ownable = GetComponent<Ownable>();
        this.circleCollider2D = GetComponent<CircleCollider2D>();   

        this.selectable.onSelect += OnSelect;
    }

    public void OnSelect()
    {
        logger.Log("On Select");
        if(this.ownable.IsOwnedByMe())
        {
            this.ShowMenu();
        }
    }

    public void OnClickBuildMenuItem(IMenuItemData item)
    {
        if (!this.ownable.IsOwnedByMe()) return;

        if(item is BuildingMenuItemData)
        {
            BuildingMenuItemData buildingMenuItem = (BuildingMenuItemData) item;
            Blueprint blueprint = Blueprints.GetInstance().GetBlueprintById(buildingMenuItem.BlueprintId);
            Debug.Log("BLUEPRINT_ID " + buildingMenuItem.BlueprintId);
            if(blueprint == null)
            {
                throw new System.Exception("Blueprint was not found");
            } else
            {
                this.StartBuildPlanningMode(blueprint);
            }
        }
    }

    public void OnDestroy()
    {
        this.builderMenu.OnClickMenuItemEvent -= this.OnClickBuildMenuItem;
    }

    public void ShowMenu()
    {
        this.commanderBlueprints = Commander.GetLocalCommander().GetComponent<CommanderBlueprints>();
        logger.Log("Show Menu");
        if (builderMenu == null) throw new System.Exception("BuilderMenu is null");

        builderMenu.RootMenuItems.Clear();
        builderMenu.SetRootNode(null);
        foreach (string bpId in commanderBlueprints.availableBlueprints)
        {
            logger.Log($"bpID {bpId}");
            Blueprint bp = Blueprints.GetInstance().GetBlueprintById(bpId);
            BuildingMenuItemData buildingMenuItemData = new BuildingMenuItemData();
            buildingMenuItemData.Description = bp.Description;
            buildingMenuItemData.Name = bp.Name;
            buildingMenuItemData.BlueprintId = bp.BlueprintId;
            builderMenu.RootMenuItems.Add(buildingMenuItemData);
        }
        builderMenu.Show();
    }

    [Command]
    public void Build(string blueprintid, float deg, float radius)
    {
        Blueprint bp = Blueprints.GetInstance().GetBlueprintById(blueprintid);
        this.buildingContext.SpawnBuilding(bp.Construction, deg, radius);
    }

    public void StartBuildPlanningMode(Blueprint blueprint)
    {
        BuildingPrefabResolver buildingPrefabResolver = Component.FindAnyObjectByType<BuildingPrefabResolver>();
        GameObject currentPlaceholder = GameObject.Instantiate(buildingPrefabResolver.Resolve(Buildings.GetBuilding(blueprint.BuildsTo)));
        currentPlaceholder.transform.localScale = transform.localScale;
        this.buildPlanningMode = new BuildPlanningMode(blueprint.Construction, 1, currentPlaceholder, blueprint.BlueprintId); 

        this.buildPlanningMode.placeholder = currentPlaceholder;

        MessageRouter.RaiseMessage(new BuildModeStartEvent());
    }

    public void UpdateBuildPlanningMode()
    {
        //get surface in the direction of arrowCursor
        Vector3 dir = MouseCursor.GetInstance().transform.position - this.circleCollider2D.bounds.center;
        dir.z = 0;
        Vector3 surfacePos = this.circleCollider2D.bounds.center + dir.normalized * this.circleCollider2D.radius;
        GameObject placeHolder = this.buildPlanningMode.placeholder.gameObject;
        placeHolder.transform.position = surfacePos;

        float deg = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        placeHolder.transform.eulerAngles = new Vector3(0, 0, deg);

        PolarPosition polarPosition = new PolarPosition(1, deg);
        //if (this.buildPlanningMode.planetController.ModelComponent.DomainModel.IsFreeRect(polarPosition, 1, 1))
        //{
        //    placeHolder.GetComponentInChildren<SpriteRenderer>().color = new Color(Color.green.r, Color.green.g, Color.green.b, 10);
        //}
        //else
        //{
        //    placeHolder.GetComponentInChildren<SpriteRenderer>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 10);
        //} 
    }


    public void Update()
    {
        bool primaryBtn = Input.GetMouseButtonDown(0);
        if(this.buildPlanningMode != null)
        {
            this.UpdateBuildPlanningMode();
        }

        if(primaryBtn)
        {
            if(this.buildPlanningMode != null)
            {
                Vector3 dir = MouseCursor.GetInstance().transform.position - this.circleCollider2D.bounds.center;
                dir.z = 0;
                Vector3 surfacePos = dir.normalized * this.circleCollider2D.radius;
                GameObject placeHolder = this.buildPlanningMode.placeholder.gameObject;
                placeHolder.transform.position = surfacePos;

                float deg = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
                placeHolder.transform.eulerAngles = new Vector3(0, 0, deg);
                this.Build(this.buildPlanningMode.blueprintId, deg, this.circleCollider2D.radius);
                this.buildPlanningMode = null;
                GameObject.Destroy(placeHolder);
                MessageRouter.RaiseMessage(new BuildModeEndEvent());
            }
        }
    }

}

public class BuildPlanningMode
{
    public string blueprintId;
    public Construction construction;
    public int heightLevel;
    public GameObject placeholder;

    public BuildPlanningMode(Construction construction, int heightLevel, GameObject placeholder, string blueprintId)
    {
        this.construction = construction;
        this.heightLevel = heightLevel;
        this.placeholder = placeholder;
        this.blueprintId = blueprintId;
    }
}
