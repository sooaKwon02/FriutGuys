using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    
    public Item item;
    //===============================================
    public Image itemImg;
    public Image priceImg;
    public Text price;
        
   
    private void Start()
    {
        ItemImageSet();
    }
    void ItemImageSet()
    {
        itemImg.sprite = item.sprite;
        priceImg.sprite = item.priceImg;
        price.text = item.price.ToString();
    }   
    public void GetButton()
    {
        GameObject[] inven = GameObject.FindGameObjectsWithTag(item.itemType.ToString());
        GetItems(inven);
    }
    void GetItems(GameObject[] inven)
    {
        foreach(GameObject obj in inven)
        {
            ItemData _item =obj.GetComponentInChildren<ItemData>();
            if(!_item.isEmpty)
            {
                _item.ItemGET(item);
                break;
            }            
        }
    }
    
   

}
