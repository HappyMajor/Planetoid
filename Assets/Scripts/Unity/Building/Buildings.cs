using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public static readonly string SHELTER_ID = "id_building_shelter";
    public static readonly string HQ_ID = "id_building_hq";

    public static Building GetBuilding(string id)
    {
        if(id == SHELTER_ID)
        {
            return new Shelter();
        }
        if(id == HQ_ID)
        {
            return new HQ();
        }

        throw new System.Exception("No Building Registered For this ID: " + id);
    }
}
