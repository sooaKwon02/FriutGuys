using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    public Text fixedPartName;
    public Text styleName;
    LobbyCharacterCustom custom;
    Transform player=null;
    SaveLoad.PLAYER p; 
    string StyleCheck= "Scale";
    int num ;
    public float min ;
    public float max ;
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;
    public bool setSize;
   

    private void Awake()
    {              
        custom = FindObjectOfType<LobbyCharacterCustom>();
        if (FindObjectOfType<SaveLoad>())
            p = FindObjectOfType<SaveLoad>().player; 
      
    }
    private void Start()
    {
        num = 0;
        StyleCheck = "Scale";
        styleName.text = "Scale";
        min = 1f; max = 4f;
        player = custom.body.transform;
        ValueSet();
        sliderX.value = p.body_x;
        sliderY.value = p.body_y;
        sliderZ.value = p.body_z;      

    }

   
    public void Choice(string str)
    {
        StyleCheck = str;
        styleName.text = str;
        Set();
    }
    public void PartSelect(int _num)
    {
        num = _num;
        Set();    
    }
    void Set()
    {
        setSize = false;
        if (StyleCheck == "Rotation")
        {
            min = 0f; max = 180f;
            ValueSet();
        }
        else if (StyleCheck == "Scale")
        {
            min = 1f; max = 4f;
            ValueSet();
        }
    }
   
    void ValueSet()
    {
        sliderX.minValue = min; sliderX.maxValue = max;
        sliderY.minValue = min; sliderY.maxValue = max;
        sliderZ.minValue = min; sliderZ.maxValue = max; 
        if (num == 0)
        {
            player = custom.body.transform;
            fixedPartName.text = player.name;
        }
        else if (num == 1)
        {
            player = custom.glove1.transform;
            fixedPartName.text = player.name;
        }
        else if (num == 2)
        {
            player = custom.glove2.transform;
            fixedPartName.text = player.name;
        }
        else if (num == 3)
        {
            player = custom.head.transform;
            fixedPartName.text = player.name;
        }
        else if (num == 4)
        {
            player = custom.tail.transform;
            fixedPartName.text = player.name;
        }
        if (StyleCheck=="Rotation")
        {
            sliderX.value = player.localRotation.eulerAngles.x;
            sliderY.value = player.localRotation.eulerAngles.y;
            sliderZ.value = player.localRotation.eulerAngles.z;
        }
        else
        {
            sliderX.value = player.localScale.x;
            sliderY.value = player.localScale.y;
            sliderZ.value = player.localScale.z;
        }
        setSize = true;
    }
    public void SizeCustom()
    {
        if (setSize)
        {
            Vector3 slideSet = new Vector3(sliderX.value, sliderY.value, sliderZ.value);
            if (StyleCheck == "Rotation")
            {
            player.transform.localRotation = Quaternion.Euler(slideSet);
            }
            else if (StyleCheck == "Scale")
            {
            player.transform.localScale = slideSet;
            }
            UpdatePlayerData();
        }       
    }
    void UpdatePlayerData()
    {
        if (p == null) return;

        Vector3 scale = player.localScale;
        Vector3 rotation = player.localRotation.eulerAngles;

        switch (num)
        {
            case 0: // body
                p.body_x = scale.x;
                p.body_y = scale.y;
                p.body_z = scale.z;
                p.body_rotX = rotation.x;
                p.body_rotY = rotation.y;
                p.body_rotZ = rotation.z;
                break;
            case 1: // glove1
                p.glove1_x = scale.x;
                p.glove1_y = scale.y;
                p.glove1_z = scale.z;
                p.glove1_rotX = rotation.x;
                p.glove1_rotY = rotation.y;
                p.glove1_rotZ = rotation.z;
                break;
            case 2: // glove2
                p.glove2_x = scale.x;
                p.glove2_y = scale.y;
                p.glove2_z = scale.z;
                p.glove2_rotX = rotation.x;
                p.glove2_rotY = rotation.y;
                p.glove2_rotZ = rotation.z;
                break;
            case 3: // head
                p.head_x = scale.x;
                p.head_y = scale.y;
                p.head_z = scale.z;
                p.head_rotX = rotation.x;
                p.head_rotY = rotation.y;
                p.head_rotZ = rotation.z;
                break;
            case 4: // tail
                p.tail_x = scale.x;
                p.tail_y = scale.y;
                p.tail_z = scale.z;
                p.tail_rotX = rotation.x;
                p.tail_rotY = rotation.y;
                p.tail_rotZ = rotation.z;
                break;
            default:
                Debug.LogWarning("Unknown part selected: " + num);
                break;
        }
    }
}
