using Photon.Pun;
using System.Collections;
using UnityEngine;

public class CoffeCup : UseItem
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
            pv.RPC("SpeedUp", RpcTarget.All, player.ViewID);
            // ��Ʈ��ũ���� ��ü ����
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void SpeedUp(int playerViewID)
    {
        // playerViewID�� PhotonView ã��
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            // PhotonView�� PlayerCtrl ������Ʈ ��������
            PlayerCtrl playerCtrl = playerPhotonView.GetComponent<PlayerCtrl>();
            if (playerCtrl != null)
            {
                playerCtrl.par[2].Play();
                // BuffTime �ڷ�ƾ ����
                playerCtrl.BuffTime(2);
                // �̵� �ӵ� �� ��� ����
                playerCtrl.moveSpeed *= 2;
            }
        }
    }
}
