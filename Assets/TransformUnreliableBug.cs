using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUnreliableBug : MonoBehaviour
{
    public float rotationAmount = 0.1f;

    public void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(0, 0.1f, 0);
        transform.Rotate(new Vector3(0, 0, rotationAmount));
    }
}
