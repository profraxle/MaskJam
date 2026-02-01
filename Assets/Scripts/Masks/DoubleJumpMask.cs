using System;
using UnityEngine;

public class DoubleJumpMask :Mask
{
    [SerializeField] public bool canDoubleJump;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropPrefab = Resources.Load<GameObject>("DropPrefabs/DoubleJumpDrop");

		customControlTooltip = "Space: Perform An Extra Jump Whilst Midair";
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
