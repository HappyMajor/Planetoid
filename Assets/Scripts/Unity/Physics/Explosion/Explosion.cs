using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 1f;
    public float force = 1f;
    public Animator animator;
    public float destroyAfterSeconds = 1.2f;
    public LayerMask layerMask;

    public void Start()
    {
        this.Explode();
        StartCoroutine(SelfDestruction());
    }

    public IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        Destroy(gameObject);
    }

    public void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        foreach(Collider2D collider in colliders)
        {
            //apply force based on distance
            Vector2 relative = (collider.bounds.center - transform.position);
            float distance = relative.magnitude;

            collider.attachedRigidbody.AddForce((relative.normalized * force)/(distance), ForceMode2D.Impulse);
        }
    }

}