using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandle2D : MonoBehaviour
{
    public Vector2 targetPosition { get { return m_TargetPosition; } set { m_TargetPosition = value; } }
    [SerializeField]
    private Vector2 m_TargetPosition = new Vector2(1f, 0f);

    public virtual void Update()
    {
    }
}
