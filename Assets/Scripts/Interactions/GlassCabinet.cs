using System;
using UnityEngine;

public class GlassCabinet : Punchable
{
    private GameObject brokenPrefab;
    
    [SerializeField]
    private GameObject deleteGlass;
    [SerializeField]
    private GameObject AudioSource;
	[SerializeField]
	private float brokenGlassDespawnTime = 10f;

    private void Awake()
    {
        brokenPrefab = Resources.Load("BrokenGlass") as GameObject;
    }

    public void BreakGlass()
    {
        AudioSource.GetComponent<AudioSource>().Play();
        GameObject brokenGlass = Instantiate(brokenPrefab, gameObject.transform.parent.position, Quaternion.identity);
		Destroy(brokenGlass, brokenGlassDespawnTime);
        Destroy(deleteGlass);
    }
}
