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

    [SerializeField]
    GameObject AudioSource;

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
            if (plateMesh.transform.localPosition.y > bottomPos)
            {
                plateMesh.transform.localPosition = plateMesh.transform.localPosition - new Vector3(0, 0.005f, 0);
            }
            if (plateMesh.transform.localPosition.y < bottomPos)
            {
                plateMesh.transform.localPosition =
                    new Vector3(plateMesh.transform.localPosition.x, bottomPos, plateMesh.transform.localPosition.z);
            }
        }
        else
        {
            if (plateMesh.transform.localPosition.y < topPos)
            {
                plateMesh.transform.localPosition = plateMesh.transform.localPosition + new Vector3(0, 0.005f, 0);
            }
            if (plateMesh.transform.localPosition.y > topPos)
            {
                plateMesh.transform.localPosition =
                    new Vector3(plateMesh.transform.localPosition.x, topPos, plateMesh.transform.localPosition.z);
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
                AudioSource.GetComponent<AudioSource>().Play();
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
                AudioSource.GetComponent<AudioSource>().Play();
            }
            isActivated = false;
        }
    }
}
