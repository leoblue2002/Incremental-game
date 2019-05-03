using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpgrader : MonoBehaviour
{
    public GameObject[] Upgrades;
    public int currentlevel = 0;

    public void Upgrade ()
    {
        Upgrades[currentlevel].SetActive(true);
        if (Upgrades.Length - 1 > currentlevel)
        {
            currentlevel++;
        }
    }
}
