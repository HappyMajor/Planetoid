using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "data/building/material")]
public class SOMaterial : ScriptableObject
{
    public string materialName;
    public string description;
    public string materialType;
    public float mass;
}
