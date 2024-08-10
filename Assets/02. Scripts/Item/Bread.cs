using Photon.Pun;
using UnityEngine;

public class Bread : UseItem
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (pv.IsMine)
        {
            PhotonView p = ctrl(collision);
            if (p != null)
            {
                pv.RPC("DeBuffJump", RpcTarget.AllBuffered, p.ViewID);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    void DeBuffJump(int playerViewID)
    {
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            PlayerCtrl p = playerPhotonView.GetComponent<PlayerCtrl>();
            if (p != null)
            {
                p.DeBuffTime();
                p.jumpForce = 0f; 
            }
        }
    }
}
