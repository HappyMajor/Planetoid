using System.Collections.Generic;
using UnityEngine;

public class BuildingMenuItemData : IMenuItemData
{
    public List<IMenuItemData> Children {  get; set; } = new List<IMenuItemData>();
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public IMenuItemData Parent { get; set; }
    public string BlueprintId { get => blueprintId; set => blueprintId = value; }

    private string name;
    private string description;
    private Sprite icon;
    private string blueprintId;
}