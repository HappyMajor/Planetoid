using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 
using UnityEngine.EventSystems;
public class PlanetController : Controller<Planet>, IPointerClickHandler
{
    public GameObject buildingPrefab;
    public GameObject constructionPrefab;
    public GameObject shelterPrefab;
    public GameObject hqPrefab;
    public override void OnModelChange(Planet model)
    {
        model.OnBuildBuilding += OnBuildBuilding;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICKED ON PLANET!");
        PlayerController playerController = Component.FindAnyObjectByType<PlayerController>();
        if (playerController != null)
        {
            playerController.FocusPlanet(this);
        }
        else
        {
            Debug.Log("No player found!");
        }
    }

    public float Radius()
    {
        return 3f;
    }

    public void OnBuildBuilding(OnBuildBuildingEvent buildingEvent)
    {
        BuildingPrefabResolver buildingPrefabResolver = Component.FindObjectOfType<BuildingPrefabResolver>();
        GameObject building = GameObject.Instantiate(buildingPrefabResolver.Resolve(buildingEvent.building), gameObject.transform);
        PolarPosition polarPosition = this.ModelComponent.DomainModel.GetPositionOfBuilding(buildingEvent.building);
        Vector3 position = PolarPositionToCartesian(this, polarPosition);
        Debug.Log("transform.position: " + transform.position.ToString());
        Debug.Log("center: " + Center(this).ToString() + " +  cartesian position: " +  position.ToString());
        building.transform.localPosition = transform.InverseTransformPoint(Center(this) + position);
        building.transform.eulerAngles = new Vector3(0, 0, polarPosition.Phi);
        Debug.Log("localPosition: " + building.transform.localPosition);
        Debug.Log("cartesian: " + position.x + ", " + position.y + ", " + position.z);
        Debug.Log("ON BUILD BUILDING POLAR POSITION: phi " + polarPosition.Phi + " radius: " + polarPosition.R);
    }

    public static Vector3 PolarPositionToCartesian(PlanetController planetController, PolarPosition polarPosition)
    {
        float x = planetController.Radius() * Mathf.Cos(polarPosition.Phi * Mathf.Deg2Rad);
        float y = planetController.Radius() * Mathf.Sin(polarPosition.Phi * Mathf.Deg2Rad);
        return new Vector3(x, y, 0);
    }

    public static Vector3 SurfacePosition(PlanetController planetController, Vector3 direction)
    {
        CircleCollider2D collider = planetController.GetComponent<CircleCollider2D>();
        if (collider == null) throw new System.Exception("No Collider Set For Planet");
        direction.z = 0;
        Vector3 surfacePos = Center(planetController) + direction.normalized * planetController.GetComponent<CircleCollider2D>().bounds.extents.magnitude;
        return surfacePos;
    }

    public static Vector3 Center(PlanetController planetController)
    {
        CircleCollider2D collider = planetController.GetComponent<CircleCollider2D>();
        if (collider == null) throw new System.Exception("No Collider Set For Planet");

        return collider.bounds.center;
    }
}
