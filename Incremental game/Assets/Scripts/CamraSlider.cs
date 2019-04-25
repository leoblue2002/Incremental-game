using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraSlider : MonoBehaviour
{
    public float MaxUpwardRange;
    private float MinLowerRange = 0.5f;

    public void MoveCamra (float yehaw)
    {
        Vector3 NiceVariableName = new Vector3(0,yehaw,-10);
        transform.position = NiceVariableName;
    }

    public void SlideCamra (float yehaw)
    {
        Vector3 originalposition = transform.position;
        originalposition.y += yehaw;
        if (originalposition.y < MinLowerRange)
        { originalposition.y = MinLowerRange; }
        if (originalposition.y > MaxUpwardRange)
        { originalposition.y = MaxUpwardRange; }
        transform.position = originalposition;
    }

    private void Update()
    {
        SlideCamra(Input.GetAxis("Mouse ScrollWheel"));
    }
}
