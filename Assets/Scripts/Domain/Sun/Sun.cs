using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sun : PhysicalEntity
{
    private List<Planet> orbitingPlanets = new List<Planet>();
    public List<Planet> OrbitingPlanets { get => orbitingPlanets; set => orbitingPlanets = value; }

    public override void OnCollide(CollisionEvent collisionEvent)
    {
        throw new NotImplementedException();
    }

    public override void OnUpdatePosition(Vector3 position)
    {
        this.Position = position;
    }

    public float CalculateSolarConstantForDistance(float distance)
    {
        //1361 W/m2 is the solar constant for earth (1AU distance) * 1 AU in mKm
        return 1361 / Mathf.Pow((distance/150),2);
    }

    public override void Update(float deltaTimeMillis)
    {
        foreach(Planet planet in  orbitingPlanets)
        {
            float distance = Vector2.Distance(planet.Position, this.Position);
            SolarRadiationEvent solarRadiationEvent = new SolarRadiationEvent();
            solarRadiationEvent.Power = this.CalculateSolarConstantForDistance(distance);
            planet.OnRecieveSolarRadiation(solarRadiationEvent);
        }
    }

    public override void OnRecieveSolarRadiation(SolarRadiationEvent s)
    {
    }

    public override void EmitThermalRadiation()
    {
        //
    }

    public override void Heat(float power)
    {
        throw new NotImplementedException();
    }
}
