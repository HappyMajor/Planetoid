using Planetoid.Livestock;
using UnityEngine;

public class LivestockPhysicsController : Controller<Livestock>
{
    private Rigidbody2D rg;
    public override void OnModelChange(Livestock model)
    {
        this.rg = GetComponent<Rigidbody2D>();
        this.rg.gravityScale = 0;
        this.rg.mass = model.Mass;
    }
}