using UnityEngine;
using UnityEngine.Rendering;

public class MeleeManager : WeaponManager
{
    private float lastMeleeAttackTime = -999f;


    public override void ShowWeapon()
    {
        if (currentWeapon)
        {
            Debug.Log("Enabling Gun");
            currentWeapon.gameObject.SetActive(true);
        }

    }

    public override void HideWeapon()
    {
        if (currentWeapon)
        {
            Debug.Log("Enabling Gun");
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

    public void PerformAttack()
    {

        if (!CheckWeaponExists()) return;

        Melee meleeWeapon = currentWeapon.GetComponent<Melee>();
        ShowWeapon();
        if (meleeWeapon == null) return;

        float weaponCooldown = meleeWeapon.GetCD();

        if (Time.time - lastMeleeAttackTime < weaponCooldown)
        {
            return;
        }
        lastMeleeAttackTime = Time.time;


        Debug.Log("Melee Attack Performed!");
        animationManager.SetMeleeAttackTrigger();

        meleeWeapon.SwingWeapon();
    }
}