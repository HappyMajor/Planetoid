using UnityEngine;

public abstract class MenuItem : MonoBehaviour
{
    private IMenuItemData menuItemData;
    private TreeMenu menu;

    public void SetMenuItemData(IMenuItemData menuItemData)
    {
        this.menuItemData = menuItemData;
    }

    public void SetTreeMenu(TreeMenu menu)
    {
        this.menu = menu;
    }

    public IMenuItemData GetMenuItemData()
    {
        return this.menuItemData;
    }

    public void OnClick()
    {
        if (this.menu == null) throw new System.Exception("Menu is null");

        this.menu.OnClickMenuItemEvent?.Invoke(this.menuItemData);
    }

    public abstract void Render();
}