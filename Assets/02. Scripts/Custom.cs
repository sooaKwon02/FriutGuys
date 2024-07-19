using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Custom : MonoBehaviour
{
    enum KEYGET { UP, DOWN, RIGHT, LEFT }
    public Text FixedPartName;
    public Text styleName;
    CharacterCustom player;
    Item item;
    string StyleCheck = "Position";
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

        if (StyleCheck == "Position")
        {

            if (num == 2 || num == 1)
            {
                player.bodyParts[1].transform.localPosition = slideSet;
                player.bodyParts[2].transform.localPosition = slideSet;
            }
            else
                player.bodyParts[num].transform.localPosition = slideSet;
        }
        else if (StyleCheck == "Rotation")
        {
            if (num == 2 || num == 1)
            {
                player.bodyParts[1].transform.localRotation=Quaternion.Euler(slideSet);
                player.bodyParts[2].transform.localRotation = Quaternion.Euler(slideSet);
            }
            else
                player.bodyParts[num].transform.localRotation = Quaternion.Euler(slideSet);
        }
        else if (StyleCheck == "Scale")
        {
            if (num == 2 || num == 1)
            {
                player.bodyParts[1].transform.localScale = slideSet;
                player.bodyParts[2].transform.localScale = slideSet;
            }
            else
                player.bodyParts[num].transform.localScale = slideSet;
        }
    }
    //public void StyleSet(string str)
    //{
    //    StyleCheck = str;  
    //}
    public void PartSelect(int _num)
    {
        num = _num;
        FixedPartName.text = player.bodyParts[num].name;
        if (StyleCheck == "Position")
        {
            min = -0.5f; max = 0.5f;
            ValueSet();
            Vector3 pos= player.bodyParts[num].transform.localPosition;
            sliderX.value = pos.x;
            sliderY.value = pos.y;
            sliderZ.value = pos.z;
            slideSet = new Vector3(sliderX.value, sliderY.value, sliderZ.value);
        }
        else if (StyleCheck == "Rotation")
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
            slideSet= new Vector3(sliderX.value, sliderY.value, sliderZ.value);
        }

    }
}
