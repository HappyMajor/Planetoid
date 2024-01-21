using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialWalker : MonoBehaviour
{
    private CelestialWalkSurface surface;
    public Transform feetPosition;
    public ICelestialWalkerState currentState;
    public CelestialWalkSurface Surface { get => surface; set => surface = value; }

    public void Start()
    {
        this.ChangeState(new CelestialWalkerIdle());
    }

    public void Update()
    {
        if(this.currentState != null)
        {
            this.currentState.OnUpdate(this);
        }
    }

    public void ChangeState(ICelestialWalkerState state)
    {
        if(currentState != null)
        {
            this.currentState.OnExit(this);
        }
        this.currentState = state;
        this.currentState.OnEnter(this);
    }

    public void Walk(float len, float speed, CelestialWalkSurface.WalkFunc walkFunc, Vector2 startPosition, Action onEnd)
    {
        surface.WalkSurface(len, speed, walkFunc, startPosition, onEnd);
    }

    public void StandUpright()
    {
        Vector2 outward = (new Vector2(transform.position.x, transform.position.y) - surface.Center).normalized;
        float angle = Mathf.Atan2(outward.y, outward.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle + -90);
    }

}
