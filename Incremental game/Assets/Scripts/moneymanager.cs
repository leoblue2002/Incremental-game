using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneymanager : MonoBehaviour
{
    public int Money;
    public int MoneyPerSecond;
    public int[] MakingMoneyBoxes;

    private int[] Mpsofboxes;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddMoney", 1, 1);
        MakingMoneyBoxes = new int[2] ;
        Mpsofboxes = new int [2] {1, 10};
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
}
