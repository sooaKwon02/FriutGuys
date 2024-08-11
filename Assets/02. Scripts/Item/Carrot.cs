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
            // RPC 호출 시, player의 ViewID만 전송
            pv.RPC("JumpUp", RpcTarget.All, player.ViewID);
            // RPC 호출 후, 네트워크에서 객체 삭제
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void JumpUp(int playerViewID)
    {
        // playerViewID로 플레이어의 PhotonView를 찾음
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            // 플레이어의 PlayerCtrl 컴포넌트를 가져옴
            PlayerCtrl playerCtrl = playerPhotonView.GetComponent<PlayerCtrl>();
            if (playerCtrl != null)
            {
                // 플레이어의 BuffTime 코루틴 시작
                playerCtrl.BuffTime();
                // 점프력을 두 배로 증가
                playerCtrl.jumpForce *= 2;
            }
        }
    }
}
