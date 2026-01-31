using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public bool isGround;


    public void OnTriggerStay(Collider other)
    {
        isGround = true;
    }

    public void OnTriggerExit(Collider other)
    {
        isGround = false;
    }
    

}
