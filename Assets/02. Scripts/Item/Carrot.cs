using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : UseItem
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
            pv.GetComponent<PlayerCtrl>().BuffTime();
                pv.GetComponent<PlayerCtrl>().jumpForce*=2;
                pv.GetComponent<Rigidbody>().AddForce(Physics.gravity*2, ForceMode.Acceleration);
            PhotonNetwork.Destroy(gameObject);
        }

    }
}
