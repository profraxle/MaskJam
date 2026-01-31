using UnityEngine;

public class SprintMask : Mask
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropPrefab = Resources.Load<GameObject>("DropPrefabs/SprintDrop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
