using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : UseItem
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        PhotonView pv;
        if (collision.transform.GetComponentInParent<PhotonView>())
        {
            pv = collision.gameObject.GetComponentInParent<PhotonView>();
        }
        else if (collision.transform.GetComponent<PhotonView>())
        {
            pv = collision.gameObject.GetComponent<PhotonView>();
        }
        else
        {
            pv = null;
        }
        if (pv != null && !pv.IsMine && pv.CompareTag("Player"))
        {
            pv.GetComponent<PlayerCtrl>().DeBuffTime();
                pv.GetComponent<PlayerCtrl>().camHide.color = new Color(0, 0, 0, 1);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
