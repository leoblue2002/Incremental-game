using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonsdostuff : MonoBehaviour
{
    private bool togglehelper = false;
    public GameObject lid;
    public GameObject[] spawners = new GameObject[3];
    private GameObject anoying;
    private moneymanager moneym;

    private void Start()
    {
       anoying = GameObject.FindWithTag("MoneyManager");
       moneym = anoying.GetComponent<moneymanager>();
    }

    public void spawnblock (int level)
    {
        if (moneym.CanAfford(level))
        {
            Instantiate(spawners[level]);
            moneym.RemoveMoney(moneym.CostOfBoxes[level]);
        }
    }

    public void togglelid ()
    {
        lid.SetActive(togglehelper);
        togglehelper = !togglehelper;
    }

    

}
