using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 100f;
	[SerializeField]
    private float sprintSpeedMult = 2f;
    [SerializeField]
    private float jumpHeight = 1.5f;
    [SerializeField]
    private float doubleJumpHeight = 1.5f;

    [SerializeField]
    private float teleportMaxRange = 10f;
    
    [SerializeField]
    private GroundCheck groundCheck;

    private Rigidbody rb;
    private Vector3 velocity;
    private bool grounded;
    
    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference lookAction;
	public InputActionReference sprintAction;
    public InputActionReference maskEffectAction;
 
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
        if (jumpAction.action.triggered)
        {
			DoubleJumpMask doubleJumpMask = GetComponent<DoubleJumpMask>();
			if (groundCheck.isGround) {
            	Jump();
				if (doubleJumpMask) {
					doubleJumpMask.canDoubleJump = true;
				}
			} else if (doubleJumpMask) {
				if (doubleJumpMask.canDoubleJump) {
					DoubleJump();
					doubleJumpMask.canDoubleJump = false;
				}
			}
        }
        
        // Teleport
        if (maskEffectAction.action.triggered && GetComponent<TeleportMask>())
        {
            TeleportPlayer();
        }

        if (maskEffectAction.action.triggered && GetComponent<CloneMask>())
        {
            ClonePlayer();
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
        rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }

    void DoubleJump()
    {
        rb.AddForce(transform.up * doubleJumpHeight, ForceMode.Impulse);
    }

    void MovePlayer(Vector3 move)
    {
		bool applySprint = false;
		if (GetComponent<SprintMask>() && (sprintAction.action.ReadValue<float>() != 0)) {
			applySprint = true;
		}
        Vector3 newVelocity = new Vector3(move.x* Time.fixedDeltaTime * (applySprint ? sprintSpeedMult : 1), rb.linearVelocity.y, move.z* Time.fixedDeltaTime * (applySprint ? sprintSpeedMult : 1)) ;
        rb.linearVelocity = newVelocity;
        
    }

    void TeleportPlayer()
    {
        Physics.Raycast(localCamera.position,localCamera.forward,out RaycastHit hit,teleportMaxRange);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                gameObject.transform.position = hit.point;
            }
        }
    }

    void ClonePlayer()
    {
        CloneMask cloneMask = GetComponent<CloneMask>();
        if (cloneMask)
        {
            GameObject clone = Instantiate(cloneMask.clonePrefab, gameObject.transform.position, gameObject.transform.rotation);
            clone.AddComponent(cloneMask.GetType());
            Mask newMask = clone.GetComponent<Mask>();
            Mask playerMask = gameObject.GetComponent<Mask>();
            newMask.wearPrefab = playerMask.wearPrefab;
            clone.GetComponent<WearMaskManager>().AddWearMask(cloneMask.wearPrefab);
            gameObject.GetComponent<WearMaskManager>().RemoveWearMask();
            Destroy(playerMask);
        }
    }
}

