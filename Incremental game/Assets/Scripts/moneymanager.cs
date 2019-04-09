using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneymanager : MonoBehaviour
{
    public decimal Money;
    public decimal MoneyPerSecond;
    public int[] MakingMoneyBoxes = new int[3];

    public int[] Mpsofboxes = new int[3];
    public int[] CostOfBoxes = new int[3];
    public int[] PlatformUpgradeCosts = new int[1];

    private int[] StartingCostOfBoxes;

    public Text MoneyDisplay;
    public Text MpsDisplay;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddMoney", 0, 1);
        StartingCostOfBoxes = CostOfBoxes;
    }

    void AddMoney ()
    {
        CalculateMoneyPerSecond();
        Money += MoneyPerSecond;
        UpdateDisplays();
    }

    void CalculateMoneyPerSecond ()
    {
        MoneyPerSecond = 0;
        for (int i = 0; i < MakingMoneyBoxes.Length; i++)
        {
            MoneyPerSecond += MakingMoneyBoxes[i] * Mpsofboxes[i];
        }
    }

    public void UpdateDisplays ()
    {
        MoneyDisplay.text = "Money: " + Money;
        MpsDisplay.text = "MPS: " + MoneyPerSecond;
    }

    public void RemoveMoney (int amount)
    {
        Money -= amount;
    }

    public bool CanAfford (int level)
    {
        return Money >= CostOfBoxes[level];
    }

    public bool EnoughCash (int cash)
    {
        return Money >= cash;
    }

    public bool NoActiveBoxes ()
    {
        foreach (int t in MakingMoneyBoxes)
        {
            if (t != 0)
            { return false; }
        }
        return true;
    }
}
