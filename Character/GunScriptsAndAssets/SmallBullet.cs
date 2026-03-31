using UnityEngine;

public class SmallBullet : BaseBullet
{
    [SerializeField] private int damage = 10;

    protected override void OnCollisionEnter(Collision collision)
    {
        HandleDamage(collision);
        HandleInteractions(collision);
        base.OnCollisionEnter(collision);
        Destroy(gameObject);
    }

    private void HandleDamage(Collision collision)
    {
        Health target = collision.gameObject.GetComponent<Health>();
        if (target != null)
        {
            target.TakeDamage(damage, transform.position);
        }
    }

    private void HandleInteractions(Collision collision)
    {
        BlastItem blastItem = collision.transform.GetComponent<BlastItem>();
        if (blastItem)
        {
            Debug.Log("Blasting");
            blastItem.Blast();
        }
    }
}