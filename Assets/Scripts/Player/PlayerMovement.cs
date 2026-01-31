using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 100f;
    [SerializeField]
    private float jumpHeight = 1.5f;

    private Rigidbody rb;
    private Vector3 velocity;
    private bool grounded;
    
    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference lookAction;
 
    private float pitch =0f;
    [SerializeField] private Transform localCamera;

    private Vector3 finalMove;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
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

        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>() ;
        Vector3 move = input.y * transform.forward + input.x * transform.right;
        move = Vector3.ClampMagnitude(move, 1f);


        // Jump
        if (jumpAction.action.triggered && grounded)
        {
            Jump();
        }
        

        // Combine horizontal and vertical movement
        finalMove = ((move * moveSpeed));


    }

    private void FixedUpdate()
    {
        MovePlayer(finalMove);
    }

    void Jump()
    {
    }

    void MovePlayer(Vector3 move)
    {
        Vector3 newVelocity = new Vector3(move.x* Time.fixedDeltaTime, rb.linearVelocity.y, move.z* Time.fixedDeltaTime) ;
        rb.linearVelocity = newVelocity;
        
    }
    
}

