using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractWithMask : MonoBehaviour
{
    
    [Header("Input Actions")]
    public InputActionReference InteractAction;

    [SerializeField] private float pickupDistance = 1.5f;
    
    [SerializeField] private Transform localCamera;

    [SerializeField]
    GameObject AudioSource;
    
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
            
            if (hit.collider != null)
            {

                Mask hitMaskData = hit.collider.gameObject.GetComponentInParent<Mask>();
                
                
                if (hitMaskData != null)
                {
                    if (GetComponent<Mask>())
                    {
                        DropMask();
                    }
                    gameObject.AddComponent(hitMaskData.GetType());
                    Mask newMask = GetComponent<Mask>();
                    newMask.wearPrefab = hitMaskData.wearPrefab;
                    newMask.dropPrefab = hitMaskData.dropPrefab;
                    
                    if (gameObject.GetComponent<WearMaskManager>())
                    {
                        gameObject.GetComponent<WearMaskManager>().AddWearMask(hitMaskData.wearPrefab);  
                    }
                    Destroy(hitMaskData.gameObject);
                    AudioSource.GetComponent<AudioSource>().Play();
                }
                else
                {
                    DropMask();
                }
            }
            else
            {
                DropMask();
            }
        }
    }

    void DropMask()
    {
        if (GetComponent<Mask>())
        {
            Instantiate(gameObject.GetComponent<Mask>().dropPrefab, localCamera.position + localCamera.forward * 0.5f,
                Quaternion.identity);
            gameObject.GetComponent<WearMaskManager>().RemoveWearMask();
            Destroy(gameObject.GetComponent<Mask>());
        }
    }
    
}
