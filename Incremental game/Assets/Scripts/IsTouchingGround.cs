using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouchingGround : MonoBehaviour
{

    public bool IsOnGround = false;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {



    }

    private void OnCollisionExit(Collision collision)
    {
        IsOnGround = false;
    }

    public bool GetIsOnGround ()
    {
        return IsOnGround;
    }

}
