using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Carrot : UseItem
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        if (pv.IsMine)
        {
            // RPC ȣ�� ��, player�� ViewID�� ����
            pv.RPC("JumpUp", RpcTarget.All, player.ViewID);
            // RPC ȣ�� ��, ��Ʈ��ũ���� ��ü ����
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void JumpUp(int playerViewID)
    {
        // playerViewID�� �÷��̾��� PhotonView�� ã��
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            // �÷��̾��� PlayerCtrl ������Ʈ�� ������
            PlayerCtrl playerCtrl = playerPhotonView.GetComponent<PlayerCtrl>();
            if (playerCtrl != null)
            {
                // �÷��̾��� BuffTime �ڷ�ƾ ����
                playerCtrl.BuffTime();
                // �������� �� ��� ����
                playerCtrl.jumpForce *= 2;
            }
        }
    }
}
