using UnityEngine;

public class PlayerDetector : MonoBehaviour, IDetectable
{
    [Header("Sight")]
    public float viewRadius = 15f;
    public float viewAngle = 90f;

    [Header("Hearing")]
    public float hearingRadius = 10f;  // Radius to detect player noise

    [Header("Layers")]
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    public bool DetectPlayer(out Transform player)
    {

        Collider[] playersInSight = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        foreach (var col in playersInSight)
        {
            Vector3 dir = (col.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dir) < viewAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (!Physics.Raycast(transform.position, dir, distance, obstacleMask))
                {
                    player = col.transform;
                    return true; 
                }
            }
        }

        Collider[] playersInHearing = Physics.OverlapSphere(transform.position, hearingRadius, playerMask);
        foreach (var col in playersInHearing)
        {
            PlayerNoise noise = col.GetComponent<PlayerNoise>();
            if (noise != null && noise.IsMakingNoise)
            {
                player = col.transform;
                return true; 
            }
        }

        player = null;
        return false; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingRadius);
    }
}

