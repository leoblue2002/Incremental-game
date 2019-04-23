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
        MoneyDisplay.text = "Money:_" + FormatNumbers(Money,true);
        MpsDisplay.text = "Money Per Second:_" + FormatNumbers(MoneyPerSecond,true);
    }

    public string FormatNumbers (decimal input,bool lockdecimalpoint)
    {
        int HowBig = HowBigNumber(input);
        string Output;
        decimal RoundedInput = Math.Round(input / (decimal)Mathf.Pow(1000, HowBig), 2);
        Output = RoundedInput.ToString();

        if (Output.IndexOf('.') == -1)
        {
            Output += ".00";
        }
        else
        {
            if (Output.Substring(Output.IndexOf('.'), Output.Length - Output.IndexOf('.')).Length <= 2)
            { Output = Output.Substring(0, Output.IndexOf('.') + 2) + "0"; }
            else
            { Output = Output.Substring(0, Output.IndexOf('.') + 3); }
        }
        if (lockdecimalpoint)
        {
            while (Output.Length != 6)
            {
                Output = "_" + Output;
            }
        }
        Output = Output + BigNumberNames[HowBig];
        return Output;
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
        bool yehaw = true;
        foreach (int t in OwnedBoxes)
        {
            if (t != 0)
            { yehaw = false; }
        }
        return yehaw;
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
