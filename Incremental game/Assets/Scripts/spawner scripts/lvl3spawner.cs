using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl3spawner : MonoBehaviour
{
    public GameObject[] boxes = new GameObject[1];

    void Start()
    {
        //make the random number
        Instantiate(boxes[0]);
        Destroy(this.gameObject);
    }
}
