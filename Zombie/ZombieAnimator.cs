using UnityEngine;

public class ZombieAnimator : MonoBehaviour, IAttackable
{
    [SerializeField] private Animator _animator = null;

    private void Awake()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
    }

    public void Attack(Transform target)
    {
        transform.LookAt(target);
        _animator.ResetTrigger("Attack");
        _animator.SetTrigger("Attack");
    }
}
