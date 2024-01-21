using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : NetworkBehaviour
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
