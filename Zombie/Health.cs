using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Transform bloodEffectPrefab;
    private float currentHealth;

    private Animator animator;
    private ZombieMovement movement;
    private MonoBehaviour[] otherControllers; 

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        movement = GetComponent<ZombieMovement>();

        otherControllers = GetComponents<MonoBehaviour>();
    }

    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {currentHealth}");

        if (bloodEffectPrefab != null)
        {
            Transform blood = Instantiate(bloodEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(blood.gameObject, 0.3f);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (movement != null)
        {
            movement.Stop();
            var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null) agent.enabled = false;
        }

        foreach (var script in otherControllers)
        {
            if (script != this && script != animator)
                script.enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null)
            cc.enabled = false;

        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        Destroy(gameObject, 5f);
    }
}
