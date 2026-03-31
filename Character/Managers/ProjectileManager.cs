using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BulletInfo
{
    public GameObject prefab;
    public float weight = 1f;
    public float baseForce = 50f;
}

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    


    private void Awake()
    {
        Instance = this;
        Debug.Log("Woken up");
    }

    public void Shoot(BaseBullet bulletType, int power, Vector3 spawnPos, Vector3 direction)
    {
        Debug.Log("Inside projectile manager");
        //cameraHandler.ToggleGunRecoil(true);
        GameObject projectile = Instantiate(bulletType.gameObject, spawnPos, Quaternion.LookRotation(direction, Vector3.up));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * power;
        }
        //cameraHandler.ToggleGunRecoil(false);
    }

}
