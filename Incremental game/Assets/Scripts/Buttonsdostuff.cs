using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonsdostuff : MonoBehaviour
{
    private bool togglehelper = false;
    public GameObject lid;

    public void spawnblock (int level)
    {

    }

    public void togglelid ()
    {
        lid.SetActive(togglehelper);
        togglehelper = !togglehelper;
    }


}
