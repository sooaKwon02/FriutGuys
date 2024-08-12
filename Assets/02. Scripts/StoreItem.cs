using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static SaveLoad;

public class StoreItem : MonoBehaviour
{
    public Item item;
    //===============================================
    public Image itemImg;
    public Image priceImg;
    public Text priceText;
        
   
    private void Start()
    {
        ItemImageSet();
    }
    public void ItemImageSet()
    {
        itemImg.sprite = item.sprite;
        priceImg.sprite = item.priceImg;
        priceText.text = item.price.ToString();
    }
    public void GetButton()
    {
        SaveLoad.PLAYER player = FindObjectOfType<SaveLoad>().player;
        bool isCash = item.moneyType == Item.MoneyType.Cash;
        bool isGameMoney = item.moneyType == Item.MoneyType.GameMoney;

        if ((isCash && player.cashMoney >= item.price) || (isGameMoney && player.gameMoney >= item.price))
        {
            if (isCash) player.cashMoney -= item.price;
            if (isGameMoney) player.gameMoney -= item.price;

            FindObjectOfType<GameManager>().IDPanelSet();
            GameObject[] inven = GameObject.FindGameObjectsWithTag(item.itemType.ToString());
            GetItems(inven);
        }
        else
        {
           StartCoroutine(FindObjectOfType<GameManager>().ErrorSend("머니가 부족합니다"));
        }
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
