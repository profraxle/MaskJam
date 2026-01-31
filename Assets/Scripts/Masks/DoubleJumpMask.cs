using System;
using UnityEngine;

public class DoubleJumpMask :Mask
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropPrefab = Resources.Load<GameObject>("DropPrefabs/DoubleJumpDrop");
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
