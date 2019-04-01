using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouchingGround : MonoBehaviour
{
    public int Level = 0;
    public bool IsGround = false;
    public bool IsNotGround = false;
    public bool IsPlatform = false;

    public bool IsConnectedToGround = false;
    public bool IsConnectedToPlatform = false;
    public bool Ismakingmoney = false;
    private bool WasActiveLastFrame = false;
    private bool StoreIsConnectedToGround = false;
    private bool StoreIsConnectedToPlatform = false;
    private bool StoreIsMakingMoney = false;
    private bool replace = false;
    private GameObject Moneymanager;
    private moneymanager themanager;

    void Start ()
    {
        if (this.IsGround)
        { this.IsConnectedToGround = true; }
        if (this.IsPlatform)
        { this.IsConnectedToPlatform = true; }
        Moneymanager = GameObject.FindWithTag("MoneyManager");
        themanager = Moneymanager.GetComponent<moneymanager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.IsGround && !this.IsNotGround)
        {
            IsTouchingGround other = collision.gameObject.GetComponent<IsTouchingGround>();
            if (other.IsGround || other.GetIsConnectedToGround())
            { this.StoreIsConnectedToGround = true; }

            if (other.IsPlatform || other.GetIsConnectedToPlatform())
            { this.StoreIsConnectedToPlatform = true; }
            this.replace = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!this.IsGround && !this.IsNotGround)
        {
            IsTouchingGround other = collision.gameObject.GetComponent<IsTouchingGround>();
            if (other.IsGround || other.GetIsConnectedToGround())
            { this.IsConnectedToGround = true; }

            if (other.IsPlatform || other.GetIsConnectedToPlatform())
            { this.IsConnectedToPlatform = true; }

            if (!this.IsConnectedToGround && this.IsConnectedToPlatform)
            { this.Ismakingmoney = true; }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsTouchingGround newthing = collision.gameObject.GetComponent<IsTouchingGround>();

        if (!this.IsGround && !this.IsNotGround)
        {
            if (newthing.IsGround || newthing.GetIsConnectedToGround())
            { this.StoreIsConnectedToGround = false; }

            if (newthing.IsPlatform || newthing.GetIsConnectedToPlatform())
            { this.StoreIsConnectedToPlatform = false; }
            this.replace = true;
        }
        
    }

    void LateUpdate ()
    {
        if (replace)
        {
            this.IsConnectedToGround = StoreIsConnectedToGround;
            this.IsConnectedToPlatform = StoreIsConnectedToPlatform;
        }
        replace = false;

        if (!this.IsConnectedToGround && this.IsConnectedToPlatform)
        { this.Ismakingmoney = true; }
        else 
        { this.Ismakingmoney = false; }
        this.gameObject.transform.GetChild(1).gameObject.SetActive(!this.Ismakingmoney);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(this.Ismakingmoney);

        // send active
        if (Ismakingmoney != WasActiveLastFrame)
        {
            // send the +1 or the -1
            if (Ismakingmoney)
            {
                themanager.MakingMoneyBoxes[Level] += 1;
            }
            else
            {
                themanager.MakingMoneyBoxes[Level] -= 1;
            }
            WasActiveLastFrame = Ismakingmoney;
        }
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
