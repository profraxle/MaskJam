using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpHeight = 1.5f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool grounded;
    
    [Header("Inpit Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference lookAction;
 
    private float pitch =0f;
    [SerializeField] private Transform localCamera;
    
    private void Awake()
    {
        controller = gameObject.AddComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }
    
    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    void ProcessLook()
    {
        var lookInput = lookAction.action.ReadValue<Vector2>();
        
        pitch += lookInput.y * -1f;
        pitch = Mathf.Clamp(pitch, -90, 90);

        localCamera.localRotation = Quaternion.Euler(pitch, 0, 0);
        transform.Rotate(0, lookInput.x * 1f, 0);
    }
    
    void Update()
    {

        ProcessLook();
        grounded = controller.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>() ;
        Vector3 move = input.y * transform.forward + input.x * transform.right;
        move = Vector3.ClampMagnitude(move, 1f);


        // Jump
        if (jumpAction.action.triggered && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Apply gravity
        velocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = ((move * moveSpeed)) + (velocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
        
    }
}

