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
    public Sprite priceImg;

    public enum ItemType
    {
        UseItem,
        FashionItem
    }

   
}
