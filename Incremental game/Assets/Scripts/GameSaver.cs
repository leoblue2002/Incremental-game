using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSaver : MonoBehaviour
{
    moneymanager MMRef;

    private void Start()
    {
        MMRef = GameObject.FindWithTag("MoneyManager").GetComponent<moneymanager>();
    }

    public void SaveEverything()
    {
        GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject[] Platforms = GameObject.FindGameObjectsWithTag("Platform");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MoneyInfo.dat");

        SaveMoneyInfo HitThatMFLike = new SaveMoneyInfo();
        HitThatMFLike.Money = MMRef.Money;
        HitThatMFLike.CostOfBoxes = MMRef.CostOfBoxes;
        HitThatMFLike.CostOfNewPlatform = MMRef.CostOfNewPlatform;

        bf.Serialize(file, HitThatMFLike);
        file.Close();

    }

    // 46:35 of the video, Live Training 3 mar 2014 - data persistene

    public void LoadThatBitch ()
    {
        if (File.Exists(Application.persistentDataPath + "/MoneyInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MoneyInfo.dat",FileMode.Open);
            SaveMoneyInfo HTMFL = (SaveMoneyInfo)bf.Deserialize(file);
            file.Close();
            MMRef.Money = HTMFL.Money;
            MMRef.CostOfBoxes = HTMFL.CostOfBoxes;
            MMRef.OwnedBoxes = HTMFL.OwnedBoxes;
            MMRef.CostOfNewPlatform = HTMFL.CostOfNewPlatform;
        }
    }

}

[Serializable]
class SaveBlocks
{
    SaveBlock[] blocks;


}


[Serializable]
class SaveBlock
{
    Vector3 BlockPos;
    Quaternion rotation;
    int level;

    public SaveBlock(Vector3 pos, Quaternion rot, int lvl)
    {
        BlockPos = pos;
        rotation = rot;
        level = lvl;
    }
}

[Serializable]
class SaveMoneyInfo
{
    public decimal Money;
    public decimal[] CostOfBoxes;
    public int[] OwnedBoxes;
    public decimal CostOfNewPlatform;

    //public SaveMoneyInfo (decimal inMoney, decimal[] inCostofBoxes, decimal[] InOwnedBoxes, decimal inCostOfnewPlatform)
    //{
    //    Money = inMoney;
    //    CostOfBoxes = inCostofBoxes;
    //    OwnedBoxes = InOwnedBoxes;
    //    CostOfNewPlatform = inCostOfnewPlatform;
    //}
}