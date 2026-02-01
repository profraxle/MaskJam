using System;
using UnityEngine;

public class GlassCabinet : Punchable
{
    private GameObject brokenPrefab;
    
    [SerializeField]
    private GameObject deleteGlass;

    private void Awake()
    {
        brokenPrefab = Resources.Load("BrokenGlass") as GameObject;
    }

    public void BreakGlass()
    {
        
        Instantiate(brokenPrefab, gameObject.transform.parent.position, Quaternion.identity);
        Destroy(deleteGlass);
    }
}
