using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneymanager : MonoBehaviour
{
    public int Money;
    public int MoneyPerSecond;
    public int[] MakingMoneyBoxes;

    public int[] Mpsofboxes;
    public int[] CostOfBoxes;

    private int[] StartingCostOfBoxes;

    public Text MoneyDisplay;
    public Text MpsDisplay;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddMoney", 0, 1);
        MakingMoneyBoxes = new int[3] ;
        Mpsofboxes = new int [3] {1, 11, 121};
        CostOfBoxes = new int[3] { 20, 200, 2000 };
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
