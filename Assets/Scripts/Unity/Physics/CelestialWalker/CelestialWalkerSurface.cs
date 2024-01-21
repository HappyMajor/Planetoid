using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CelestialWalkSurface : RigidBodyBehaviour
{
    public float surfacePositionAngleSteps = 2f;
    public CircleCollider2D circleCollider;
    public delegate void WalkFunc(float x, float y);
    //public List<Vector2> surfacePositions = new List<Vector2>();
    public List<CelestialWalker> celestialWalkers = new List<CelestialWalker>();
    public Dictionary<CelestialWalker, WalkFunc> walkFunctionDic = new Dictionary<CelestialWalker, WalkFunc>();
    public List<Vector2> highlight = new List<Vector2>();
    public override void Start() 
    {
        base.Start();
        this.circleCollider = GetComponent<CircleCollider2D>();
    }



    public void Update()
    {
    }

    public void FixedUpdate()
    {

    }

    public void Land(CelestialWalker celestialWalker, Vector2 landPosition)
    {
        Vector2 closestSurfacePosition = this.GetClosestSurfacePosition(landPosition);
        celestialWalker.transform.SetParent(transform);
        this.highlight.Add(closestSurfacePosition);
        this.celestialWalkers.Add(celestialWalker);
        celestialWalker.transform.position = closestSurfacePosition;
        celestialWalker.Surface = this;
    }

    public void WalkSurface(float len, float speed, WalkFunc walk, Vector2 startPosition, Action onEnd)
    {
        Vector2 surfacePos = this.GetClosestSurfacePosition(startPosition);
        StartCoroutine(UpdateWalk(len, speed, walk, surfacePos, onEnd));
    }
    public IEnumerator UpdateWalk(float len, float speed, WalkFunc walk, Vector2 startPosition, Action onEnd)
    {
        float walkedLen = 0f;
        while(walkedLen < len)
        {
            Vector2 centerToStartPosDir = (startPosition - Center).normalized;
            float currentAngle = Mathf.Atan2(centerToStartPosDir.y, centerToStartPosDir.x);
            float x = Mathf.Cos(currentAngle + ((speed * Time.deltaTime) / Radius)) * Radius;
            float y = Mathf.Sin(currentAngle + ((speed * Time.deltaTime) / Radius)) * Radius;
            walkedLen += Mathf.Abs(speed * Time.deltaTime);
            startPosition = Center + new Vector2(x, y);
            walk?.Invoke(Center.x + x, Center.y + y);
            yield return new WaitForEndOfFrame();
        }
        onEnd();
    }

    private void OnDisable()
    {
       highlight.Clear();
    }
}
