using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    moneymanager MMRef;

    public void SaveEverything ()
    {
        GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject[] Platforms = GameObject.FindGameObjectsWithTag("Platform");

    }
}

[System.Serializable]
class SaveObject
{
    float x;
    float y;
    float z;
    Quaternion rotation;

}