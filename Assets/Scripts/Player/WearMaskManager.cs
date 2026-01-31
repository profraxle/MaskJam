using UnityEngine;

public class WearMaskManager : MonoBehaviour
{
    [SerializeField] private GameObject maskSlot;
    private GameObject maskModel;
    
    
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
    }

    public void RemoveWearMask()
    {
        if (maskModel != null)
        {
            Destroy(maskModel);
        }
    }
}
