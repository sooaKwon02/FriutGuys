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
    public bool custom;
    public enum InvenSwap
    {
        Null,
        BODY,
        GLOVEL,
        GLOVER,
        HEAD,
        TAIL,
        USEITEM1,
        USEITEM2
    }
    public InvenSwap inven;

    SaveLoad.PLAYER p;
    private void Awake()
    {
        if(FindObjectOfType<SaveLoad>())
        p = FindObjectOfType<SaveLoad>().player;
        image = GetComponent<Image>();
        button = GetComponent<Button>();      
    }
    private void Start()
    {
        if(custom)
        {
            CustomSet();
        }
        button.onClick.AddListener(Onclick);
    }
    void Onclick()
    {
        if(item!=null)
        {
            
            Inventory inventory = FindObjectOfType<GameManager>().GetComponent<Inventory>();
            inventory.target.SetActive(true);
            ItemTG itemTG = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<ItemTG>();
            itemTG.ItemInfo(item);
            itemTG.itemSwap = gameObject;
            ItemGET(null);            
        }
       
    }
    public void ItemGET(Item _item)
    {
        item = _item;         
        ItemSet(); 
        if (custom)
        {
            GameObject.FindGameObjectWithTag(inven.ToString()).GetComponent<PlayerItem>().ItemSet(item);
            SaveSet();
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

        }        
    }
    void SaveSet()
    {
        if (custom)
        {
            if (inven == InvenSwap.BODY && item != null)
            {
                { p.body_name = item.name.ToString(); }
            }
            else if (inven == InvenSwap.GLOVEL && item != null)
            {
                p.glove1_name = item.name.ToString();
            }
            else if (inven == InvenSwap.GLOVER && item != null)
            {
                p.glove2_name = item.name.ToString();
            }
            else if (inven == InvenSwap.HEAD && item != null)
            {
                p.head_name = item.name.ToString();
            }
            else if (inven == InvenSwap.TAIL && item != null)
            {
                p.tail_name = item.name.ToString();
            }
            else if (inven == InvenSwap.USEITEM1 && item != null)
            {
                p.item1 = item.name.ToString();
            }
            else if (inven == InvenSwap.USEITEM2 && item != null)
            {
                p.item2 = item.name.ToString();
            }
        }
    }
        void CustomSet()
        {
            if (custom)
            {
                if (inven == InvenSwap.BODY)
                {
                    item = (Resources.Load<Item>("Item/FashionItem/" + p.body_name)); ItemSet();
                }
                else if (inven == InvenSwap.GLOVEL)
                {
                    item = (Resources.Load<Item>("Item/FashionItem/" + p.glove1_name)); ItemSet();
                }
                else if (inven == InvenSwap.GLOVER)
                {
                    item = (Resources.Load<Item>("Item/FashionItem/" + p.glove2_name)); ItemSet();
                }
                else if (inven == InvenSwap.HEAD)
                {
                    item = (Resources.Load<Item>("Item/FashionItem/" + p.head_name)); ItemSet();
                }
                else if (inven == InvenSwap.TAIL)
                {
                    item = (Resources.Load<Item>("Item/FashionItem/" + p.tail_name)); ItemSet();
                }
                else if (inven == InvenSwap.USEITEM1)
                {
                    item = (Resources.Load<Item>("Item/UseItem/" + p.item1)); ItemSet();
                }
                else if (inven == InvenSwap.USEITEM2)
                {
                    item = (Resources.Load<Item>("Item/UseItem/" + p.item2)); ItemSet();
                }
            }
        }
    }
          

            
