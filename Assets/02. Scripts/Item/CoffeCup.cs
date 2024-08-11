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
            // RPC 호출 시 player의 ViewID를 인자로 전달
            pv.RPC("SpeedUp", RpcTarget.All, player.ViewID);
            // 네트워크에서 객체 삭제
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void SpeedUp(int playerViewID)
    {
        // playerViewID로 PhotonView 찾기
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            // PhotonView로 PlayerCtrl 컴포넌트 가져오기
            PlayerCtrl playerCtrl = playerPhotonView.GetComponent<PlayerCtrl>();
            if (playerCtrl != null)
            {
                // BuffTime 코루틴 시작
                playerCtrl.BuffTime();
                // 이동 속도 두 배로 증가
                playerCtrl.moveSpeed *= 2;
            }
        }
    }
}
