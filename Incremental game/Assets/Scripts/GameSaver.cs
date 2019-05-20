using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSaver : MonoBehaviour
{
    moneymanager MMRef;


    public void SaveEverything()
    {
        GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject[] Platforms = GameObject.FindGameObjectsWithTag("Platform");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
    }

    // 46:35 of the video, Live Training 3 mar 2014 - data persistene

    public void LoadThatBitch ()
    {
        if (File.Exists(Application.persistentDataPath + "playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter;
            FileStream file = File.Open(Application.persistentDataPath + "playerInfo.dat",FileMode.Open);
        }
    }

}

[Serializable]
class SaveBlock
{
    float x;
    float y;
    float z;
    Quaternion rotation;

}

[Serializable]
class SaveMoneyInfo
{
    decimal Money;
    decimal[] CostOfBoxes;
    decimal[] OwnedBoxes;
    decimal CostOfNewPlatform;
}