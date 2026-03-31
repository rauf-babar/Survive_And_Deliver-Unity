using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The part that rotates (The shield/gun).")]
    [SerializeField] private Transform turretHead;

    [Tooltip("Where bullets come out. Add Left and Right points here.")]
    [SerializeField] private Transform[] firePoints;

    [Tooltip("The Muzzle Flash particle system (Optional).")]
    [SerializeField] private ParticleSystem muzzleFlashVFX;

    [Header("Weapon Config")]
    [Tooltip("Drag your SmallBullet Prefab here.")]
    [SerializeField] private BaseBullet bulletPrefab;

    [SerializeField] private int bulletPower = 50;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float range = 20f;
    [SerializeField] private float turnSpeed = 10f;

    [Header("Targeting")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private string enemyTag = "Zombie";

    // Internal State
    private Transform target;
    private float nextFireTime = 0f;
    private int currentBarrelIndex = 0;

    void Start()
    {
        // Optimization: Scan for targets 4 times a second, not every frame.
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.25f);
    }

    void Update()
    {
        if (target == null) return;

        // 1. Aim at the target
        AimAtTarget();

        // 2. Fire if cooldown is ready
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void UpdateTarget()
    {
        // Find all colliders in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, enemyLayer);

        float shortestDist = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (Collider col in colliders)
        {
            // Tag check ensures we don't target random debris on the Enemy layer
            if (col.CompareTag(enemyTag))
            {
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    nearestEnemy = col.gameObject;
                }
            }
        }

        if (nearestEnemy != null && shortestDist <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void AimAtTarget()
    {
        // Get direction to target
        Vector3 dir = target.position - turretHead.position;

        // Create rotation looking at target
        Quaternion lookRot = Quaternion.LookRotation(dir);

        // Smoothly rotate the head
        // Note: We use the head's position but rotate towards the target
        Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRot, Time.deltaTime * turnSpeed).eulerAngles;

        // Apply rotation (Lock Z to 0 so it doesn't tilt weirdly)
        turretHead.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    void Shoot()
    {
        // Cycle barrels (Left -> Right -> Left)
        Transform currentFirePoint = firePoints[currentBarrelIndex];
        currentBarrelIndex = (currentBarrelIndex + 1) % firePoints.Length;

        // --- THE SDA PART: REUSING YOUR PROJECTILE MANAGER ---
        // Instead of Instantiating manually, we ask the Manager to do it.
        // This ensures physics, layers, and logic match your Player's gun.
        ProjectileManager.Instance.Shoot(
            bulletPrefab,
            bulletPower,
            currentFirePoint.position,
            currentFirePoint.forward // Shoot in the direction the barrel is pointing
        );

        // Visuals
        if (muzzleFlashVFX != null)
        {
            // Move VFX to active barrel and play
            muzzleFlashVFX.transform.position = currentFirePoint.position;
            muzzleFlashVFX.transform.rotation = currentFirePoint.rotation;
            muzzleFlashVFX.Stop();
            muzzleFlashVFX.Play();
        }
    }

    // Visualize Range in Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}