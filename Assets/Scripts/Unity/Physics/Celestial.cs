using JetBrains.Annotations;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Celestial : RigidBodyBehaviour
{
    public float smallLayerMass = 1f;
    public float spin = 1f;
    public override void Start()
    {
        base.Start();
        this.rb.AddTorque(spin);
    }
    public void FixedUpdate()
    {
        this.Gravity();
    }
    public void Gravity()
    {
        Celestial[] celestials = Component.FindObjectsByType<Celestial>(FindObjectsSortMode.InstanceID);
        foreach(Celestial celestial in celestials)
        {
            if(celestial.gameObject != gameObject)
            {
                float m1 = rb.mass;
                float m2 = celestial.rb.mass;
                float r = Vector2.Distance(this.GetCenterOfMass(), celestial.GetCenterOfMass());
                Vector2 force = (this.GetCenterOfMass() - celestial.GetCenterOfMass()).normalized * (UniverseController.G() * ((m1 * m2) / (r * r)));
                celestial.rb.AddForce(force * Time.deltaTime);
            }
        }

        Attracted[] attracteds = Component.FindObjectsByType<Attracted>(FindObjectsSortMode.InstanceID);
        foreach(Attracted attracted in attracteds)
        {
            float m1 = smallLayerMass;
            float m2 = attracted.rigidbody2D.mass;
            float r = Vector2.Distance(this.GetCenterOfMass(), attracted.transform.position);
            
            Vector2 force = (this.GetCenterOfMass() - new Vector2(attracted.transform.position.x,attracted.transform.position.y)).normalized * ((UniverseController.G() * 1000) * ((m1 * m2) / (r * r)));
            attracted.rigidbody2D.AddForce(force);
        }
    }
}