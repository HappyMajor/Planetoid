using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "data/builder/blueprint")]
public class SOBlueprint : ScriptableObject
{
    public string blueprintName;
    public string description;
    public IMaterialStack[] materialsRequired;
    public float costToBuild;
}
