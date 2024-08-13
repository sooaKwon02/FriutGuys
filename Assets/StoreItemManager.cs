using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StoreItemManager : MonoBehaviour
{
    public Item[] items;
    public Item.MoneyType[] itemMoneys;

    public void StoreSet()
    {
        StoreItem[] stores = GetComponentsInChildren<StoreItem>();

        for (int i = 0; i < stores.Length; i++)
        {
            stores[i].item = items[i];
            stores[i].item.moneyType = itemMoneys[i];
            stores[i].ItemImageSet();
        }
    }
}
