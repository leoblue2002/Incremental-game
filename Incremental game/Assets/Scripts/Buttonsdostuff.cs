using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Buttonsdostuff : MonoBehaviour
{
    private bool togglehelper = false;
    private bool blockmenueisopen = false;
    private bool UpgradeMenueIsOpen = false;

    public GameObject SpawnLocation;
    public GameObject CamraSlider;
    
    public GameObject lid;
    public GameObject UiThing;
    public GameObject StuckUi;
    public GameObject BuyBlocksMenue;
    public GameObject BuyUpgradesMenue;

    public GameObject[] spawners = new GameObject[3];
    public GameObject[] PlatformUpgrades = new GameObject[1];
    public Text[] ButtonLabels = new Text[4];

    private GameObject MoneyManagerObject;
    private moneymanager MoneyManagerRef;
    private SpaceIsClear SpawnLocationRef;


    private void Start()
    {
        MoneyManagerObject = GameObject.FindWithTag("MoneyManager");
        MoneyManagerRef = MoneyManagerObject.GetComponent<moneymanager>();
        SpawnLocationRef = SpawnLocation.GetComponent<SpaceIsClear>();
    }

    public void spawnblock (int level)
    {
        if (MoneyManagerRef.CanAfford(level) && SpawnLocationRef.YeahItsClear)
        {
            Instantiate(spawners[level]);
            MoneyManagerRef.RemoveMoney(MoneyManagerRef.CostOfBoxes[level]);
            MoneyManagerRef.IncreaseBoxCosts(level);
            MoneyManagerRef.OwnedBoxes[level] += 1;
            updatebuttonlabesl();
        }
    }

    public void CamraUpgrades ()
    {
        float maxrange = 2.3f;
        float howbigslider = 54.9f;

        
        
        //uhh wierd codin workin on.
        maxrange += 1.8f;
        howbigslider += 30.5f;
        // internet said this would work, it didnt
        CamraSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, howbigslider);
    }

    public void togglelid ()
    {
        lid.SetActive(togglehelper);
        togglehelper = !togglehelper;
    }

    public void ActivateKillMenue (bool yeah)
    {
        UiThing.SetActive(yeah);
    }

    public void KillEverything (string tag)
    {
        GameObject[] foundobjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in foundobjects)
        {
            Destroy(target);
        }
        for (int i = 0; i < MoneyManagerRef.MakingMoneyBoxes.Length; i++)
        {
            MoneyManagerRef.MakingMoneyBoxes[i] = 0;
        }
        Array.Copy(MoneyManagerRef.StartingCostOfBoxes, MoneyManagerRef.CostOfBoxes, MoneyManagerRef.CostOfBoxes.Length);
        Array.Clear(MoneyManagerRef.OwnedBoxes, 0, MoneyManagerRef.OwnedBoxes.Length);
        StuckDetection();
        updatebuttonlabesl();

    }

    public void updatebuttonlabesl ()
    {
        for (int i = 0; i < ButtonLabels.Length; i++)
        {
            string output;
            decimal uhh = Math.Round(MoneyManagerRef.CostOfBoxes[i], 2);
            output = uhh + "";
            if (output.IndexOf('.') != -1)
            {
                if (output.Substring(output.IndexOf('.'), output.Length - output.IndexOf('.')).Length <= 2)
                { output = output.Substring(0, output.IndexOf('.') + 2) + "0"; }
                else
                { output = output.Substring(0, output.IndexOf('.') + 3); }
            }
            else
            {
                output += ".00";
            }
            ButtonLabels[i].text = "lvl" + (i+1) + ": $" + output;
        }
    }

    public void StuckDetection ()
    {
        Debug.Log(MoneyManagerRef.Money);
        Debug.Log(MoneyManagerRef.CostOfBoxes[0]);
        Debug.Log(MoneyManagerRef.NoOwnedBoxes());

        if (MoneyManagerRef.Money < MoneyManagerRef.CostOfBoxes[0] && MoneyManagerRef.NoOwnedBoxes())
        {
            StuckUi.SetActive(true);
        }
    }

    public void Freebie ()
    {
        Instantiate(spawners[0]);
        MoneyManagerRef.OwnedBoxes[0] += 1;
        StuckUi.SetActive(false);
    }

    public void PlatformUpgrader (int level)
    {
        if (MoneyManagerRef.EnoughCash(MoneyManagerRef.PlatformUpgradeCosts[level]))
        {
            PlatformUpgrades[level].SetActive(true);
            MoneyManagerRef.RemoveMoney(MoneyManagerRef.PlatformUpgradeCosts[level]);
        }
    }

    public void ToggleBlockBuy ()
    {
        blockmenueisopen = !blockmenueisopen;
        if (blockmenueisopen)
        { UpgradeMenueIsOpen = false; }
        BuyBlocksMenue.SetActive(blockmenueisopen);
        BuyUpgradesMenue.SetActive(UpgradeMenueIsOpen);
 
    }

    public void ToggleUpgradeBuy ()
    {
        UpgradeMenueIsOpen = !UpgradeMenueIsOpen;
        if (UpgradeMenueIsOpen)
        { blockmenueisopen = false; }
        BuyUpgradesMenue.SetActive(UpgradeMenueIsOpen);
        BuyBlocksMenue.SetActive(blockmenueisopen);
    }
}
