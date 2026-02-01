using UnityEngine;

public class PaperBag : Mask
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropPrefab = Resources.Load<GameObject>("DropPrefabs/PaperBagDrop");
        customControlTooltip = "Right Click: Rustle Bag";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
