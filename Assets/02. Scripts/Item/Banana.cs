using Photon.Pun;
using UnityEngine;

public class Banana : UseItem
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (pv.IsMine)
        {
            PhotonView p = ctrl(collision);
            if (p != null)
            {
                pv.RPC("DeBuffSlide", RpcTarget.AllBuffered, p.ViewID);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    void DeBuffSlide(int playerViewID)
    {
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            PlayerCtrl p = playerPhotonView.GetComponent<PlayerCtrl>();
            p.rb.AddForce(p.rb.velocity * 1000f);
        }
    }


}
