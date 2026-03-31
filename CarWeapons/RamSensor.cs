using UnityEngine;
using UnityEngine.AI;

public class RamController : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float minRamSpeed = 10f;
    [SerializeField] private float speedForMaxDamage = 30f;
    [SerializeField] private float minDamage = 10f;
    [SerializeField] private float maxDamage = 100f;
    [SerializeField] private float impactForce = 50f;

    private Rigidbody truckRb;

    private void Awake()
    {
        // Get the Rigidbody from the Parent (The Truck)
        // because this Rammer likely doesn't have its own physics body
        truckRb = GetComponentInParent<Rigidbody>();
    }

    // We use OnTriggerEnter because "Is Trigger" is Checked
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            ProcessHit(other.gameObject);
        }
    }

    private void ProcessHit(GameObject zombie)
    {
        if (truckRb == null) return;

        float currentSpeed = truckRb.linearVelocity.magnitude;

        if (currentSpeed < minRamSpeed) return;
        Rigidbody zombieRb = zombie.GetComponent<Rigidbody>();
        if (zombieRb != null)
        {
            zombieRb.isKinematic = false;

            Health health = zombie.GetComponent<Health>();
            if (health != null)
            {
                float speedPercentage = Mathf.InverseLerp(minRamSpeed, speedForMaxDamage, currentSpeed);
                float damage = Mathf.Lerp(minDamage, maxDamage, speedPercentage);
                health.TakeDamage(damage, zombie.transform.position);
            }

        }
    }
}