using UnityEngine;

public class BaseWarhead : MonoBehaviour, IWarhead
{
    public GameObject explosionPrefab;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Explosion explosion = GameObject.Instantiate(explosionPrefab).GetComponent<Explosion>();
        explosion.transform.position = transform.position;
        Destroy(transform.parent.gameObject);
    }
}