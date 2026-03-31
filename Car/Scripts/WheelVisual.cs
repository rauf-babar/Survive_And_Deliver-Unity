using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class WheelVisual : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Check this for the two front wheels.")]
    public bool isFrontWheel = false;

    [Header("Visuals")]
    [Tooltip("How fast the wheel spins.")]
    public float spinSpeed = 2000f;

    [Tooltip("How far the wheel turns to steer.")]
    public float maxSteerAngle = 30f;

    [Tooltip("How fast the wheel steers (for smoothness).")]
    public float steerSpeed = 10f;

    private StarterAssetsInputs _inputReader;

    private float steerInput;
    private float moveInput;

    [SerializeField] Transform MainCar;
    [SerializeField] CarProperties carProperties;

    // Stored rotations
    private float currentSteerAngle = 0f;
    private float currentRollAngle = 0f;

    void Awake()
    {
        // Initialize and enable our input map

        _inputReader = FindAnyObjectByType<StarterAssetsInputs>();
        carProperties = MainCar.GetComponent<CarProperties>();
    }

    void Update()
    {
        steerInput = _inputReader.steer;

        Rigidbody rb = MainCar.GetComponent<Rigidbody>();

        if (isFrontWheel)
        {
            float targetSteerAngle = steerInput * maxSteerAngle;

            currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, Time.deltaTime * steerSpeed);
        }

        currentRollAngle += spinSpeed * (rb.linearVelocity.magnitude * Vector3.Dot(rb.linearVelocity.normalized, MainCar.forward) ) * Time.deltaTime;

        currentRollAngle %= 360f;

        
        transform.localRotation = Quaternion.Euler(currentRollAngle, currentSteerAngle, 0f);
    }
}