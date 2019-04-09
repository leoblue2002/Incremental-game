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

    private string[] BigNumberNames;

    private int[] StartingCostOfBoxes;

    public Text MoneyDisplay;
    public Text MpsDisplay;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddMoney", 0, 1);
        StartingCostOfBoxes = CostOfBoxes;
        BigNumberNames = new string[12] 
        { "empty", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion",
         "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion" };
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

    public int HowDeep (decimal Monay)
    {
        for (int i = 0; i < 12; i++)
        {
            int max = 999;
            if (Monay < max)
            {
                return i;
            }
            else
            {
                max *= 1000;
            }
        }
        return 0;
    }

    public decimal Exponent (decimal basenum, int exponent)
    {
        for (int i = 1; i < exponent; i++)
            basenum *= basenum;
        return basenum;
    }

    public void UpdateDisplays ()
    {

        int ahhh = HowDeep(Money);

        MoneyDisplay.text = "Money: " + (Money / Exponent(1000,ahhh)) + BigNumberNames[ahhh];
       
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
