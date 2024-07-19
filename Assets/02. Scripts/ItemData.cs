using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public Item item;
    Image image;
    Button button;
    public bool isEmpty;
    ItemData itemdata;
    bool custom;
    


    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        if (GetComponentInParent<Custom>() == true)
        {
            custom = true;
        } 
        else
            custom = false;
        
    }
    private void Start()
    {
        button.onClick.AddListener(Onclick);
    }
    void Onclick()
    {
        if(item!=null)
        {
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            Inventory inventory = canvas.GetComponentInChildren<Inventory>();
            inventory.target.SetActive(true);
            ItemTG itemTG = canvas.GetComponentInChildren<ItemTG>();
            itemTG.ItemInfo(item);
            itemTG.itemSwap = gameObject;
            ItemGET(null);            
        }
       
    }
    public void ItemGET(Item _item)
    {
        item = _item;
        itemdata = GetComponent<ItemData>();
        ItemSet();     
        if(custom)
        {
            FindObjectOfType<CharacterCustom>().CharSet();
        }
    }
   
    void ItemSet()
    {
        if(item!=null)
        {
            isEmpty = true;
            image.sprite = item.sprite;
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            isEmpty = false;
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);
            if(custom)
            {
                FindObjectOfType<CharacterCustom>().CharSet();
            }
            
        }        
    }  
}
