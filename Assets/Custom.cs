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
    public GameObject pos;
    public GameObject scale;
    private void Start()
    {
        pos.SetActive(true);
        scale.SetActive(false);
        sliderX.minValue = min;
        sliderX.maxValue = max;
        sliderY.minValue = min;
        sliderY.maxValue = max;
        sliderZ.minValue = min;
        sliderZ.maxValue = max;
    }
    public void PlayerSet(Item _item)
    {        
        item = _item;
        player.CharSet();
    }
    private void Awake()
    {
        player = FindObjectOfType<CharacterCustom>();
    }
    public void Choice(string str)
    {
        StyleCheck = str;
        styleName.text = str;
        if (str == "Scale")
        {
            pos.SetActive(false);
            scale.SetActive(true);
        }
        else
        {
            pos.SetActive(true);
            scale.SetActive(false);
        }
    }
    public void ScaleSet()
    {
        player.bodyParts[num].transform.localScale =new Vector3(sliderX.value, sliderY.value, sliderZ.value) ;
    }
    public void StyleLeft(string str)
    {
        if (StyleCheck == "Position")
        {
            if (str == KEYGET.UP.ToString())
            {
                if (num == 2 || num == 1)
                {
                    player.bodyParts[1].transform.Translate(Vector2.up * 0.01f, Space.Self);
                    player.bodyParts[2].transform.Translate(Vector2.up * 0.01f, Space.Self);
                }
                else
                    player.bodyParts[num].transform.Translate(Vector2.up * 0.01f, Space.Self);
            }
            else if (str == KEYGET.DOWN.ToString())
            {
                if (num == 2 || num == 1)
                {
                    player.bodyParts[1].transform.Translate(Vector2.down * 0.01f, Space.Self);
                    player.bodyParts[2].transform.Translate(Vector2.down * 0.01f, Space.Self);
                }
                else
                    player.bodyParts[num].transform.Translate(Vector2.down * 0.01f, Space.Self);
            }
            else if (str == KEYGET.LEFT.ToString())
            {
                if (num == 2 || num == 1)
                {
                    player.bodyParts[1].transform.Translate(Vector2.left * 0.01f, Space.Self);
                    player.bodyParts[2].transform.Translate(Vector2.left * -0.01f, Space.Self);
                }
                else
                    player.bodyParts[num].transform.Translate(Vector2.left * 0.01f, Space.Self);
            }
            else if (str == KEYGET.RIGHT.ToString())
            {
                if (num == 2 || num == 1)
                {
                    player.bodyParts[1].transform.Translate(Vector2.right * 0.01f, Space.Self);
                    player.bodyParts[2].transform.Translate(Vector2.right * -0.01f, Space.Self);
                }
                else
                    player.bodyParts[num].transform.Translate(Vector2.right * 0.01f, Space.Self);
            }
        }
        else if (StyleCheck == "Rotation")
        {
            if (str == KEYGET.UP.ToString())
            {
                if (num == 2 || num == 1)
                {
                    player.bodyParts[1].transform.Rotate(Vector2.up * 1f, Space.Self);
                    player.bodyParts[2].transform.Rotate(Vector2.up * 1f, Space.Self);
                }
                else
                    player.bodyParts[num].transform.Rotate(Vector2.up * 1f, Space.Self);
            }
            else if (str == KEYGET.DOWN.ToString())
            {
                if (num == 2 || num == 1)
                {
                    player.bodyParts[1].transform.Rotate(Vector2.down * 1f, Space.Self);
                    player.bodyParts[2].transform.Rotate(Vector2.down * 1f, Space.Self);
                }
                else
                    player.bodyParts[num].transform.Rotate(Vector2.down * 1f, Space.Self);
            }
            else if (str == KEYGET.LEFT.ToString())
            {
                if (num == 2 || num == 1)
                {
                    player.bodyParts[1].transform.Rotate(Vector2.left * 1f, Space.Self);
                    player.bodyParts[2].transform.Rotate(Vector2.left * -1f, Space.Self);
                }
                else
                    player.bodyParts[num].transform.Rotate(Vector2.left * 1f, Space.Self);
            }
            else if (str == KEYGET.RIGHT.ToString())
            {
                if (num == 2 || num == 1)
                {
                    player.bodyParts[1].transform.Rotate(Vector2.right * 1f, Space.Self);
                    player.bodyParts[2].transform.Rotate(Vector2.right * -1f, Space.Self);
                }
                else
                    player.bodyParts[num].transform.Rotate(Vector2.right * 1f, Space.Self);
            }
        }         
    } 
    public void PartSelect(int _num)
    {
        num = _num;
        FixedPartName.text = player.bodyParts[num].name;
        sliderX.value = player.bodyParts[num].transform.localScale.x;
        sliderY.value = player.bodyParts[num].transform.localScale.y;
        sliderZ.value = player.bodyParts[num].transform.localScale.z;
    }
}
