using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpgrader : MonoBehaviour
{
    public GameObject[] Upgrades;
    public int currentlevel = 0;

    public void Upgrade ()
    {
        //Debug.Log("Upgrade script ran!");
        if (Upgrades.Length  > currentlevel)
        Upgrades[currentlevel].SetActive(true);

        if (Upgrades.Length > currentlevel)
        {
            currentlevel++;
        }
    }
}
