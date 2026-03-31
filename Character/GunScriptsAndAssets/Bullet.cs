using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{

    [SerializeField] protected float LifeTime = 3f;
    [SerializeField] protected Transform vfxHit;

    protected virtual void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        SpawnVFX(collision);
    }

    protected void SpawnVFX(Collision collision)
    {
        if (vfxHit != null)
        {
            Instantiate(vfxHit, collision.contacts[0].point, Quaternion.identity);
        }
    }
}