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
        saveload.inventory = GetComponent<Inventory>();
    }
    private void Start()
    {
        InventorySet(saveload.useNum, saveload.useName, saveload.fashionNum, saveload.fashionName);
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
    void InventorySet(int[] _useNum, string[] _useName, int[] _fashionNum, string[] _fashionName)
    {
        for(int i=0;i<_useNum.Length;i++)
        {
            GameObject obj = Instantiate(inventory);
            obj.transform.SetParent(useInventory.GetComponent<RectTransform>(), false);
            Item item = Resources.Load<Item>("Item/UseItem/" + _useName[i]);
            obj.GetComponentInChildren<ItemData>().ItemGET(item);
            obj.tag = useInventory.name.ToString();
        }
        for (int i = 0; i < _fashionNum.Length; i++)
        {
            GameObject obj = Instantiate(inventory);
            obj.transform.SetParent(fashionInventory.GetComponent<RectTransform>(), false);
            Item item = Resources.Load<Item>("Item/FashionItem/" + _fashionName[i]);
            obj.GetComponentInChildren<ItemData>().ItemGET(item);
            obj.tag = fashionInventory.name.ToString();
        }
    }
}
