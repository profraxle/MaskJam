using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private bool isActivated;

    [SerializeField]
    private float bottomPos;
    [SerializeField]
    private float topPos;

    [SerializeField]
    private GameObject plateMesh;

    public UnityEvent onActivated = new UnityEvent();
    public UnityEvent onDeactivated = new UnityEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActivated = false;
    }

    void Update()
    {
        if (isActivated)
        {
            if (plateMesh.transform.position.y > bottomPos)
            {
                plateMesh.transform.position = plateMesh.transform.position - new Vector3(0, 0.005f, 0);
            }
            if (plateMesh.transform.position.y < bottomPos)
            {
                plateMesh.transform.position =
                    new Vector3(plateMesh.transform.position.x, bottomPos, plateMesh.transform.position.z);
            }
        }
        else
        {
            if (plateMesh.transform.position.y < topPos)
            {
                plateMesh.transform.position = plateMesh.transform.position + new Vector3(0, 0.005f, 0);
            }
            if (plateMesh.transform.position.y > topPos)
            {
                plateMesh.transform.position =
                    new Vector3(plateMesh.transform.position.x, topPos, plateMesh.transform.position.z);
            }
        }
    }
    
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Clone")
        {
            if (!isActivated)
            {
                onActivated.Invoke();
            }
            isActivated = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Clone")
        {
            if (isActivated)
            {
                onDeactivated.Invoke();
            }
            isActivated = false;
        }
    }
}
