using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchData
{
    public Vector2 Dir { get => dir; set => dir = value; }
    public float Force { get => force; set => force = value; }
    public Fuel Fuel { get => fuel; set => fuel = value; }

    private Vector2 dir;
    private float force;
    private Fuel fuel;
    public LaunchData(Vector2 dir, float force, Fuel fuel) { 
        this.dir = dir;
        this.force = force;
        this.fuel = fuel;
    }

    public override string ToString()
    {
        return "LaunchData: " + " FuelAmount: " + this.fuel.amount + " FuelType: " + this.fuel.type;
    }
}
