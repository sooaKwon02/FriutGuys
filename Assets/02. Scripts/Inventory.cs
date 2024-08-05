using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

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

    SaveLoad saveload;
    
    private void Awake()
    {
        saveload = FindObjectOfType<SaveLoad>();
        saveload.InventorySet();
    }
    public void InventorySwap(bool check)
    {
        useInventory.SetActive(check);
        fashionInventory.SetActive(!check);
    }
    public void InventoryAdd()
    {
        GameObject inven = GameObject.FindGameObjectWithTag("Inventory");
        GameObject obj = Instantiate(inventory);
        obj.transform.SetParent(inven.GetComponent<RectTransform>(), false);
        obj.tag = inven.name.ToString();         
    }
   
   
    
}
