using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1spawner : MonoBehaviour
{
    public GameObject[] boxes = new GameObject[2];

    void Start()
    {
        //make the random number
        Instantiate(boxes[0]);
        Destroy(this.gameObject);
    }
}
