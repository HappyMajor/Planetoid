using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeMenu : MonoBehaviour
{
    public IMenuItemData currentRootNode;
    private List<IMenuItemData> rootMenuItems = new List<IMenuItemData>();
    public GameObject menuItemPrefab;

    public List<IMenuItemData> RootMenuItems { get => rootMenuItems; set => rootMenuItems = value; }
    public delegate void OnClickMenuItemDelegate(IMenuItemData menuItemData);
    public OnClickMenuItemDelegate OnClickMenuItemEvent;

    public void Start()
    {
        List<IMenuItemData> list = rootMenuItems = new List<IMenuItemData>();
        
        for(int i = 0; i < 8; i++)
        {
            BuildingMenuItemData buildingMenuItemData = new BuildingMenuItemData();
            buildingMenuItemData.Description = i.ToString() + " - some description";
            buildingMenuItemData.Name = i.ToString() + " - Name";
            rootMenuItems.Add(buildingMenuItemData);
            for(int j = 0; j < Random.Range(1,8); j++)
            {
                BuildingMenuItemData child = new BuildingMenuItemData();
                child.Description = j.ToString() + " - child some description";
                child.Name = i.ToString() + " - child Name";
                child.Parent = buildingMenuItemData;
                buildingMenuItemData.Children.Add(child);
            }
        }

        this.Show();
        this.OnClickMenuItemEvent += this.OnClickMenuItem;
    }

    public void MoveUp()
    {
        if (this.currentRootNode == null) throw new System.Exception("Current root node is null");

        this.SetRootNode(this.currentRootNode.Parent);
        this.Show();
    }

    public void Show()
    {
        this.Clear();
        if (this.currentRootNode == null)
        {
            this.ShowRoot();
        } else
        {
            this.ShowMenuItem(this.currentRootNode);
        }
    }

    public void OnClickMenuItem(IMenuItemData menuItemData)
    {
        if(menuItemData.Children.Count > 0)
        {
            this.SetRootNode(menuItemData);
            this.Show();
        }
    }

    public void SetRootNode(IMenuItemData menuItemData)
    {
        this.currentRootNode = menuItemData;
    }

    public void Clear()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    private void ShowMenuItem(IMenuItemData menuItemData)
    {
        this.InstantiateItems(menuItemData.Children);
    }
    private void ShowRoot()
    {
        this.InstantiateItems(this.rootMenuItems);
    }

    private void InstantiateItems(List<IMenuItemData> items)
    {
        foreach (IMenuItemData item in items)
        {
            GameObject newMenuItem = GameObject.Instantiate(menuItemPrefab, transform);
            newMenuItem.GetComponent<MenuItem>().SetMenuItemData(item);
            newMenuItem.GetComponent<MenuItem>().SetTreeMenu(this);
            newMenuItem.GetComponent<MenuItem>().Render();
        }
    }

}
