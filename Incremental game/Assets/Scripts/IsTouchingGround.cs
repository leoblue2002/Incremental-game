using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouchingGround : MonoBehaviour
{
    public int Level = 0;
    public bool IsGround = false;
    public bool IsNotGround = false;
    public bool IsPlatform = false;

    private bool IsConnectedToGround = false;
    private bool IsConnectedToPlatform = false;
    private bool IsMakingMoney = false;
    private bool WasMakingMoneyLastFrame = false;
    private bool StoreIsConnectedToGround = false;
    private bool StoreIsConnectedToPlatform = false;
    private bool Replace = false;
    private GameObject MoneyManagerObject;
    private moneymanager MoneyManagerScriptRefrence;

    void Start ()
    {
        if (this.IsGround)
        { this.IsConnectedToGround = true; }
        if (this.IsPlatform)
        { this.IsConnectedToPlatform = true; }
        MoneyManagerObject = GameObject.FindWithTag("MoneyManager");
        MoneyManagerScriptRefrence = MoneyManagerObject.GetComponent<moneymanager>();
    }
        //this fixes the problem where if an object collides with the platform then the ground it stays active
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.IsGround && !this.IsNotGround)
        {
            IsTouchingGround other = collision.gameObject.GetComponent<IsTouchingGround>();
            if (other.IsGround || other.GetIsConnectedToGround())
            { this.StoreIsConnectedToGround = true; }

            if (other.IsPlatform || other.GetIsConnectedToPlatform())
            { this.StoreIsConnectedToPlatform = true; }
            this.Replace = true;
        }
    }
        //this solves some issues where objects would be in collision with the platform but not active
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!this.IsGround && !this.IsNotGround)
        {
            IsTouchingGround other = collision.gameObject.GetComponent<IsTouchingGround>();
            if (other.IsGround || other.GetIsConnectedToGround())
            { this.IsConnectedToGround = true; }

            if (other.IsPlatform || other.GetIsConnectedToPlatform())
            { this.IsConnectedToPlatform = true; }

        }
    }

    internal void SetIsConnectedToPlatform(bool v)
    {
        IsConnectedToPlatform = v;
    }

    //this disconects the object from the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        IsTouchingGround newthing = collision.gameObject.GetComponent<IsTouchingGround>();
        if (!this.IsGround && !this.IsNotGround)
        {
            if (newthing.IsGround || newthing.GetIsConnectedToGround())
            { this.StoreIsConnectedToGround = false; }

            if (newthing.IsPlatform || newthing.GetIsConnectedToPlatform())
            { this.StoreIsConnectedToPlatform = false; }
            this.Replace = true;
        }
    }

    private void LateUpdate ()
    {
            //this is used because sometimes oncollisionstay overwrites oncollisionexit/enter
        if (Replace)
        {
            this.IsConnectedToGround = StoreIsConnectedToGround;
            this.IsConnectedToPlatform = StoreIsConnectedToPlatform;
        }
        Replace = false;
            //Determine weather or not the object should be making money
        if (!this.IsNotGround && !this.IsGround)
        {
            if (!this.IsConnectedToGround && this.IsConnectedToPlatform)
            { this.IsMakingMoney = true; }
            else
            { this.IsMakingMoney = false; }
            this.gameObject.transform.GetChild(1).gameObject.SetActive(!this.IsMakingMoney);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(this.IsMakingMoney);
        }

            // Update Global Money Manager
        if (IsMakingMoney != WasMakingMoneyLastFrame)
        {
            if (IsMakingMoney)
            {
                MoneyManagerScriptRefrence.MakingMoneyBoxes[Level] += 1;
            }
            else
            {
                MoneyManagerScriptRefrence.MakingMoneyBoxes[Level] -= 1;
            }
            WasMakingMoneyLastFrame = IsMakingMoney;
        }
    }

    public void SetIsConnectedToGround (bool input)
    {
        IsConnectedToGround = input;
    }

    public bool GetIsMakingMoney ()
    {
        return IsMakingMoney;
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
