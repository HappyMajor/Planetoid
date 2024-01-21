using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class RigidBodyBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D collider;

    public Vector2 Center { get => collider.bounds.center; }
    public float Radius { get => collider.radius; }

    public virtual void Start()
    {
        this.collider = GetComponent<CircleCollider2D>();
        this.rb = GetComponent<Rigidbody2D>();
    }
    public Vector2 GetCenterOfMass()
    {
        return GetComponent<CircleCollider2D>().bounds.center; 
    }

    public Vector2 GetClosestSurfacePosition(Vector2 pos)
    {
        Vector2 dir = (pos - Center).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x);
        float x = Mathf.Cos(angle) * Radius;
        float y = Mathf.Sin(angle) * Radius;
        Vector2 relative = new Vector2(x, y);
        return relative + Center;
    }

}