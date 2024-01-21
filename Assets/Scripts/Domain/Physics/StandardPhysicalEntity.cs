using UnityEngine;

public class StandardPhysicalEntity : PhysicalEntity
{
    public override void EmitThermalRadiation()
    {
        float emitPower = Constants.STEFAN_BOLTZMANN_CONSTANT * this.Emissivity * this.Radius * 2 * Mathf.PI * Mathf.Pow(this.Temperature, 4);
        this.Heat(-emitPower);
    }

    public override void OnCollide(CollisionEvent collisionEvent)
    {
        throw new System.NotImplementedException();
    }

    public override void Heat(float power)
    {
        this.Temperature += power / (this.SpecificHeatCapacity * this.Mass);
    }

    public override void OnRecieveSolarRadiation(SolarRadiationEvent solarRadiationEvent)
    {
        this.Heat(solarRadiationEvent.Power);
    }

    public override void OnUpdatePosition(Vector3 position)
    {
        this.Position = position;
    }

    public override void Update(float deltaTimeMillis)
    {
        this.EmitThermalRadiation();
    }
}