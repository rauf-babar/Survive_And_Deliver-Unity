using UnityEngine;
using Cinemachine;
using System.Collections.Generic;


public enum CameraType
{
    Player,
    Vehicle,
    Aim
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [System.Serializable]
    public struct CameraEntry
    {
        public CameraType Type;
        public CinemachineVirtualCameraBase CameraObject;
    }

    [Header("Dependencies")]
    [SerializeField] private CameraShaker cameraShaker;

    [Header("Cameras")]
    [SerializeField] private List<CameraEntry> definedCameras;

    private Dictionary<CameraType, CinemachineVirtualCameraBase> cameraMap;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (cameraShaker == null)
        {
            cameraShaker = GetComponent<CameraShaker>();
        }

        cameraMap = new Dictionary<CameraType, CinemachineVirtualCameraBase>();
        foreach (var entry in definedCameras)
        {
            if (entry.CameraObject != null)
            {
                cameraMap[entry.Type] = entry.CameraObject;
                entry.CameraObject.Priority = 10;
            }
        }
    }

    private void Start()
    {
        SwitchCamera(CameraType.Player);
    }

    public void SwitchCamera(CameraType type)
    {
        if (!cameraMap.ContainsKey(type)) return;

        foreach (var cam in cameraMap.Values)
        {
            cam.Priority = 10;
        }

        cameraMap[type].Priority = 100;
    }

    public void TriggerShake(float force)
    {
        if (cameraShaker != null)
        {
            cameraShaker.Shake(force);
        }
    }
}