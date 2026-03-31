using UnityEngine;
using StarterAssets;
using UnityEditor.Rendering;

public abstract class WeaponManager : MonoBehaviour
{

    [SerializeField] protected Weapon currentWeapon;
    [SerializeField] protected IAnimationManager animationManager;
    protected ThirdPersonController thirdPersonController;
    protected Transform playerTransform;

    public void Awake()
    {
        

        thirdPersonController = GetComponent<ThirdPersonController>();
        playerTransform = transform;
    }

    public void Start()
    {
        animationManager = AnimationManager.Instance;
    }
    public abstract void ShowWeapon();
    public abstract void HideWeapon();

    public abstract void SetWeapon(Weapon w);

    public abstract bool CheckWeaponExists();

}