using UnityEngine;
using UnityEngine.InputSystem; // Needed for PlayerInput
using StarterAssets;           // Needed to find ThirdPersonController

public class CarInteractionHandler : MonoBehaviour
{
    [Header("Dependencies")]
    // This is the physics script on the car
    private CarPhysicsController carPhysicsScript;
    private CarInputHandler carInputHandler;

    [Tooltip("Create an empty GameObject where the player should spawn when they leave")]
    [SerializeField] private Transform exitPoint;

    [Header("Settings")]
    [SerializeField] private CameraType carCameraType = CameraType.Vehicle;

    // These variables store the player references
    private GameObject playerObject;
    private PlayerInput playerInput; // <--- THIS is the variable you were missing
    private CharacterController playerController;
    private ThirdPersonController playerMovementScript;

    private bool isDriving = false;

    private void Awake()
    {
        // Find the physics script on THIS car object
        carPhysicsScript = GetComponent<CarPhysicsController>();
        carInputHandler = GetComponent<CarInputHandler>();
    }

    private void Start()
    {
        // Ensure the car starts turned off
        if (carPhysicsScript != null)
        {
             carPhysicsScript.enabled = false;
            carInputHandler.enabled = false;
        }
           
    }

    private void Update()
    {

        if (isDriving && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            ExitCar();
        }
    }

    public void EnterCar()
    {
        // 1. Find Player References (if we haven't already)
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerInput = playerObject.GetComponent<PlayerInput>();
                playerController = playerObject.GetComponent<CharacterController>();
                playerMovementScript = playerObject.GetComponent<ThirdPersonController>();
            }
        }

        Debug.Log("Player found?");

        if (playerObject == null) return;
        Debug.Log("Player found");

        isDriving = true;

        if (playerController) playerController.enabled = false;
        if (playerMovementScript) playerMovementScript.enabled = false;

        AnimationManager.Instance.ResetAnimations();

        playerObject.transform.SetParent(this.transform);
        playerObject.transform.localPosition = Vector3.zero;
        playerObject.transform.localRotation = Quaternion.identity;

        


        Renderer[] allRenderers = playerObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in allRenderers) r.enabled = false;

        if (playerInput != null)
        {
            Debug.Log("Changed Input to Vehicle");
            playerInput.SwitchCurrentActionMap("Vehicle");
        }
            
        carPhysicsScript.enabled = true;
        carInputHandler.enabled = true;

        Debug.Log("Switched Camera to Car");
        CameraManager.Instance.SwitchCamera(carCameraType);
    }

    private void ExitCar()
    {
        isDriving = false;

        playerObject.transform.SetParent(null);

        if (exitPoint != null)
        {
            playerObject.transform.position = exitPoint.position;
            playerObject.transform.rotation = exitPoint.rotation;
        }
        else
        {
            playerObject.transform.position = transform.position + transform.right * 2f;
            playerObject.transform.rotation = Quaternion.identity;
        }

        playerObject.transform.localScale = Vector3.one;

        Renderer[] allRenderers = playerObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in allRenderers)
        {
            r.enabled = true;
        }

        AnimationManager.Instance.ResetAnimations();

        if (playerMovementScript) playerMovementScript.enabled = true;
        if (playerController) playerController.enabled = true;

        if (playerInput != null)
            playerInput.SwitchCurrentActionMap("Player");

        carPhysicsScript.enabled = false;
        carInputHandler.enabled = false;

        CameraManager.Instance.SwitchCamera(CameraType.Player);
    }
}