using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraSlider : MonoBehaviour
{
    public void MoveCamra (float yehaw)
    {
        Vector3 fuckinhell = new Vector3(0,yehaw,-10);
        transform.position = fuckinhell;
    }
}
