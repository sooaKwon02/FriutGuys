using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTG : MonoBehaviour
{
   
    public Item item;
    Image image;

    [HideInInspector]
    public GameObject itemSwap;
    
   
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
            
        transform.position = Input.mousePosition;      
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf)
        {         
            Vector2 clickPos = transform.position;
            ItemGive(clickPos);
        }
    }
    public void ItemInfo(Item _item)
    {
        item = _item;
        ItemSet();
    }
    void ItemSet()
    {
        if (item != null)
        {            
            image.sprite = item.sprite;
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {            
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);
        }
    }
    void ItemGive(Vector2 pos)
    {
        GameObject[] inventory = GameObject.FindGameObjectsWithTag(item.itemType.ToString());
        foreach(GameObject obj in inventory)
        {
            float objPos = Pos(obj.transform.position,pos);   
            if (objPos<60)
            {
                itemSwap.GetComponent<ItemData>().ItemGET(obj.GetComponentInChildren<ItemData>().item);
                obj.GetComponentInChildren<ItemData>().ItemGET(item);
                ItemInfo(null);
                gameObject.SetActive(false);
            }            
        }
    }   
    float Pos(Vector2 obj,Vector2 pos)
    {      

        Vector2 vec = obj - pos;
        return vec.magnitude;        
    }

}
