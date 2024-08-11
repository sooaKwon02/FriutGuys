using Photon.Pun;
using UnityEngine;

public class Apple : UseItem
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (pv.IsMine)
        {
            PhotonView p = ctrl(other);
            if (p != null)
            {
                pv.RPC("DeBuffSpeed", RpcTarget.AllBuffered, p.ViewID);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
   

    [PunRPC]
    void DeBuffSpeed(int playerViewID)
    {
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            PlayerCtrl p = playerPhotonView.GetComponent<PlayerCtrl>();
            if (p != null)
            {
                p.DeBuffTime();
                p.moveSpeed=0f;
            }
        }
    }

}
