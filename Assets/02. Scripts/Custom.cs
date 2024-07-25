using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    enum KEYGET {UP,DOWN,RIGHT,LEFT }
    public Text FixedPartName;
    public Text styleName;
    CharacterCustom player;
    Item item;
    string StyleCheck="Position";
    int num=0;
    public float min=1;
    public float max=3;
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;
    Vector3 slideSet;
    Quaternion slideRot;
     
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
        sliderX.minValue = min;        sliderX.maxValue = max;
        sliderY.minValue = min;        sliderY.maxValue = max;
        sliderZ.minValue = min;        sliderZ.maxValue = max;
        slideSet = new Vector3(sliderX.value, sliderY.value, sliderZ.value);
    }
    public void SizeCustom()
    {
        if (StyleCheck == "Position")
        {
            if (num == 2 || num == 1)
            {
                player.bodyParts[1].transform.position += slideSet;
                player.bodyParts[1].transform.position += slideSet;
            }
            else
                player.bodyParts[1].transform.position += slideSet;
        }
        else if (StyleCheck == "Rotation")
        {
            if (num == 2 || num == 1)
            {
                player.bodyParts[1].transform.Rotate(slideSet, Space.Self);
                player.bodyParts[2].transform.Rotate(slideSet, Space.Self);
            }
            else
                player.bodyParts[num].transform.Rotate(slideSet, Space.Self);
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
        ValueSet();
    }
    public void StyleSet(string str)
    {
        StyleCheck = str;
        PartSelect(num);
    } 
    public void PartSelect(int _num)
    {        
        num = _num;
        FixedPartName.text = player.bodyParts[num].name;
        if (StyleCheck == "Position")
        {
            min = -0.5f; max = 0.5f;
            ValueSet();
            slideSet = player.bodyParts[num].transform.position;           
        }
        else if (StyleCheck == "Rotation")
        {
            min = -180f; max = 180;
            ValueSet();
            Quaternion qu = new Quaternion(sliderX.value, sliderY.value, sliderZ.value, 1);
            qu= player.bodyParts[num].transform.localRotation;            
        }
        else if (StyleCheck == "Scale")
        {
            min = 1; max = 4;
            ValueSet();
            slideSet = player.bodyParts[num].transform.localScale;        
        }        
       
    }
}
