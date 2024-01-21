using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialVelocityOrbit : RigidBodyBehaviour
{
    [SerializeField]
    public Celestial orbit;

    [Header("Values above 1 like 1.1 makes it get thrown out of orbit after some time and below crash into the orbit")]
    public float destabilizer = 1f;
    public override void Start()
    {
        base.Start();

        //We need to wait for the application of the start velocity of the other objects we want to orbit so we 
        //can add their velocity to ours in order to accomplish an orbit
        StartCoroutine(InitialVelocity());
    }

    public IEnumerator InitialVelocity()
    {
        yield return new WaitForSeconds(0.1f);
        //other start scripts should be finished by now 
        float mass = orbit.gameObject.GetComponent<Celestial>().smallLayerMass;

        float r = Vector2.Distance(transform.position, orbit.Center);
        //Vector3 relative = transform.InverseTransformPoint(orbit.transform.position);
        //float angleToSun = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        //Debug.Log("mass: " + mass + " r: " + r);
        //transform.Rotate(0, 0, -angleToSun);
        Vector2 relative = (orbit.Center - (Vector2)transform.position);
        float angleToOrbit = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angleToOrbit);
        Vector3 orthogonal = Vector3.Cross(relative.normalized, transform.forward).normalized;

        Debug.Log("distance: " + relative.magnitude);

        this.rb.velocity = (Vector2) (orthogonal * Mathf.Sqrt((UniverseController.G() * 1000 * mass) / r) * destabilizer) + orbit.rb.velocity;
        Debug.Log("speed: " + rb.velocity.magnitude * destabilizer);
    }
}
