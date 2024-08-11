using Photon.Pun;
using UnityEngine;

public class Banana : UseItem
{
    public PhotonView p;
    protected override void Awake()
    {
        base.Awake();
    }
    new void Start()
    {
        
    }
 
    void OnTriggerEnter(Collider other)
    {
        if (pv.IsMine)
        {
            p = ctrl(other);
            if (p != null)
            {      
                p.GetComponent<PlayerCtrl>().rb.AddForce(p.transform.forward * 2000f);
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
            p.rb.AddForce(p.transform.forward * 2000f);
        }
    }


}
