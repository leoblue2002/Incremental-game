using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRandomiser : MonoBehaviour
{
    public GameObject[] boxes = new GameObject[2];

    void Start()
    {
        //make the random number
        Instantiate(boxes[0]);
        Destroy(this.gameObject);
    }
}
