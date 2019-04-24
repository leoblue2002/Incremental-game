using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraSlider : MonoBehaviour
{
    public void MoveCamra (float yehaw)
    {
        Vector3 NiceVariableName = new Vector3(0,yehaw,-10);
        transform.position = NiceVariableName;
    }

    public void SlideCamra (float yehaw)
    {
        Vector3 originalposition = transform.position;
        originalposition.y += yehaw;
        transform.position = originalposition;
    }

    private void Update()
    {
        SlideCamra(Input.GetAxis("Mouse ScrollWheel"));
    }
}
