using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    public Text FixedPartName;
    public Text styleName;
    CharacterCustom custom;
    Transform player=null;
    SaveLoad.PLAYER p; // PLAYER 객체를 추가합니다.
    string StyleCheck= "Scale";
    int num = 0;
    public float min = 1;
    public float max = 4;
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;
    Vector3 slideSet;

    private void Awake()
    {        
        
        custom = FindObjectOfType<CharacterCustom>();
        if (FindObjectOfType<SaveLoad>())
            p = FindObjectOfType<SaveLoad>().player; 
        if (num == 0)
        {
            player = custom.body.transform;
            sliderX.value = p.body_x;
            sliderY.value = p.body_y;
            sliderZ.value = p.body_z;
        }
    }

    private void Update()
    {
        SizeCustom();
    }
    public void SizeCustom()
    {

        slideSet = new Vector3(sliderX.value, sliderY.value, sliderZ.value);
        if (StyleCheck == "Rotation")
        {
            player.transform.localRotation = Quaternion.Euler(slideSet);
            UpdatePlayerData();
        }
        else if (StyleCheck == "Scale")
        {
            player.transform.localScale = slideSet;
            UpdatePlayerData();
        }
    }
    public void Choice(string str)
    {
        StyleCheck = str;
        styleName.text = str;
        PartSelect(num);
    }
    public void PartSelect(int _num)
    {
        if (StyleCheck == "Rotation")
        {
            min = -180f; max = 180;
            ValueSet();
        }
        else if (StyleCheck == "Scale")
        {
            min = 1; max = 4;
            ValueSet();
        }
        if (_num==0)
        {
            player = custom.body.transform;
        }
        else if(_num==1)
        {
            player = custom.glove1.transform;
        }
        else if (_num == 2)
        {
            player = custom.glove2.transform;
        }
        else if (_num == 3)
        {
            player = custom.head.transform;
        }
        else if (_num == 4)
        {
            player = custom.tail.transform;
        }    
        num = _num;
        UpdatePlayerData();
    }
   
    void ValueSet()
    {
        sliderX.minValue = min; sliderX.maxValue = max;
        sliderY.minValue = min; sliderY.maxValue = max;
        sliderZ.minValue = min; sliderZ.maxValue = max;
    }
    void UpdatePlayerData()
    {
        if (p == null) return;
        switch (num)
        {
            case 0: // body
                p.body_x = player.localScale.x;
                p.body_y = player.localScale.y;
                p.body_z = player.localScale.z;
                p.body_rotX = player.localRotation.eulerAngles.x;
                p.body_rotY = player.localRotation.eulerAngles.y;
                p.body_rotZ = player.localRotation.eulerAngles.z;
                break;
            case 1: // glove1
                p.glove1_x = player.localScale.x;
                p.glove1_y = player.localScale.y;
                p.glove1_z = player.localScale.z;
                p.glove1_rotX = player.localRotation.eulerAngles.x;
                p.glove1_rotY = player.localRotation.eulerAngles.y;
                p.glove1_rotZ = player.localRotation.eulerAngles.z;
                break;
            case 2: // glove2
                p.glove2_x = player.localScale.x;
                p.glove2_y = player.localScale.y;
                p.glove2_z = player.localScale.z;
                p.glove2_rotX = player.localRotation.eulerAngles.x;
                p.glove2_rotY = player.localRotation.eulerAngles.y;
                p.glove2_rotZ = player.localRotation.eulerAngles.z;
                break;
            case 3: // head
                p.head_x = player.localScale.x;
                p.head_y = player.localScale.y;
                p.head_z = player.localScale.z;
                p.head_rotX = player.localRotation.eulerAngles.x;
                p.head_rotY = player.localRotation.eulerAngles.y;
                p.head_rotZ = player.localRotation.eulerAngles.z;
                break;
            case 4: // tail
                p.tail_x = player.localScale.x;
                p.tail_y = player.localScale.y;
                p.tail_z = player.localScale.z;
                p.tail_rotX = player.localRotation.eulerAngles.x;
                p.tail_rotY = player.localRotation.eulerAngles.y;
                p.tail_rotZ = player.localRotation.eulerAngles.z;
                break;
            
            default:
                Debug.LogWarning("Unknown part selected: " + num);
                break;
        }
    }
}
