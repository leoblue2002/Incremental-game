using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneymanager : MonoBehaviour
{
    public int CheatingMoney;
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
        { "", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion",
         "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion" };
    }

    void AddMoney ()
    {
        CalculateMoneyPerSecond();
        Money += MoneyPerSecond;
        if (CheatingMoney != 0)
        {
            Money = CheatingMoney;
            CheatingMoney = 0;
        }
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
        //figures out what big number should be used to describe the money
    public int HowBigNumber (decimal Monay)
    {
        decimal max = 999.999999M;
        for (int i = 0; i < 10; i++)
        {
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

    public void UpdateDisplays ()
    {
        MoneyDisplay.text = "Money:_" + FormatNumbers(Money);
        MpsDisplay.text = "Money Per Second:_" + FormatNumbers(MoneyPerSecond);
    }

    private string FormatNumbers (decimal input)
    {
        int HowBig = HowBigNumber(input);
        string moneyout;
        moneyout = (input / (decimal)Mathf.Pow(1000, HowBig)) + "";

        if (moneyout.IndexOf('.') == -1)
        {
            moneyout += ".00";
        }
        else
        {
            moneyout = moneyout.Substring(0, moneyout.IndexOf('.') + 3);
        }

        while (moneyout.Length != 6)
        {
            moneyout = "_" + moneyout;
        }

        moneyout = moneyout + "_" + BigNumberNames[HowBig];
        return moneyout;
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
