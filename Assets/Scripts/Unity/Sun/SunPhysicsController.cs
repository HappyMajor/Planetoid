using UnityEngine;
using System.Collections.Generic;

public class SunPhysicsController : Controller<Sun>
{
    private Rigidbody2D rg;

    public override void OnModelChange(Sun model)
    {
        this.rg = GetComponent<Rigidbody2D>();
        this.rg.gravityScale = 0;
        this.rg.mass = model.Mass;
    }

    public void Update()
    {
        this.ModelComponent.DomainModel.OnUpdatePosition(transform.position);
    }
}