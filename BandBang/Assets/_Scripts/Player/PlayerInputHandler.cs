using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [Header("Other player Scripts")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerInteraction interaction;



    private InputActionMap playerMap;
    private InputAction moveAction;
    private InputAction interactAction;

    private void Awake()
    {
        playerMap = inputActions.FindActionMap("Player");

        moveAction = playerMap.FindAction("Move");
        interactAction = playerMap.FindAction("Interact");
        if (moveAction == null) Debug.LogError("Move action not found in Player action map!");
        if (interactAction == null) Debug.LogError("Interact action not found in Player action map!");

    }

    private void Update()
    {
     
        movement.moveInput = moveAction.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        playerMap.Enable();

        interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteract;

        playerMap.Disable();
    }

    private void OnInteract(InputAction.CallbackContext ctx) => interaction.Interact();
}
