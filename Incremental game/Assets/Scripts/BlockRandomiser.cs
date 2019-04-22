using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRandomiser : MonoBehaviour
{
    public GameObject[] boxes = new GameObject[2];

    void Start()
    {
        Transform spawnpoint = GameObject.FindWithTag("SpawnPoint").transform;
        Instantiate(boxes[Random.Range(0,boxes.Length)], spawnpoint.transform.position, spawnpoint.transform.rotation);
        Destroy(this.gameObject);
    }
}
