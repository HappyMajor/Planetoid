using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreOfGravity : MonoBehaviour
{
    public Transform centerOfMass;
    public Rigidbody2D rigidbody2D;

    public void Start()
    {
        this.rigidbody2D.centerOfMass = centerOfMass.localPosition;
    }

    public void Update()
    {

    }
}
