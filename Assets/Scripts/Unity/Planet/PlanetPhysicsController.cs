using System;
using UnityEngine;

public class PlanetPhysicsController : Controller<Planet>
{
    private Rigidbody2D rigidbody2D;

    public override void OnModelChange(Planet model)
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.rigidbody2D.mass = model.Mass;
        this.rigidbody2D.gravityScale = 0;
        this.transform.position = model.Position;
    }
    public void InitialVelocity()
    {
        SunPhysicsController sun = Component.FindAnyObjectByType<SunPhysicsController>();
        float mass = sun.gameObject.GetComponent<Rigidbody2D>().mass;
        
        float r = Vector2.Distance(transform.position, sun.transform.position);
        Vector3 relative = transform.InverseTransformPoint(sun.transform.position);
        float angleToSun = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        Debug.Log("mass: " + mass + " r: " + r);
        transform.Rotate(0,0,-angleToSun);
        Vector3 originalVector = ((transform.position - sun.transform.position).normalized);
        Vector3 orthogonal = Vector3.Cross(originalVector, transform.forward).normalized;
        this.rigidbody2D.velocity = -orthogonal * Mathf.Sqrt((UniverseController.G() * mass) / r);
    }

    public void Update()
    {
        this.ModelComponent.DomainModel.OnUpdatePosition(transform.position);
    }

    public void Start()
    {
        this.InitialVelocity();
    }
}