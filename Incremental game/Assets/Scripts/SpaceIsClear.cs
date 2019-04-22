using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceIsClear : MonoBehaviour
{
    public int CollisionCount;
    public bool YeahItsClear = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        CollisionCount++;
        CheckIsClear();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        CollisionCount--;
        CheckIsClear();
    }

    void CheckIsClear()
    {
        if (CollisionCount == 0)
        { YeahItsClear = true; }
        else
        { YeahItsClear = false; }
        this.gameObject.transform.GetChild(1).gameObject.SetActive(!this.YeahItsClear);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(this.YeahItsClear);
    }
}
