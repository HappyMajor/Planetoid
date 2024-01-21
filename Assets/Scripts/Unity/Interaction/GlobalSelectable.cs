using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalSelectable : MonoBehaviour, ISelectable, IPointerClickHandler
{
    public static GlobalSelectable currentSelectedInstance = null;
    public bool Selected { get => GetSelected(); set => SetSelect(value); }
    public MonoBehaviour MonoBehaviour { get => this; }

    public event ISelectable.OnSelect onSelect;
    public event ISelectable.OnDeSelect onDeSelect;

    private void SetSelect(bool selected)
    {
        if(selected)
        {
            this.UnselectCurrent();
            currentSelectedInstance = this;
            currentSelectedInstance.onSelect?.Invoke();
        } else
        {
            if(currentSelectedInstance == this)
            {
                UnselectCurrent();
            }
        }
    }

    private void UnselectCurrent()
    {
        if(currentSelectedInstance != null )
        {
            currentSelectedInstance.onDeSelect?.Invoke();
            currentSelectedInstance=null;
        }
    }

    private bool GetSelected()
    {
        if (currentSelectedInstance == null) return false;

        if(currentSelectedInstance == this) return true;

        return false;
    }

    public void OnDestroy()
    {
        if(currentSelectedInstance == this)
        {
            this.UnselectCurrent();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.Selected = !this.Selected;
    }
}