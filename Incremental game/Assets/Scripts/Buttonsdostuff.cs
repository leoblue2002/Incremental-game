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

    public GameObject MapBoundreys;
    public GameObject SpawnLocation;
    public GameObject CamraObject;
    public GameObject CamraSliderSlider;
    public GameObject CamraSliderSliderHorizontal;
    public Text CamraUpgradeText;
    public Text PlatformUpgradeText;
    public Text NewPlatformText;

    public GameObject lid;
    public GameObject UiThing;
    public GameObject StuckUi;
    public GameObject BuyBlocksMenue;
    public GameObject BuyUpgradesMenue;

    public GameObject[] spawners = new GameObject[3];

    public Text[] ButtonLabels = new Text[4];

    private GameObject MoneyManagerObject;
    private moneymanager MMRef; 
    private SpaceIsClear SpawnLocationRef;
    private CamraSlider CamraSliderRef;



    private void Start()
    {
        MoneyManagerObject = GameObject.FindWithTag("MoneyManager");
        MMRef = MoneyManagerObject.GetComponent<moneymanager>();
        SpawnLocationRef = SpawnLocation.GetComponent<SpaceIsClear>();
        CamraSliderRef = CamraObject.GetComponent<CamraSlider>();
        UpdateButtonLabesl();
    }

    public void Spawnblock (int level)
    {
        if (MMRef.CanAfford(level) && SpawnLocationRef.YeahItsClear)
        {
            Instantiate(spawners[level]);
            MMRef.RemoveMoney(MMRef.CostOfBoxes[level]);
            MMRef.IncreaseBoxCosts(level);
            MMRef.OwnedBoxes[level] += 1;
            UpdateButtonLabesl();
        }
    }


    public void CamraUpgrades ()
    {
        if (MMRef.EnoughCash(MMRef.CamraUpgradePrice) && MMRef.CamraUpgradeLevel <= 14)
        {
            CamraSliderSlider.SetActive(true);
            CamraSliderRef.MaxUpwardRange += 1.8f;
            MMRef.RemoveMoney(MMRef.CamraUpgradePrice);
            MMRef.CamraUpgradePrice *= 1.1m;
            CamraSliderSlider.GetComponent<Slider>().maxValue = CamraSliderRef.MaxUpwardRange;
            if (MMRef.CamraUpgradeLevel <14)
            { CamraUpgradeText.text = "Upgrade Camra: " + MMRef.FormatNumbers(MMRef.CamraUpgradePrice, false); }
            else
            {
                CamraUpgradeText.text = "Camra Max Level";
            }

            MMRef.CamraUpgradeLevel++;
            
        }

        //float howbigslider = 54.9f;
        //uhh wierd codin workin on.
        //howbigslider += 30.5f;
        // internet said this would work, it didnt
        //CamraSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, howbigslider);
    }

    public void Togglelid ()
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
        for (int i = 0; i < MMRef.MakingMoneyBoxes.Length; i++)
        {
            MMRef.MakingMoneyBoxes[i] = 0;
        }
        Array.Copy(MMRef.StartingCostOfBoxes, MMRef.CostOfBoxes, MMRef.CostOfBoxes.Length);
        Array.Clear(MMRef.OwnedBoxes, 0, MMRef.OwnedBoxes.Length);
        StuckDetection();
        UpdateButtonLabesl();

    }

    public void UpdateButtonLabesl ()
    {
        for (int i = 0; i < ButtonLabels.Length; i++)
        {
            ButtonLabels[i].text = "lvl" + (i + 1) + ": $" + MMRef.FormatNumbers(MMRef.CostOfBoxes[i], false);

            //string output;
            //decimal RoundedInput = Math.Round(MoneyManagerRef.CostOfBoxes[i], 2);
            //output = RoundedInput + "";
            //if (output.IndexOf('.') != -1)
            //{
            //    if (output.Substring(output.IndexOf('.'), output.Length - output.IndexOf('.')).Length <= 2)
            //    { output = output.Substring(0, output.IndexOf('.') + 2) + "0"; }
            //    else
            //    { output = output.Substring(0, output.IndexOf('.') + 3); }
            //}
            //else
            //{
            //    output += ".00";
            //}
            //ButtonLabels[i].text = "lvl" + (i+1) + ": $" + output;
        }
        if (MMRef.PlatformUpgradeCosts[MMRef.PlatformUpgraders[MMRef.SelectedPlatform].currentlevel] != 0)
        { PlatformUpgradeText.text = "Upgrade Platform " + (MMRef.SelectedPlatform + 1) + ": " + MMRef.FormatNumbers(MMRef.PlatformUpgradeCosts[MMRef.PlatformUpgraders[MMRef.SelectedPlatform].currentlevel], false); }
        else
        { PlatformUpgradeText.text = "Upgrade Platform " + (MMRef.SelectedPlatform + 1) + ": Max Level"; }
    }

    public void StuckDetection ()
    {
        if (MMRef.Money < MMRef.CostOfBoxes[0] && MMRef.NoOwnedBoxes())
        {
            StuckUi.SetActive(true);
        }
    }
        // this whole method is wrong 
    private int CheapestLevel ()
    {
        decimal biggest = 0;
        int output = 0;
        for (int i = 0; i > MMRef.CostOfBoxes.Length; i++)
        {
            if (MMRef.CostOfBoxes[i] > biggest)
            {
                biggest = MMRef.CostOfBoxes[i];
                output = i;
            }
        }
        return output;
    }

    public void Freebie ()
    {
        Instantiate(spawners[CheapestLevel()]);
        MMRef.OwnedBoxes[CheapestLevel()] += 1;
        StuckUi.SetActive(false);
    }

    public void PlatformUpgrader ()
    {
        if (MMRef.EnoughCash(MMRef.PlatformUpgradeCosts[MMRef.PlatformUpgraders[MMRef.SelectedPlatform].currentlevel]))
        {
            MMRef.RemoveMoney(MMRef.PlatformUpgradeCosts[MMRef.PlatformUpgraders[MMRef.SelectedPlatform].currentlevel]);
            MMRef.PlatformUpgraders[MMRef.SelectedPlatform].Upgrade();
            UpdateButtonLabesl();
        }
    }

    public void BuyNewPlatform()
    {
        if (MMRef.EnoughCash(MMRef.CostOfNewPlatform))
        {
        //transform the map boundries
        Vector3 Stupid;
        Transform output = MapBoundreys.transform;

        Stupid = output.localScale;
        Stupid.x += 1;
        MapBoundreys.transform.localScale = Stupid;

        Stupid = output.localPosition;
        Stupid.x -= 8;
        MapBoundreys.transform.localPosition = Stupid;
        

        //update/activate horizontal camra slider
        CamraSliderSliderHorizontal.SetActive(true);
        CamraSliderSliderHorizontal.GetComponent<Slider>().minValue -= 16;

        //instantiate the new platform and move it to the left
        Transform LMP = MMRef.PlatformUpgraders[MMRef.PlatformUpgraders.Count - 1].transform;
        MMRef.PlatformUpgraders.Add(Instantiate(MMRef.platform, new Vector3(LMP.position.x - 16, LMP.position.y, LMP.position.z), LMP.rotation).GetComponent<PlatformUpgrader>());
            MMRef.RemoveMoney(MMRef.CostOfNewPlatform);
            MMRef.CostOfNewPlatform *= 2;
            NewPlatformText.text = "Unlock new platform: " + MMRef.FormatNumbers(MMRef.CostOfNewPlatform,false);
            //had to be down here to work
            GameObject.FindWithTag("Walls").GetComponent<ScreenEdgeWarp>().PlaySpaceWidth = MMRef.PlatformUpgraders.Count * 16 - 1;
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
