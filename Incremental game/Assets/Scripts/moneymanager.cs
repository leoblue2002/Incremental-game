using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneymanager : MonoBehaviour
{
    public int Money;
    public int MoneyPerSecond;
    public int ActiveLvl1s;
    public int ActiveLvl2s;
    public int ActiveLvl3s

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddMoney", 1, 1);
    }

    void AddMoney ()
    {
        CalculateMoneyPerSecond();
        Money += MoneyPerSecond;
    }

    void CalculateMoneyPerSecond ()
    {
        MoneyPerSecond = 1 * ActiveLvl1s + 10 * ActiveLvl2s;
    }
}
