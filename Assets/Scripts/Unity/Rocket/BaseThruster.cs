using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseThruster : MonoBehaviour, IThruster
{
    private bool acceptsLowerFuelAmountThanRequested = true;
    private bool isThrusterOn = false;
    private Fuel fuel = new Fuel(FuelType.STANDARD, 0);
    private float intensity = 100f;
    private float maxIntensity = 100f;
    private float force =  0.01f;
    public BaseMissileCU baseMissileCU;
    public void StartThruster()
    {
        this.isThrusterOn = true;
        StartCoroutine(Thrust());
    }

    public void StopThruster()
    {
        this.isThrusterOn = false;
        StopAllCoroutines();
    }

    public Fuel DrainFuel(Fuel fuel)
    {
        Fuel drainedFuel = this.baseMissileCU.DrainFuel(fuel);
        this.fuel.amount += drainedFuel.amount;
        this.fuel.type = FuelType.STANDARD;
        return fuel;
    }

    public IEnumerator Thrust()
    {
        const float TIME_INTERVAL = 0.1f;
        while (this.isThrusterOn)
        {
            float requiredFuelAmount = 1f * TIME_INTERVAL;
            if (this.fuel.amount <= 0f)
            {
               if(DrainFuel(new Fuel(FuelType.STANDARD, requiredFuelAmount)).amount <= 0)
               {
                    break;
               }
            }

            Rigidbody2D rb = baseMissileCU.GetRigidBody();
            Vector2 dir = baseMissileCU.Dir;

            rb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + -90);
            rb.AddForce(force * dir.normalized * TIME_INTERVAL);
            fuel.amount -= requiredFuelAmount;
            yield return new WaitForSeconds(TIME_INTERVAL);
        }
        this.isThrusterOn = false;
    }

    public void IncreaseThrusterIntensity(float amount)
    {
        this.intensity += amount;
    }

    public float GetMaxIntensity()
    {
        return this.maxIntensity;
    }

    public List<FuelType> GetAllowedFuelTypes()
    {
        return new List<FuelType> { FuelType.STANDARD };
    }
}