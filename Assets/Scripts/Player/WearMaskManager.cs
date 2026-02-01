using UnityEngine;
using UnityEngine.Events;

public class WearMaskManager : MonoBehaviour
{
    [SerializeField] private GameObject maskSlot;
    private GameObject maskModel;
    public UnityEvent maskChanged = new UnityEvent();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWearMask(GameObject maskPrefab)
    {
        if (maskModel == null)
        {
            maskModel = Instantiate(maskPrefab, maskSlot.transform);
        }
        else
        {
            Destroy(maskModel);
            maskModel = Instantiate(maskPrefab, maskSlot.transform);
        }
		maskChanged.Invoke();
    }

    public void RemoveWearMask()
    {
        if (maskModel != null)
        {
            Destroy(maskModel);
        }
		maskChanged.Invoke();
    }
}
