using Photon.Pun;
using System.Collections;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public Item item;
    public PhotonView pv;
    protected PhotonView player; // private�� ����

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
        }// PhotonView �ʱ�ȭ
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
                // Buff Ÿ�� ó��
                // ���� PlayerCtrl�� ã�� ������ Awake���� ó���Ǿ����Ƿ�, ����� ����� �� �ֽ��ϴ�.
                break;
            case Item.ITEM_STYLE.Debuff:
                // Debuff Ÿ�� ó��
                break;
            case Item.ITEM_STYLE.Trap:
                // Trap Ÿ�� ó��
                break;
            case Item.ITEM_STYLE.Bomb:
                // Bomb Ÿ�� ó��
                break;
        }
    }

    private IEnumerator Des()
    {
        yield return new WaitForSeconds(5f);
        // ���� ������Ʈ�� ������ �����ϴ��� Ȯ�� �� ����
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
