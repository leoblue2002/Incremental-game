using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneymanager : MonoBehaviour
{
    public int Money;
    public int MoneyPerSecond;
    public int[] MakingMoneyBoxes;

    private int[] Mpsofboxes;
    public int[] CostOfBoxes;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddMoney", 1, 1);
        MakingMoneyBoxes = new int[3] ;
        Mpsofboxes = new int [3] {1, 15, 225};
        CostOfBoxes = new int[3] { 20, 200, 2000 };
    }

    void AddMoney ()
    {
        CalculateMoneyPerSecond();
        Money += MoneyPerSecond;
    }

    void CalculateMoneyPerSecond ()
    {
        MoneyPerSecond = 0;
        for (int i = 0; i < MakingMoneyBoxes.Length; i++)
        {
            MoneyPerSecond += MakingMoneyBoxes[i] * Mpsofboxes[i];
        }
    }

    public void RemoveMoney (int amount)
    {
        Money -= amount;
    }

    public bool CanAfford (int level)
    {
        return Money >= CostOfBoxes[level];
    }
}
