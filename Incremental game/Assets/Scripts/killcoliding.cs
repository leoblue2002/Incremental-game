using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killcoliding : MonoBehaviour
{
    public GameObject bm;
    private Buttonsdostuff bmref;
    void Start ()
    {
        bmref = bm.GetComponent<Buttonsdostuff>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        bmref.StuckDetection();
    }
}
