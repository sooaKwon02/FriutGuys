using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    [SerializeField]
    Item item;
    //===============================================
    public Image itemImg;
    public Image priceImg;
    public Text priceText;
        
   
    private void Start()
    {
        ItemImageSet();
    }
    void ItemImageSet()
    {
        itemImg.sprite = item.sprite;
        priceImg.sprite = item.priceImg;
        priceText.text = item.price.ToString();
    }   
    public void GetButton()
    {
        if(item.moneyType==Item.MoneyType.Cash)
        {
            int money = FindObjectOfType<SaveLoad>().player.cashMoney;
            BuyTry(money);
        }
        else if(item.moneyType==Item.MoneyType.GameMoney)
        {
            int money=FindObjectOfType<SaveLoad>().player.gameMoney;
            BuyTry(money);           
        }        
    }
    void BuyTry(int money)
    {
        if (item.price >= money)
        {
            money -= item.price;
            FindObjectOfType<GameManager>().IDPanelSet();
            GameObject[] inven = GameObject.FindGameObjectsWithTag(item.itemType.ToString());
            GetItems(inven);
        }
        else
        {
            FindObjectOfType<GameManager>().ErrorSend("머니가 부족합니다");
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
