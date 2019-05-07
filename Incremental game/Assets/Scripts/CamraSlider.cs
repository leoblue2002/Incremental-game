using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamraSlider : MonoBehaviour
{
    public GameObject BackGround;
    public Slider CamraSlideyboyo;
    public Slider CamraSliderHorizontal;
    public float ScrolSensitivity = 1;
    public float MaxUpwardRange;
    private float MinLowerRange = 0.5f;
    public Text PlatformUpgradeText;

    private moneymanager MMRef;

    private void Start()
    {
        MMRef = GameObject.FindWithTag("MoneyManager").GetComponent<moneymanager>();
    }

    public void MoveCamra (float yehaw)
    {
        Vector3 NiceVariableName = new Vector3(transform.position.x,yehaw,-10);
        transform.position = NiceVariableName;
    }

    public void MoveCamraHorizontal(float yehaw)
    {
        Vector3 NiceVariableName = new Vector3(yehaw, transform.position.y, -10);
        transform.position = NiceVariableName;
        BackGround.transform.position = new Vector3(yehaw, BackGround.transform.position.y, BackGround.transform.position.z);
        MMRef.SelectedPlatform = UpdateSelectedPlatform();
    }

    public int UpdateSelectedPlatform()
    {
        float slidervalue = CamraSliderHorizontal.value;
        int max = 8;
        int min = -8;
        for (int i = 0; i < MMRef.PlatformUpgraders.Count; i++)
        {
            if (slidervalue <= max && slidervalue >= min)
            {
                return i;
            }
            else
            {
                max -= 16;
                min -= 16;
            }
        }
        return -1;
    }

    public void SlideCamra (float yehaw)
    {
        yehaw *= ScrolSensitivity;
        Vector3 originalposition = transform.position;
        originalposition.y += yehaw;
        if (originalposition.y < MinLowerRange)
        { originalposition.y = MinLowerRange; }
        if (originalposition.y > MaxUpwardRange)
        { originalposition.y = MaxUpwardRange; }
        transform.position = originalposition;
        CamraSlideyboyo.value = transform.position.y;
    }

    private void Update()
    {
        SlideCamra(Input.GetAxis("Mouse ScrollWheel"));
    }
}
