using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killcoliding : MonoBehaviour
{
    public GameObject bm;
    public GameObject MM;
    private moneymanager MMref;
    private Buttonsdostuff bmref;
    void Start ()
    {
        bmref = bm.GetComponent<Buttonsdostuff>();
        MMref = MM.GetComponent<moneymanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int level = collision.GetComponent<IsTouchingGround>().Level;
        MMref.OwnedBoxes[level] -= 1;
        MMref.DecreaseBoxCosts(level);
        Destroy(collision.gameObject);
        bmref.StuckDetection();
        bmref.updatebuttonlabesl();
    }
}
