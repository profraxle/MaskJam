using UnityEngine;

public class GodMask : Mask
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropPrefab = Resources.Load<GameObject>("DropPrefabs/GodMaskDrop");
		customControlTooltip = "Right Click: Ascend To Godhood";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
