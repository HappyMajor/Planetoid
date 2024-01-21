using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel
{
    public FuelType type;
    public float amount;

    public Fuel(FuelType type, float amount)
    {
        this.type = type;
        this.amount = amount;
    }
}
