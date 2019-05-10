using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEdgeWarp : MonoBehaviour
{
    public int PlaySpaceWidth = 16;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("triggered the wall!");
        if (other.transform.position.x > 1)
        {
            other.transform.Translate(new Vector3(-PlaySpaceWidth+1.2f, 0, 0), Space.World);
        }
        else
        {
            other.transform.Translate(new Vector3(PlaySpaceWidth-1.2f, 0, 0), Space.World);
        }
        other.GetComponent<mousedrag>().selected = false;
    }
}
