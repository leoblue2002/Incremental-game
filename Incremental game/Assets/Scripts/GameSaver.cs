using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSaver : MonoBehaviour
{
    moneymanager MMRef;
    public GameObject[] BlockPrefabs;

    private void Start()
    {
        MMRef = GameObject.FindWithTag("MoneyManager").GetComponent<moneymanager>();
    }

    public void SaveEverything()
    {
        GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject[] Platforms = GameObject.FindGameObjectsWithTag("Platform");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream MoneyFile = File.Create(Application.persistentDataPath + "/MoneyInfo.dat");
        FileStream BlockFile = File.Create(Application.persistentDataPath + "/BlockInfo.dat");

        SaveMoneyInfo HitThatMFLike = new SaveMoneyInfo();
        HitThatMFLike.Money = MMRef.Money;
        HitThatMFLike.CostOfBoxes = MMRef.CostOfBoxes;
        HitThatMFLike.CostOfNewPlatform = MMRef.CostOfNewPlatform;
        HitThatMFLike.OwnedBoxes = MMRef.OwnedBoxes;

        SaveBlocks gravy = new SaveBlocks(Blocks);

        bf.Serialize(BlockFile, gravy);
        bf.Serialize(MoneyFile, HitThatMFLike);
        MoneyFile.Close();
        BlockFile.Close();
    }

    // 46:35 of the video, Live Training 3 mar 2014 - data persistene

    public void LoadThatBitch ()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/MoneyInfo.dat"))
        {

            FileStream file = File.Open(Application.persistentDataPath + "/MoneyInfo.dat",FileMode.Open);
            SaveMoneyInfo HTMFL = (SaveMoneyInfo)bf.Deserialize(file);
            file.Close();
            MMRef.Money = HTMFL.Money;
            MMRef.CostOfBoxes = HTMFL.CostOfBoxes;
            MMRef.OwnedBoxes = HTMFL.OwnedBoxes;
            MMRef.CostOfNewPlatform = HTMFL.CostOfNewPlatform;
        }

        if (File.Exists(Application.persistentDataPath + "/BlockInfo.dat"))
        {

            GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
            for (int i = 0; i < Blocks.Length; i++)
            {
                Destroy(Blocks[i]);
            }

            FileStream BlockFile = File.Open(Application.persistentDataPath + "/BlockInfo.dat", FileMode.Open);
            SaveBlocks SavedBlocks = (SaveBlocks)bf.Deserialize(BlockFile);
            BlockFile.Close();

            foreach (SaveBlock CBlock in SavedBlocks.blocks)
            {
                Vector3 BlockPos = new Vector3(CBlock.x, CBlock.y, CBlock.z);
                Quaternion quat = new Quaternion(CBlock.qx, CBlock.qy, CBlock.qz, CBlock.qw);
                Instantiate(BlockPrefabs[CBlock.level], BlockPos, quat);
            }
        }
    }

}

[Serializable]
class SaveBlocks
{
    public SaveBlock[] blocks;

    public SaveBlocks (GameObject[] inblocks)
    {
        blocks = new SaveBlock[inblocks.Length];
        for (int i = 0; i < inblocks.Length; i++)
        {
            Transform CBlock = inblocks[i].transform;
            Vector3 CPos = CBlock.position;
            Quaternion CRot = CBlock.rotation;
            int lvl = CBlock.GetComponent<IsTouchingGround>().Level;
            blocks[i] = new SaveBlock(CPos.x, CPos.y, CPos.z, CRot.x, CRot.y, CRot.z, CRot.w, lvl);
        }
    }
}


[Serializable]
class SaveBlock
{
    public float x;
    public float y;
    public float z;
    public float qx;
    public float qy;
    public float qz;
    public float qw;
    public int level;

    public SaveBlock(float x, float y, float z,float qx, float qy, float qz, float qw, int lvl)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.qx = qx;
        this.qy = qy;
        this.qz = qz;
        this.qw = qw;
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