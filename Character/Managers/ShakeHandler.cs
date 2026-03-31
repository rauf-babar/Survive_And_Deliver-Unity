using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShaker : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float intensity)
    {
        if (impulseSource.m_ImpulseDefinition.m_RawSignal == null)
        {
            Debug.LogWarning("[CameraShaker] No Noise Signal assigned in the Inspector! Shake won't happen.");
            return;
        }

        impulseSource.GenerateImpulse(intensity);
    }
}