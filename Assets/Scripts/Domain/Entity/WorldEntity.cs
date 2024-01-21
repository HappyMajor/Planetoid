using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public abstract class WorldEntity : Entity
{
    [SerializeField]
    private Vector2 position;
    public Vector2 Position { get => position; set => position = value; }
    public abstract void Update(float deltaTimeMillis);

    public WorldEntity() {
        Debug.Log("WORLD ENTITY ADDED: " + this);
        Universe.WorldEntities.Add(this);
    }
}