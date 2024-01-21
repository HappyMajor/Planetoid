using PlanetoidMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Selectable : MonoBehaviour, ISelectable, IPointerClickHandler
{
    [SerializeField]
    private bool selected = false;
    public PlanetoidLogger logger = new PlanetoidLogger(typeof(ISelectable), LogLevel.DEBUG);
    public MonoBehaviour MonoBehaviour { get => this; }
    public delegate void OnSelect();
    public delegate void OnDeSelect();

    public event ISelectable.OnSelect onSelect;
    public event ISelectable.OnDeSelect onDeSelect;

    public bool Selected { 
        get { return selected; }
        set { 
            this.selected = value;
            if (!this.selected)
            {
                logger.Log("DeSelect");
                this.onDeSelect();
            }
            else
            {
                logger.Log("Select");
                this.onSelect();
            }
        }
    }


    public void Start()
    {
        this.onSelect += OnSelectE;
        this.onDeSelect += OnDeSelectE;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        logger.Log("Click Selectable " + gameObject.name);
        this.Selected = !this.Selected;
    }

    public void OnSelectE()
    {
        logger.Log("OnSelectEE");
    }

    public void OnDeSelectE()
    {
        logger.Log("OnDeSelectE");
    }
}
