using System;
using UnityEngine;
public class PolarPosition
{
    float r;
    float phi;

    public PolarPosition(float r, float phi)
    {
        this.r = r;
        this.phi = phi;
    }

    public Vector2 Cartesian()
    {
        float radians = phi/57.2957795f;
        float x = this.r * MathF.Cos(radians);
        float y = this.r * MathF.Sin(radians);
        return new Vector2(x, y);
    }

    public float R { get => r; set => r = value; }
    public float Phi { get => phi; set => phi = value; }
}