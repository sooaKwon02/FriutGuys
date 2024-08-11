using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Cookie : UseItem
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        if (pv.IsMine)
        {
            // RPC ȣ�� �� player�� ViewID�� ���ڷ� ����
            pv.RPC("PowerUp", RpcTarget.All, player.ViewID);
            // ��Ʈ��ũ���� ��ü ����
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void PowerUp(int playerViewID)
    {
        // playerViewID�� PhotonView ã��
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            // PhotonView�� PlayerCtrl ������Ʈ ��������
            PlayerCtrl playerCtrl = playerPhotonView.GetComponent<PlayerCtrl>();
            if (playerCtrl != null)
            {
                // BuffTime �ڷ�ƾ ����
                playerCtrl.BuffTime();
                // �÷��̾��� ��Ű ���¸� Ȱ��ȭ
                playerCtrl.cookie = true;
            }
        }
    }
}
