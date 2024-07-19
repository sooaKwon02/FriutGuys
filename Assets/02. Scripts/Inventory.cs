using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public GameObject useInventory;
    public GameObject fashionInventory;
    public GameObject inventory;

    //============================================================== 아이템 스왑
    [HideInInspector]
    public Item item;
    public GameObject target;
    public Image image;

  
    public void InventorySwap(bool check)
    {
        useInventory.SetActive(check);
        fashionInventory.SetActive(!check);

    }
    public void InventoryAdd()
    {
        GameObject inven = GameObject.FindGameObjectWithTag("Inventory");

        GameObject obj = Instantiate(inventory);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.SetParent(inven.GetComponent<RectTransform>(), false);
        obj.transform.tag = inven.name.ToString();
    }

    public void ItemSwap()
    {
              
    }
}
