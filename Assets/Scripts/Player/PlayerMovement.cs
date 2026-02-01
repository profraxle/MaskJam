using System;
using UnityEditor.Animations;
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
    private float punchRange = 5f;
    
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
    public InputActionReference punchAction;

    public InputActionReference maskEffectAction;

 
    private float pitch =0f;
    [SerializeField] private Transform localCamera;

    private Vector3 finalMove;

    [SerializeField]
    private Animator animController;

    [SerializeField]
    RectTransform PauseMenuPanel;
    PauseMenuScript PauseMenu;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        PauseMenu = PauseMenuPanel.GetComponent<PauseMenuScript>();
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
        if (PauseMenu.Paused) return;

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
        
        // Rustle Bag
        if (maskEffectAction.action.triggered && GetComponent<PaperBag>())
        {
	        RustleBag();
        }

		// Punch
		if (punchAction.action.triggered)
		{
			Punch();
            animController.SetTrigger("punch");
		}
        

        // Combine horizontal and vertical movement
        finalMove = ((move * moveSpeed));
        Vector3 moveVel = rb.linearVelocity;
        moveVel.y = 0;
        if (moveVel.magnitude> 0f)
        {
            animController.SetBool("walking", true);
        }
        else
        {
            animController.SetBool("walking", false);
        }
        
        
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
                gameObject.transform.position = hit.point + transform.up;
            } else {
				Vector3 raycastEndLocation = hit.point + transform.up;
			Physics.Raycast(raycastEndLocation, -transform.up, out RaycastHit downHit, teleportMaxRange);
			if (downHit.collider != null)
			{
				if (downHit.collider.gameObject.tag == "Ground")
				{
					gameObject.transform.position = downHit.point - localCamera.forward + transform.up;
				}
			}
			}
        } else {
			Vector3 raycastEndLocation = localCamera.position + localCamera.forward * teleportMaxRange;
			Physics.Raycast(raycastEndLocation, -transform.up, out RaycastHit downHit, teleportMaxRange);
			if (downHit.collider != null)
			{
				if (downHit.collider.gameObject.tag == "Ground")
				{
					gameObject.transform.position = downHit.point + transform.up;
				}
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
            gameObject.GetComponent<WearMaskManager>().RemoveWearMask();
            Destroy(playerMask);
        }
    }

	void Punch()
	{
        Physics.Raycast(localCamera.position, localCamera.forward, out RaycastHit hit, punchRange);
        if (hit.collider != null)
        {
            Punchable punchedObject = hit.collider.gameObject.GetComponent<Punchable>();
            if (punchedObject)
            {
				if (!punchedObject.requirePunchMask) {
                	punchedObject.Punch();
				} else {
					Mask punchMask = GetComponent<PunchMask>();
					if (punchMask) {
						punchedObject.Punch();
					}
				}
            }
        }
	}

	void RustleBag()
	{
		print("Rustle rustle");
	}
}

