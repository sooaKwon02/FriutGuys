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
 
    void OnTriggerEnter(Collider other)
    {
        if (pv.IsMine)
        {
            PhotonView p = ctrl(other);
            if (p != null)
            {      
                p.GetComponent<PlayerCtrl>().rb.AddForce(p.GetComponent<PlayerCtrl>().rb.velocity * 1000f);
                pv.RPC("DeBuffSlide", RpcTarget.OthersBuffered, p.ViewID);
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
