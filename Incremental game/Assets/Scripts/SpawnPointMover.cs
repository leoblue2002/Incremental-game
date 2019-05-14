using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointMover : MonoBehaviour
{
    GameObject TheMainCamra;

    private void Start()
    {
        TheMainCamra = GameObject.FindWithTag("MainCamera");
    }

    private void OnMouseDrag()
    {
        float UpLimit = TheMainCamra.transform.position.y + 4.5f;
        float DownLimit = TheMainCamra.transform.position.y - 4.5f;
        float RightLimit = TheMainCamra.transform.position.x + 8;
        float LeftLimit = TheMainCamra.transform.position.x - 8;

        Vector3 MouseClickLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (MouseClickLocation.x > RightLimit)
        { MouseClickLocation.x = RightLimit; }
        if (MouseClickLocation.x < LeftLimit)
        { MouseClickLocation.x = LeftLimit; }
        if (MouseClickLocation.y > UpLimit)
        { MouseClickLocation.y = UpLimit; }
        if (MouseClickLocation.y < DownLimit)
        { MouseClickLocation.y = DownLimit; }

        MouseClickLocation.z = 0;
        transform.position = MouseClickLocation;
    }
}
