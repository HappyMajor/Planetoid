using UnityEngine;

public class BaseFuelTank : MonoBehaviour, IFuelTank
{
    private float amount = 0f;
    private FuelType fuelType = FuelType.STANDARD;

    public void AddFuel(Fuel fuel)
    {
        this.amount = fuel.amount;
        this.fuelType = fuel.type;
    }

    public Fuel DrainFuel(Fuel fuel)
    {
        if(fuel.type != this.fuelType)
        {
            throw new FuelTypeUnavailableException();
        }
        if(amount == 0f)
        {
            throw new FuelEmptyException();
        }

        float drainedAmount = 0f;
        if(amount < fuel.amount)
        {
            drainedAmount = amount;
            amount = 0;
        } else
        {
            drainedAmount = fuel.amount;
            amount-=fuel.amount;
        }
        return new Fuel(fuelType, drainedAmount);
    }
}