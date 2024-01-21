using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public List<Rigidbody2D> rigidBodiesToAttract = new List<Rigidbody2D>();
    public Transform sourcePoint;
    public float gravity = 9.81f;
    public float range = 100f;

    public void Update()
    {
        foreach(Rigidbody2D rb in rigidBodiesToAttract)
        {
            if(Vector2.Distance(rb.position, sourcePoint.position) < range)
            {
                Vector2 directionToSourcePoint = (sourcePoint.position - rb.transform.position).normalized;
                rb.AddForce(directionToSourcePoint * gravity * rb.mass * Time.deltaTime);
            }
        }
    }
}