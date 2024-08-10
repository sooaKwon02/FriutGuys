using Photon.Pun;
using System.Collections;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public Item item;
    public PhotonView pv;
    protected PhotonView player; // private로 수정

    protected virtual void Awake()
    {
        pv = GetComponent<PhotonView>(); 
        PlayerCtrl[] players = FindObjectsOfType<PlayerCtrl>();
        foreach (PlayerCtrl p in players)
        {
            if (p.GetComponent<PhotonView>().IsMine)
            {
                player = p.GetComponent<PhotonView>();
            }
        }// PhotonView 초기화
        ItemStyle(item.useitemType);
    }

    protected virtual void Start()
    {
        StartCoroutine(Des());
    }

    private void ItemStyle(Item.ITEM_STYLE item)
    {
        switch (item)
        {
            case Item.ITEM_STYLE.Buff:
                // Buff 타입 처리
                // 현재 PlayerCtrl을 찾는 로직은 Awake에서 처리되었으므로, 여기는 비워둘 수 있습니다.
                break;
            case Item.ITEM_STYLE.Debuff:
                // Debuff 타입 처리
                break;
            case Item.ITEM_STYLE.Trap:
                // Trap 타입 처리
                break;
            case Item.ITEM_STYLE.Bomb:
                // Bomb 타입 처리
                break;
        }
    }

    private IEnumerator Des()
    {
        yield return new WaitForSeconds(5f);
        // 게임 오브젝트가 여전히 존재하는지 확인 후 삭제
        if (gameObject != null&&pv.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public PhotonView ctrl(Collision col)
    {
        PhotonView photonView = col.transform.GetComponentInParent<PhotonView>() ?? col.transform.GetComponent<PhotonView>();

        if (photonView != null && !photonView.IsMine && photonView.GetComponent<PlayerCtrl>() != null)
        {
            return photonView;
        }

        return null;
    }
}
