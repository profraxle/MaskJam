using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CCTVView : MonoBehaviour
{
    GameObject Player;
    [SerializeField]
    float ViewAngle = 30;
    [SerializeField]
    float ViewRange = 15;
    bool PlayerInView = false;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");

        Light Spotlight = GetComponentInChildren<Light>();
        Spotlight.spotAngle = 2 * ViewAngle;
        Spotlight.innerSpotAngle = 1.9f * ViewAngle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 VectorToPlayer = Player.transform.position - transform.position;
        float AngleToPlayer = Vector3.Angle(transform.forward, VectorToPlayer);

        if(!PlayerInView)
        {
            if (Player.GetComponent<Mask>() == null && AngleToPlayer <= ViewAngle && VectorToPlayer.magnitude <= ViewRange)
            {
                PlayerInView = true;
                Player.GetComponent<PlayerDeathScript>().OnPlayerEnteredCCTV();
            }
        }
        else
        {
            if (Player.GetComponent<Mask>() != null || !(AngleToPlayer <= ViewAngle && VectorToPlayer.magnitude <= ViewRange)) {
                PlayerInView = false;
                Player.GetComponent<PlayerDeathScript>().OnPlayerLeftCCTV();
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Quaternion leftRayRotation = Quaternion.AngleAxis(-ViewAngle, transform.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(ViewAngle, transform.up);
        Quaternion upRayRotation = Quaternion.AngleAxis(ViewAngle, transform.right);
        Quaternion downRayRotation = Quaternion.AngleAxis(-ViewAngle, transform.right);
        Gizmos.DrawRay(transform.position, leftRayRotation * transform.forward * ViewRange);
        Gizmos.DrawRay(transform.position, rightRayRotation * transform.forward * ViewRange);
        Gizmos.DrawRay(transform.position, upRayRotation * transform.forward * ViewRange);
        Gizmos.DrawRay(transform.position, downRayRotation * transform.forward * ViewRange);
    }
}
