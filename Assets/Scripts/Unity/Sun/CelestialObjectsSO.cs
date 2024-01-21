using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "data/celestialObjects")]
public class CelestialObjectsSO : ScriptableObject
{
    public List<CelestialPhysicsProperties> planets;
    public List<CelestialPhysicsProperties> suns;

    [Serializable]
    public struct CelestialPhysicsProperties
    {
        public string name;
        public float mass;
        public float radius;
    }
}
