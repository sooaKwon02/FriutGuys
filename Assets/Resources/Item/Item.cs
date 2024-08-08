using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName ="Item",menuName ="item")]
public class Item : ScriptableObject
{
    public string _name;
    public Sprite sprite;
    public Mesh mesh;
    public GameObject prefab;
    public ItemType itemType;
    public int price;
    public int gameMoneyPrice;
    public int cashPrice;
    public Sprite priceImg;
    public Sprite cashImg;
    public Sprite gameMoneyImg;
    public MoneyType moneyType;
    public ITEM_STYLE useitemType;
    public float speed;
    public enum ITEM_STYLE
    {
        Buff,
        Debuff,
        Trap,
        Bomb
    }
    public enum ItemType
    {
        UseItem,
        FashionItem
    }
    public enum MoneyType
    {
        Cash,
        GameMoney
    }
    private void Awake()
    {
        if(moneyType ==MoneyType.Cash)
        {
            price=cashPrice;
            priceImg = cashImg;
        }
        else if (moneyType == MoneyType.GameMoney)
        {
            price = gameMoneyPrice;
            priceImg = gameMoneyImg;
        }
        if (useitemType == ITEM_STYLE.Buff)
        {
            speed = 0;
        }
        else if (useitemType == ITEM_STYLE.Debuff)
        {
            speed = 1000;
        }
        else if (useitemType == ITEM_STYLE.Trap)
        {
            speed = 200;
        }
        else if (useitemType == ITEM_STYLE.Bomb)
        {
            speed = 700;
        }
    }

  
}
