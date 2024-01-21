using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BuilderMenuItem : MenuItem
{
    public string description;
    public string title;
    public Sprite icon;

    public TMP_Text titleDisplay;
    public TMP_Text descriptionDisplay;
    public Button button;
    public delegate void OnClickDelegate(IMenuItemData menuItemData);
    
    public override void Render()
    {
        IMenuItemData menuItemData = this.GetMenuItemData();
        if (menuItemData == null) throw new System.Exception("MenuItemData is null");
        if (menuItemData is BuildingMenuItemData != true) throw new System.Exception("MenuItemData is not of type BuildingMenuItemData");

        BuildingMenuItemData buildingMenuItemData = menuItemData as BuildingMenuItemData;

        this.description = buildingMenuItemData.Description;
        this.title = buildingMenuItemData.Name;
        this.icon = buildingMenuItemData.Icon;

        this.titleDisplay.text = this.title;
        this.descriptionDisplay.text = this.description;
    }


}