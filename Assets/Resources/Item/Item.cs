using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }


}
