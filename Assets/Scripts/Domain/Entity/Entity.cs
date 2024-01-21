using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Entity
{
    [SerializeField]
    private string id = Guid.NewGuid().ToString();

    public string Id { get => id; private set => id = value; }
}
