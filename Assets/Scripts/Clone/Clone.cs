using UnityEngine;

public class Clone : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float launchForce = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PunchCallback()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 direction = (gameObject.transform.position - playerPos);
        direction.Normalize();
        Vector3 force = direction * launchForce;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
