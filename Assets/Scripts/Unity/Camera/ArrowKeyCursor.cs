using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyCursor : MonoBehaviour
{
    public float speed = 1f;
    public float smoothing = 1f;
    public Transform follow;

    public void Follow(Transform transform)
    {
        this.follow = transform;
    }

    public void ClearFollow()
    {
        this.follow = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(follow != null)
        {
            transform.position = follow.position;
        } else
        {
            this.Move();
        }
    }
    public void Move()
    {
        bool upPressed = Input.GetKey(KeyCode.UpArrow);
        bool downPressed = Input.GetKey(KeyCode.DownArrow);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.RightArrow);

        float yAxis = 0f;
        float xAxis = 0f;
        if (upPressed) yAxis += 1;
        if (downPressed) yAxis -= 1;

        if (leftPressed) xAxis -= 1;
        if (rightPressed) xAxis += 1;

        Vector2 moveDirection = new Vector2(xAxis, yAxis).normalized;

        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * speed * Time.deltaTime;
    }
}
