using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonsdostuff : MonoBehaviour
{
    private bool togglehelper = false;
    public GameObject lid;
    public GameObject UiThing;
    public GameObject StuckUi;
    public GameObject[] spawners = new GameObject[3];
    public GameObject[] PlatformUpgrades = new GameObject[1];

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
            MoneyManagerRef.RemoveMoney(MoneyManagerRef.CostOfBoxes[level]);
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

}
