using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : RigidBodyBehaviour, Launchable
{
    public bool isThrusterOn = false;
    public Vector2 dir;
    public float force;
    public Fuel fuel;
    public bool isBeingControlled = false;
    public void Launch(LaunchData launchData)
    {
        this.dir = launchData.Dir;
        this.force = launchData.Force;
        this.fuel = launchData.Fuel;
        this.ActivateThrusters();
    }

    public void ActivateThrusters()
    {
        this.isThrusterOn = true;
        StartCoroutine(Thrust());
    }

    public IEnumerator Thrust()
    {
        const float TIME_INTERVAL = 0.1f;
        while(this.isThrusterOn)
        {
            if(this.fuel.amount <= 0f)
            {
                break;
            }

            transform.eulerAngles = new Vector3(0,0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + -90);
            rb.AddForce(force * dir.normalized * TIME_INTERVAL);
            fuel.amount -= force * force * TIME_INTERVAL;
            yield return new WaitForSeconds(TIME_INTERVAL);
        }
        this.isThrusterOn= false;
    }
}
