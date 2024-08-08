using Photon.Pun;
using System.Collections;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public Item item;

    protected virtual void Start()
    {
        StartCoroutine(Des());
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        ItemStyle(item.useitemType);
    }


    void ItemStyle(Item.ITEM_STYLE item)
    {
        switch (item)
        {
            case Item.ITEM_STYLE.Buff:
                break;
            case Item.ITEM_STYLE.Debuff:          
                break;
            case Item.ITEM_STYLE.Trap:
                break;
            case Item.ITEM_STYLE.Bomb:
                break;
        }
    }
    IEnumerator Des()
    {
        yield return new WaitForSeconds(5f);
        PhotonNetwork.Destroy(gameObject);
    }
}
