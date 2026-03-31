using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float attackingDistance = 1f;

    private IMovable _movement;
    private IAttackable _attacker;
    private IDetectable _detector;

    private int currentWaypointIndex = 0;
    private bool movingForward = true;
    private bool isPatrolling = true;

    private void Awake()
    {
        _movement = GetComponent<IMovable>();
        _attacker = GetComponent<IAttackable>();
        _detector = GetComponent<IDetectable>();
    }

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
    }

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            _movement.Move(0.5f);
            (_movement as ZombieMovement).SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    private void Update()
    {
        if (_detector.DetectPlayer(out Transform player))
        {
            isPatrolling = false;
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= attackingDistance)
            {
                _movement.Stop();
                _attacker.Attack(player);
            }
            else
            {
                _movement.Move(2f);
                (_movement as ZombieMovement).SetDestination(player.position);
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        if ((_movement as ZombieMovement).HasReachedDestination())
        {
            if (movingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length - 1) movingForward = false;
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex <= 0) movingForward = true;
            }

            (_movement as ZombieMovement).SetDestination(waypoints[currentWaypointIndex].position);
        }
        _movement.Move(0.5f);
    }
}
