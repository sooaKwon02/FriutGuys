using Photon.Pun;
using UnityEngine;

public class Cake : UseItem
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
                pv.RPC("DeBuffHide", RpcTarget.AllBuffered, p.ViewID);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
 
    [PunRPC]
    void DeBuffHide(int playerViewID)
    {
        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView != null)
        {
            PlayerCtrl p = playerPhotonView.GetComponent<PlayerCtrl>();
            if (p != null)
            {
                p.par[3].Play();
                p.DeBuffTime(3);
                p.camHide.color = new Color(0, 0, 0, 1);
            }
        }
    }
}
