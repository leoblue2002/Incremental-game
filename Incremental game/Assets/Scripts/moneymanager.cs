using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class moneymanager : MonoBehaviour
{
    public int CheatingMoney;
    public decimal Money;
    public decimal MoneyPerSecond;
    public int[] MakingMoneyBoxes = new int[3];
    public int[] OwnedBoxes = new int[3];

    public int[] Mpsofboxes = new int[3];
    public decimal[] CostOfBoxes;
    public int[] PlatformUpgradeCosts = new int[1];

    private string[] BigNumberNames;

    public decimal[] StartingCostOfBoxes = new decimal[4];

    public Text MoneyDisplay;
    public Text MpsDisplay;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddMoney", 0, 1);
        BigNumberNames = new string[12] 
        { "", "_Thousand", "_Million", "_Billion", "_Trillion", "_Quadrillion", "_Quintillion",
         "_Sextillion", "_Septillion", "_Octillion", "_Nonillion", "_Decillion" };
        CostOfBoxes = new decimal[4] {20, 58, 168.2m, 487.78m };
        Array.Copy(CostOfBoxes, StartingCostOfBoxes, CostOfBoxes.Length);
        OwnedBoxes[0] = 1;
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
        decimal max = 999.999999999999999999M;
        for (int i = 0; i < BigNumberNames.Length; i++)
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
        decimal uhh = Math.Round(input / (decimal)Mathf.Pow(1000, HowBig), 2);
        moneyout = uhh.ToString();

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

        moneyout = moneyout + BigNumberNames[HowBig];
        return moneyout;
    }

    public void RemoveMoney (decimal amount)
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

    public bool NoOwnedBoxes ()
    {
        bool fuckin = true;
        foreach (int t in OwnedBoxes)
        {
            if (t != 0)
            { fuckin = false; }
        }
        return fuckin;
    }

    public void IncreaseBoxCosts (int level)
    {
        decimal increaseby = 1.25M;
        CostOfBoxes[level] = CostOfBoxes[level] * increaseby;
    }

    public void DecreaseBoxCosts (int level)
    {
        decimal decreaseby = 1.25M;
        CostOfBoxes[level] = CostOfBoxes[level] / decreaseby;
        if (CostOfBoxes[level] < StartingCostOfBoxes[level])
        {
            CostOfBoxes[level] = StartingCostOfBoxes[level];
        }
    }
}
