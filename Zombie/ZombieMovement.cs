using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Animator _animator = null;
    private NavMeshAgent _agent;

    public float walkSpeed = 0.5f;
    public float runSpeed = 2f;



    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_animator == null) _animator = GetComponent<Animator>();

        
    }

    public void Move(float speed)
    {
        _agent.isStopped = false;
        _agent.speed = speed;

        if (speed == walkSpeed)
        {
            _animator.SetFloat("MoveSpeed", 1f);
            _animator.speed = 1.5f;
        }
        else
        {
            _animator.SetFloat("MoveSpeed", 2f);
            _animator.speed = 2f;
        }
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _agent.speed = 0;
        _animator.SetFloat("MoveSpeed", 0);
    }

    public void SetDestination(Vector3 target)
    {
        _agent.SetDestination(target);
    }

    public bool HasReachedDestination()
    {
        return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }
}
