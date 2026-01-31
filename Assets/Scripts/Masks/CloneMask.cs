using UnityEngine;

public class CloneMask : Mask
{
    [SerializeField] public GameObject clonePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropPrefab = Resources.Load<GameObject>("DropPrefabs/CloneDrop");
        clonePrefab = Resources.Load<GameObject>("ClonePrefab/PlayerClone");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
