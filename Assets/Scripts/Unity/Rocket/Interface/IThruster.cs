using System.Collections.Generic;

public interface IThruster
{
    public void StartThruster();
    public void StopThruster();

    public void IncreaseThrusterIntensity(float amount);

    public float GetMaxIntensity();

    public List<FuelType> GetAllowedFuelTypes();
}