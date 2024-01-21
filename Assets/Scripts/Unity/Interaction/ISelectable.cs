using UnityEngine;

public interface ISelectable
{
    public bool Selected { get; set; }
    public MonoBehaviour MonoBehaviour { get; }
    public delegate void OnSelect();
    public delegate void OnDeSelect();

    public event OnSelect onSelect;
    public event OnDeSelect onDeSelect;
}