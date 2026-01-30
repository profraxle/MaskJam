using UnityEngine;

public class CCTVView : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    [SerializeField]
    float ViewAngle = 30;

    [SerializeField]
    float ViewRange = 15;

    // Update is called once per frame
    void Update()
    {
        Vector3 VectorToPlayer = Player.transform.position - transform.position;
        float AngleToPlayer = Vector3.Angle(transform.forward, VectorToPlayer);

        if(AngleToPlayer <= ViewAngle && VectorToPlayer.magnitude <= ViewRange)
        {
            Player.GetComponent<PlayerDeathScript>().OnPlayerEnteredCCTV();
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
