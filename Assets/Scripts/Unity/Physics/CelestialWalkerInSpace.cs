using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CelestialWalkerInSpace : RigidBodyBehaviour
{
    public GameObject celestialWalkerPrefab;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CelestialWalkSurface>() != null)
        {
            this.Land(collision.gameObject.GetComponent<CelestialWalkSurface>());
        }
    }

    public void Land(CelestialWalkSurface celestialWalkSurface)
    {
        CelestialWalker celestialWalker = GameObject.Instantiate(celestialWalkerPrefab).GetComponent<CelestialWalker>();    
        celestialWalkSurface.Land(celestialWalker, GetCenterOfMass());
        Destroy(this.gameObject);
    }
}
