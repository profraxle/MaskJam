using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractWithMask : MonoBehaviour
{
    
    [Header("Input Actions")]
    public InputActionReference InteractAction;

    [SerializeField] private float pickupDistance = 1.5f;
    
    [SerializeField] private Transform localCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
    {
        InteractAction.action.Enable();
    }

    void OnDisable()
    {
        InteractAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractAction.action.triggered)
        {
            print("pressed");
            Physics.Raycast(localCamera.position,localCamera.forward,out RaycastHit hit,pickupDistance);
            
            if (hit.collider != null){
                if (hit.collider.gameObject.GetComponent<Mask>())
                {
                    gameObject.AddComponent(hit.collider.gameObject.GetComponent<Mask>().GetType());
                }
            }
        }
    }
}
