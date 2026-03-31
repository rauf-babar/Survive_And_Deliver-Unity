using UnityEngine;
using Cinemachine;
public class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject cameraManager;
    [HideInInspector] public Transform plrBody;
    [SerializeField] protected float coolDown;
    [SerializeField] protected float coolUp;

    public float GetCD()
    { 
        return coolDown;
    }

}

