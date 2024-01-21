using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float force = 10f;
    public void Launch(Rigidbody2D rigidbody, Vector2 direction, float force)
    {

    }

    public void Update()
    {
        Transform cursor = MouseCursor.GetInstance().transform;
        Vector2 directionLaunchpadToCursor = (cursor.position - transform.position).normalized;
        Vector2 currentPos = transform.position;
        Debug.DrawLine(currentPos, currentPos + directionLaunchpadToCursor * 10, Color.green);

        bool mouseClick = Input.GetMouseButtonDown(0);

        if (mouseClick)
        {
            //this.Launch(0.01f, directionLaunchpadToCursor);
        }
    }

    public void OnDrawGizmos()
    {

    }
}
