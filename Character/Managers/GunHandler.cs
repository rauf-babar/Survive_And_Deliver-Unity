using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;
using StarterAssets;

public class GunManager : WeaponManager
{
    [Header("Aiming Setup")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private Rig Aimrig;

    
  
    public override void ShowWeapon()
    {
        if (currentWeapon)
        {
            currentWeapon.gameObject.SetActive(true);
        }
            
    }

    public override void HideWeapon()
    {
        if (currentWeapon)
        {
            currentWeapon.gameObject.SetActive(false);
        }

    }

    public override void SetWeapon(Weapon w)
    {
        currentWeapon = w;
    }

    public override bool CheckWeaponExists()
    {
        return currentWeapon != null;
    }

    public void PullTrigger(Vector3 pos, Vector3 quar)
    {
        if (!CheckWeaponExists()) return;

        Gun gun = currentWeapon.GetComponent<Gun>();
        if (gun)
            gun.PullTrigger(pos, quar);

    }
    public void SetAiming(bool isAiming, Vector3 aimTarget)
    {
        if (isAiming)
        {
            ShowWeapon();
            animationManager.SetAimTrigger();
            thirdPersonController.SetRotationEnabled(false);
         
            aimVirtualCamera.Priority = 100;

            // Rotate the player to face the aim target
            Vector3 aimTargetPosition = aimTarget;
            aimTargetPosition.y = playerTransform.position.y;
            Vector3 targetDirection = (aimTargetPosition - playerTransform.position).normalized;
            playerTransform.forward = Vector3.Lerp(playerTransform.forward, targetDirection, Time.deltaTime * 20f);

            //animator.SetLayerWeight(1, 1f);
            Aimrig.weight = 1f;
        }
        else
        {
            HideWeapon();
            animationManager.ResetAimTrigger();
            thirdPersonController.SetRotationEnabled(true);
            aimVirtualCamera.Priority = 10;
            Aimrig.weight = 0f;
            //animator.SetLayerWeight(1, 0f);
        }
    }


}