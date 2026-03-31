using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{
    private StarterAssetsInputs _inputReader;
    public float VerticalInput { get; private set; }
    public float HorizontalInput { get; private set; }
    public bool IsBraking { get; private set; }
    public bool IsAccelerating { get; private set; }

   

    void Update()
    {
        if (_inputReader == null)
        {
            _inputReader = FindAnyObjectByType<StarterAssetsInputs>();
            if (_inputReader == null) return;
        }

        Debug.Log($"Throttle: {_inputReader.throttle} | Steer: {_inputReader.steer}");

        HorizontalInput = _inputReader.steer;

        float throttleAmount = _inputReader.throttle;
        IsAccelerating = throttleAmount > 0.01f;

        IsBraking = _inputReader.brake;
        float brakeValue = IsBraking ? -1f : 0f;
        VerticalInput = throttleAmount + brakeValue;

    }
}