using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Building
{
    [SerializeField]
    private float cost = 0;
    [SerializeField]
    private float height = 0;
    [SerializeField]
    private float width = 0;
    [SerializeField]
    private float structureHitpoints = 0;
    [SerializeField]
    private float armorHitpoints = 0;
    [SerializeField]
    private float shieldHitpoints = 0;
    [SerializeField]
    private float maxTemperatureUntilBurn = 0;
    [SerializeField]
    private float mass = 0;
    [SerializeField]
    private string material = "concrete";
    [SerializeField]
    private float powerConsumption;
    public float Cost { get => cost; set => cost = value; }
    public float Height { get => height; set => height = value; }
    public float Width { get => width; set => width = value; }
    public float StructureHitpoints { get => structureHitpoints; set => structureHitpoints = value; }
    public float ArmorHitpoints { get => armorHitpoints; set => armorHitpoints = value; }
    public float ShieldHitpoints { get => shieldHitpoints; set => shieldHitpoints = value; }
    public float MaxTemperatureUntilBurn { get => maxTemperatureUntilBurn; set => maxTemperatureUntilBurn = value; }
    public float Mass { get => mass; set => mass = value; }
    public string Material { get => material; set => material = value; }
    public float PowerConsumption { get => powerConsumption; set => powerConsumption = value; }

    public virtual void Update(float deltaTimeMillis)
    {
        
    }
}
