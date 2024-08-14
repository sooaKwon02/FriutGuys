using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject useInventoryPanel;
    public GameObject fashionInventoryPanel;
    public GameObject inventory;
    public RectTransform useItem;
    public RectTransform fashionItem;

    //============================================================== 아이템 스왑
    [HideInInspector]
    public Item item;
    public GameObject target;
    public Image image;


    public GameObject InvenAddPanel;
    private void OnEnable()
    {
        if(SaveLoad.instance.inventory==null)
        SaveLoad.instance.inventory = this;
    }
    private void Awake()
    {
        SaveLoad.instance.inventory = this;
        useItem = useInventoryPanel.transform.GetChild(0).GetComponent<RectTransform>();
        fashionItem = fashionInventoryPanel.transform.GetChild(0).GetComponent<RectTransform>();
    }
    private void Start()
    {
        useInventoryPanel.SetActive(false); 
        fashionInventoryPanel.SetActive(true);
        InvenAddPanel.SetActive(false);
        InventorySet(SaveLoad.instance.useNum, SaveLoad.instance.useName, SaveLoad.instance.fashionNum, SaveLoad.instance.fashionName);
    }
    public void InventorySwap(bool check)
    {
        useInventoryPanel.SetActive(check);
        fashionInventoryPanel.SetActive(!check);
    }
    public void InventoryAdd(int num)
    {
        if (num == 0&& SaveLoad.instance.player.cashMoney>=100)
        {
            SaveLoad.instance.player.cashMoney -= 100;
            GetComponent<GameManager>().IDPanelSet();
            InstanceAdd();
        }
        else if(num==1&& SaveLoad.instance.player.gameMoney>=1000)
        {
            SaveLoad.instance.player.gameMoney -= 1000;
            GetComponent<GameManager>().IDPanelSet();
            InstanceAdd();
        }
        else
        {
            StartCoroutine(GetComponent<GameManager>().ErrorSend("머니가 부족합니다"));
        }
    }
    void InstanceAdd()
    {
        if (fashionInventoryPanel.activeSelf)
        {
            GameObject obj = Instantiate(inventory);
            obj.transform.SetParent(fashionItem.transform, false);
            obj.tag = fashionItem.name;
            Adds(fashionItem);
        }
        else if (useInventoryPanel.activeSelf)
        {
            GameObject obj = Instantiate(inventory);
            obj.transform.SetParent(useItem.transform, false);
            obj.tag = useItem.name;
            Adds(useItem);
        }
    }
    void Adds(RectTransform inven)
    {
        if (inven.childCount != 0)
        {
            Vector2 cellSize = inven.GetComponent<GridLayoutGroup>().cellSize;
            int row = (inven.childCount / 5) + 1;            
            inven.pivot = new Vector2(0f, 1f);
            inven.sizeDelta = new Vector2(cellSize.x*4.4f, cellSize.y*row*1.2f);
        }
        else
        {
            inven.sizeDelta=Vector2.zero;
        }
    }
    public void InvenAddSet(bool check)
    {
        InvenAddPanel.SetActive(check);
    }
    void InventorySet(int[] _useNum, string[] _useName, int[] _fashionNum, string[] _fashionName)
    {
        for(int i=0;i<_useNum.Length;i++)
        {
            GameObject obj = Instantiate(inventory);
            obj.transform.SetParent(useItem, false);
            Item item = Resources.Load<Item>("Item/UseItem/" + _useName[i]);
            obj.GetComponentInChildren<ItemData>().ItemGET(item);
            obj.tag = useItem.name.ToString();
        }
        for (int i = 0; i < _fashionNum.Length; i++)
        {
            GameObject obj = Instantiate(inventory);
            obj.transform.SetParent(fashionItem, false);
            obj.tag = fashionItem.name.ToString();
            Item item = Resources.Load<Item>("Item/FashionItem/" + _fashionName[i]);
            obj.GetComponentInChildren<ItemData>().ItemGET(item);
        }
        Adds(useItem);
        Adds(fashionItem);
    }
}
