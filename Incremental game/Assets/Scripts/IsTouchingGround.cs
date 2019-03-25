using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouchingGround : MonoBehaviour
{
    public bool IsGround = false;
    public bool IsNotGround = false;
    public bool IsPlatform = false;
    public bool IsConnectedToGround = false;
    public bool IsConnectedToPlatform = false;
    private bool IsActive = false;
    private bool UggGround = false;
    private bool UggPlatform = false;
    private bool replace = false;


    void start ()
    {
        if (this.IsGround)
        { this.IsConnectedToGround = true; }
        if (this.IsPlatform)
        { this.IsConnectedToPlatform = true; }

    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!this.IsGround && !this.IsNotGround)
        {
            IsTouchingGround other = collision.gameObject.GetComponent<IsTouchingGround>();
            if (other.IsGround || other.IsConnectedToGround)
            { this.IsConnectedToGround = true; }

            if (other.IsPlatform || other.IsConnectedToPlatform)
            { this.IsConnectedToPlatform = true; }

            if (!this.IsConnectedToGround && this.IsConnectedToPlatform)
            { this.IsActive = true; }

            this.gameObject.transform.GetChild(1).gameObject.SetActive(!this.IsActive);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(this.IsActive);

           // Debug.Log(this.name + "just entered");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsTouchingGround newthing = collision.gameObject.GetComponent<IsTouchingGround>();

        if (!this.IsGround && !this.IsNotGround)
        {
            Debug.Log("collision exit happened!");
            if (newthing.IsGround || newthing.IsConnectedToGround)
            { this.UggGround = false; }

            if (newthing.IsPlatform || newthing.IsConnectedToPlatform)
            { this.UggPlatform = false; }

            this.replace = true;

            this.gameObject.transform.GetChild(1).gameObject.SetActive(!this.IsActive);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(this.IsActive);
           // Debug.Log(this.name + "just exited");
        }
        
    }

    void LateUpdate ()
    {
        if (replace)
        {
            this.IsConnectedToGround = UggGround;
            this.IsConnectedToPlatform = UggPlatform;
            this.IsActive = false;
            this.gameObject.transform.GetChild(1).gameObject.SetActive(!this.IsActive);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(this.IsActive);

        }
        replace = false;
    }

    bool GetIsConnectedToPlatform ()
    {
        return IsConnectedToPlatform;
    }

    bool GetIsConnectedToGround ()
    {
        return IsConnectedToGround;
    }
}
