using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Blueprint : Entity
{
    [SerializeField]
    private string blueprintId;
    [SerializeField]
    private string name;
    [SerializeField]
    private string category;
    [SerializeField]
    private string description;
    [SerializeField]
    private string buildsTo;
    [SerializeField]
    private Construction construction;

    private List<IMaterialStack> requiredMaterials = new List<IMaterialStack>(); 

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }

    public List<IMaterialStack> RequiredMaterialsToBuild {  get => requiredMaterials; set => requiredMaterials = value; }
    public string Category { get => category; set => category = value; }
    public string BlueprintId { get => blueprintId; set => blueprintId = value; }
    public string BuildsTo { get => buildsTo; set => buildsTo = value; }
    public Construction Construction { get => construction; set => construction = value; }

    public Blueprint(string blueprintId, string name, string description, string buildsTo, Construction construction)
    {
        this.blueprintId = blueprintId;
        this.name = name;
        this.description = description;
        this.buildsTo = buildsTo;
        this.construction = construction;
    }

    public Blueprint() { }
}
