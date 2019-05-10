using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointMover : MonoBehaviour
{
    private void OnMouseDrag()
    {
        Vector3 WhyCantIEdditTheseEasier = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        WhyCantIEdditTheseEasier.z = 0;
        transform.position = WhyCantIEdditTheseEasier;
    }
}
