using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteMask))]
public class SpriteMaskController : MonoBehaviour
{
    public SpriteRenderer target;
    private SpriteMask mask;
    public float slide = 0;
    Vector3 startPos = Vector2.zero;
    Vector3 endPos = Vector2.zero;
    float spriteHeight;
    public void Start()
    {
        this.Init();
    }

    public void Init()
    {
        this.mask = GetComponent<SpriteMask>();
        endPos = target.transform.localPosition;
        spriteHeight = target.sprite.bounds.size.y;
        startPos = target.transform.localPosition - new Vector3(spriteHeight, 0, 0);
        transform.localPosition = startPos;
        Debug.Log("rect.width: " + spriteHeight);
    }

    public void Update()
    {
        float clamped = Mathf.Clamp(slide, 0, 1);
        Vector2 delta = endPos - startPos;
        transform.localPosition = startPos + new Vector3(delta.x * clamped, 0,0);
    }
}
