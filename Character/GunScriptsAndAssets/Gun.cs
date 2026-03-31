using UnityEngine;

public abstract class Gun : Weapon
{
    [SerializeField] protected float fireRate = 0.3f;

    public abstract void PullTrigger(Vector3 spawn, Vector3 direction);
    //public abstract void SetSpawn(Vector3 spawnPosition, Vector3 aimDir);
    public abstract void EmitBulletEffect();

    public virtual void Reload()
    {
        Debug.Log("Default reload called.");
    }
}
