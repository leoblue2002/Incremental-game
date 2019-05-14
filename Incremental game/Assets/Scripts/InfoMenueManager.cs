using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenueManager : MonoBehaviour
{
    moneymanager MMRef;

    bool MenueIsOpen = false;
    public GameObject TheMenue;
    public Text[] OwnedBoxes = new Text[4];
    public Text[] MpsForTheBoxes = new Text[4];
    public Text[] HowMuchEachBoxMakes = new Text[4];

    private void Start()
    {
        MMRef = GameObject.FindWithTag("MoneyManager").GetComponent<moneymanager>();
    }

    public void OpenAndUpdateInfoMenue ()
    {
        if (!MenueIsOpen)
        {
            TheMenue.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                OwnedBoxes[i].text = MMRef.OwnedBoxes[i] + "";
                MpsForTheBoxes[i].text = MMRef.FormatNumbers(MMRef.MakingMoneyBoxes[i] * MMRef.Mpsofboxes[i], false) + "";
                HowMuchEachBoxMakes[i].text = MMRef.Mpsofboxes[i] + "";
            }
            MenueIsOpen = true;
        }
        else
        {
            TheMenue.SetActive(false);
            MenueIsOpen = false;
        }
    }
}
