using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PhysicalEntity : WorldEntity
{
    [SerializeField]
    private Vector2 rotation;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float mass = 0;
    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private float emissivity = 0;
    [SerializeField]
    private float temperature = 0;
    [SerializeField]
    private float radius = 1;
    [SerializeField]
    private float specificHeatCapacity = 0;
    public Vector2 Rotation { get => rotation; set => rotation = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public float Mass { get => mass; set => mass = value; }
    public Vector2 Velocity { get => velocity; set => velocity = value; }
    public float Emissivity { get => emissivity; set => emissivity = value; }
    public float Temperature { get => temperature; set => temperature = value; }
    public float Radius { get => radius; set => radius = value; }
    public float SpecificHeatCapacity { get => specificHeatCapacity; set => specificHeatCapacity = value; }
    public abstract void OnCollide(CollisionEvent collisionEvent);
    public abstract void OnUpdatePosition(Vector3 position);
    public abstract void EmitThermalRadiation();
    public abstract void Heat(float power);
    public abstract void OnRecieveSolarRadiation(SolarRadiationEvent solarRadiationEvent);
}
