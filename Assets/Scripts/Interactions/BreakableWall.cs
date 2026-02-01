using System;
using UnityEngine;

public class BreakableWall : Punchable
{
    private GameObject brokenPrefab;
    
    [SerializeField]
    private GameObject deleteWall;

	[SerializeField]
	private float brokenGlassDespawnTime = 2f;

    private void Awake()
    {
        brokenPrefab = Resources.Load("BrokenWall") as GameObject;
    }

    public void BreakGlass()
    {
        GameObject brokenGlass = Instantiate(brokenPrefab, gameObject.transform.parent.position, Quaternion.identity);
		Destroy(brokenGlass, brokenGlassDespawnTime);
        Destroy(deleteWall);
    }
}
