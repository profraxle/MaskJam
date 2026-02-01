using UnityEngine;

public class TeleportMask : Mask
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropPrefab = Resources.Load<GameObject>("DropPrefabs/TeleportDrop");
		customControlTooltip = "Right Click: Teleport A Short Distance Ahead";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
