using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool isOpen;

    [SerializeField]
    private float bottomPos;
    [SerializeField]
    private float topPos;

    [SerializeField]
    private float openSpeed = 1f;

    [SerializeField]
    private float openDelay = 0f;
    [SerializeField]
    private GameObject doorMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openDelay == 0) {
            if (!isOpen)
            {
                if (doorMesh.transform.localPosition.y > bottomPos)
                {
                    doorMesh.transform.localPosition =
                        doorMesh.transform.localPosition - new Vector3(0, openSpeed * Time.fixedDeltaTime, 0);
                }

                if (doorMesh.transform.localPosition.y < bottomPos)
                {
                    doorMesh.transform.localPosition =
                        new Vector3(doorMesh.transform.localPosition.x, bottomPos, doorMesh.transform.localPosition.z);
                }
            }
            else
            {
                if (doorMesh.transform.localPosition.y < topPos)
                {
                    doorMesh.transform.localPosition =
                        doorMesh.transform.localPosition + new Vector3(0, openSpeed * Time.fixedDeltaTime, 0);
                }

                if (doorMesh.transform.localPosition.y > topPos)
                {
                    doorMesh.transform.localPosition =
                        new Vector3(doorMesh.transform.localPosition.x, topPos, doorMesh.transform.localPosition.z);
                }
            }
        }
        else
        {
            openDelay -= Time.fixedDeltaTime;
            if (openDelay < 0)
            {
                openDelay = 0;
            }
        }
    }
    
    public void SetOpen(bool open)
    {
        isOpen = open;
    }
    
    public void SetDelay(float delay)
    {
        openDelay = delay;
    }
}
