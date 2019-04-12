using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Buttonsdostuff : MonoBehaviour
{
    private bool togglehelper = false;
    private bool togglehelper2 = false;
    private bool togglehelper3 = false;

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
    

    private void Start()
    {
       MoneyManagerObject = GameObject.FindWithTag("MoneyManager");
       MoneyManagerRef = MoneyManagerObject.GetComponent<moneymanager>();
    }

    public void spawnblock (int level)
    {
        if (MoneyManagerRef.CanAfford(level))
        {
            Instantiate(spawners[level]);
            MoneyManagerRef.RemoveMoney((int)MoneyManagerRef.CostOfBoxes[level]);
            MoneyManagerRef.IncreaseBoxCosts(level);
            updatebuttonlabesl();
        }
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
        updatebuttonlabesl();

    }

    public void updatebuttonlabesl ()
    {
        for (int i = 0; i < ButtonLabels.Length; i++)
        {
            ButtonLabels[i].text = "lvl" + (i+1) + ": $" + MoneyManagerRef.CostOfBoxes[i];
        }
    }

    public void StuckDetection ()
    {
        if (MoneyManagerRef.Money < MoneyManagerRef.CostOfBoxes[0])
        {
            StuckUi.SetActive(true);
        }
    }

    public void Freebie ()
    {
        Instantiate(spawners[0]);
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
        BuyBlocksMenue.SetActive(togglehelper2);
        togglehelper2 = !togglehelper2;
    }

    public void ToggleUpgradeBuy ()
    {
        BuyUpgradesMenue.SetActive(togglehelper3);
        togglehelper3 = !togglehelper3;
    }
}
