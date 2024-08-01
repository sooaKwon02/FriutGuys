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
        ItemSet(); 
        if (custom)
            GameObject.FindGameObjectWithTag(inven.ToString()).GetComponent<PlayerItem>().ItemSet(item);
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
    void CustomSet()
    {
        if(custom)
        {
            if (inven == InvenSwap.BODY) {
                item = (Resources.Load<Item>("Item/FashionItem/" + p.body_name)); ItemSet();
            }
            else if (inven == InvenSwap.GLOVEL) {
                item = (Resources.Load<Item>("Item/FashionItem/" + p.glove1_name)); ItemSet();
            }
            else if (inven == InvenSwap.GLOVER) {
                item = (Resources.Load<Item>("Item/FashionItem/" + p.glove2_name)); ItemSet();
            }
            else if (inven == InvenSwap.HEAD) {
                item = (Resources.Load<Item>("Item/FashionItem/" + p.head_name)); ItemSet();
            }
            else if (inven == InvenSwap.TAIL) {
                item = (Resources.Load<Item>("Item/FashionItem/" + p.tail_name)); ItemSet();
            }
            else if (inven == InvenSwap.USEITEM1) {
                item = (Resources.Load<Item>("Item/UseItem/" + p.item1)); ItemSet();
            }
            else if (inven == InvenSwap.USEITEM2) {
                item = (Resources.Load<Item>("Item/UseItem/" + p.item2)); ItemSet();
            }
        }
            }
            }
