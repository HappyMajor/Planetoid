using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private Building building;
    public void SetBuilding(Building building)
    {
        this.building = building;
    }

    public void Start()
    {

    }
}
