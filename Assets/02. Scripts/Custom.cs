using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static SaveLoad;

public class Custom : MonoBehaviour
{
    public Text FixedPartName;
    public Text styleName;
    CharacterCustom player;
    Item item;
    string StyleCheck;
    int num = 0;
    public float min = 1;
    public float max = 3;
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;
    Vector3 slideSet;

    private void Awake()
    {
        player = FindObjectOfType<CharacterCustom>();
    }
    public void Choice(string str)
    {
        StyleCheck = str;
        styleName.text = str;
        PartSelect(num);
    }

    void ValueSet()
    {
        sliderX.minValue = min; sliderX.maxValue = max;
        sliderY.minValue = min; sliderY.maxValue = max;
        sliderZ.minValue = min; sliderZ.maxValue = max;
    }
    public void SizeCustom()
    {
        slideSet = new Vector3(sliderX.value, sliderY.value, sliderZ.value);
        if (StyleCheck == "Rotation")
        {
            player.bodyParts[num].transform.localRotation = Quaternion.Euler(slideSet);
        }
        else if (StyleCheck == "Scale")
        {
            player.bodyParts[num].transform.localScale = slideSet;
        }
    }

    public void PartSelect(int _num)
    {
        num = _num;
        FixedPartName.text = player.bodyParts[num].name;
        if (StyleCheck == "Rotation")
        {
            min = -180f; max = 180;
            ValueSet();
            Quaternion qu = player.bodyParts[num].transform.localRotation;
            sliderX.value = qu.x;
            sliderY.value = qu.y;
            sliderZ.value = qu.z;
            slideSet = new Vector3(sliderX.value, sliderY.value, sliderZ.value);
        }
        else if (StyleCheck == "Scale")
        {
            min = 1; max = 4;
            ValueSet();
            Vector3 scale = player.bodyParts[num].transform.localScale;
            sliderX.value = scale.x;
            sliderY.value = scale.y;
            sliderZ.value = scale.z;
            slideSet = new Vector3(sliderX.value, sliderY.value, sliderZ.value);
        }
    }
}
