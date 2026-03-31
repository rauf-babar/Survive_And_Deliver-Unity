using UnityEngine;

public interface IAnimationManager
{
    void SetAnimator(Animator animator);
    void SetReloadTrigger();
    void ResetReloadTrigger();
    void SetAimTrigger();
    void ResetAimTrigger();
    void SetMeleeAttackTrigger();

    void ResetAnimations();
}