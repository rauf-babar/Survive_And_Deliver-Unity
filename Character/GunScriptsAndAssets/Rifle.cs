using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Rifle : Gun
{
    [SerializeField] private ParticleSystem muzzleFlashPrefab;

    [SerializeField] private int maxBullets = 10;
    [SerializeField] private int usedBullets = 0;

    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private Transform Magazine;
    [SerializeField] private SmallBullet bullet;

    private bool isReloading = false;

    private Coroutine currentReloadRoutine;

    public override void PullTrigger(Vector3 spawn, Vector3 shootDir)
    {
 Debug.Log("debug before reloading");
        if (isReloading) return;

 Debug.Log("debug after reloading");
        if (usedBullets >= maxBullets)
        {
            Debug.Log("Out of ammo! Reloading...");
            Reload();
            return;
        }

        if (Time.time < coolDown) return;
        coolDown = Time.time + fireRate;
 Debug.Log("before used bullets ++");
        usedBullets++;
        //CameraManager.Instance.TriggerShake(2f);
         Debug.Log("after pulse shake");
        EmitBulletEffect();
        Debug.Log("before projectile manager");
        ProjectileManager.Instance.Shoot(bullet, 50, spawn, shootDir);

    }

    public override void Reload()
    {
        if (isReloading) return;

        AnimationManager.Instance.SetReloadTrigger();
        currentReloadRoutine = StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;
        Debug.Log("Reloading... will take " + reloadTime + " seconds.");

        yield return new WaitForSeconds(reloadTime);

        // reload done
        usedBullets = 0;
        isReloading = false;
        Debug.Log("Reload complete!");

        AnimationManager.Instance.ResetReloadTrigger();

        currentReloadRoutine = null;
    }

    public override void EmitBulletEffect()
    {
  Debug.Log("Before the Emitted log");
        if (muzzleFlashPrefab == null) return;
        
        Debug.Log("Emitted");
        AudioSource audio = muzzleFlashPrefab.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Stop();
            audio.PlayOneShot(audio.clip);
        }

        muzzleFlashPrefab.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        muzzleFlashPrefab.Play();
    }

    private void OnDisable()
    {
        if (currentReloadRoutine != null)
        {
            StopCoroutine(currentReloadRoutine);
            currentReloadRoutine = null;
        }

        AnimationManager.Instance.ResetReloadTrigger();
    }

    private void OnEnable()
    {
        // was reload interrupted?
        if (isReloading)
        {
            Debug.Log("Forcing a new reload.");
            
            isReloading = false;
            Reload();
        }
    }
}