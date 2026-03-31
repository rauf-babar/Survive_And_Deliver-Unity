using UnityEngine;

public class AnimationManager : MonoBehaviour, IAnimationManager
{
    public static IAnimationManager Instance { get; private set; }

    private Animator animator;

    public void Awake()
    {
        // Check if an instance already exists
        if (Instance != null )
        {
            Destroy(gameObject);
        }
        else
        {
            // Set the one and only instance
            Instance = this;
        }
        animator = transform.GetComponent<Animator>();
    }

    // This method fulfills the interface's requirement
    public void SetAnimator(Animator animator_)
    {
        animator = animator_;
    }

    // --- All other methods are unchanged ---
    public void SetReloadTrigger()
    {
        animator.SetInteger("Reload", 1);
    }

    public void ResetReloadTrigger()
    {
        animator.SetInteger("Reload", 0);
    }

    public void SetAimTrigger()
    {
        animator.SetBool("Aiming", true);
    }

    public void ResetAimTrigger()
    {
        animator.SetBool("Aiming", false);
    }

    public void SetMeleeAttackTrigger()
    {
        animator.SetTrigger("MeleeAttack");
    }

    public void ResetAnimations()
    {

        animator.Play("Idle", 0, 0F);
       
    }

}