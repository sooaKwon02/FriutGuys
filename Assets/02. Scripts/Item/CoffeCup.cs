using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeCup : UseItem
{
    PhotonView pvMine;
    private void Awake()
    {
        pvMine = GetComponent<PhotonView>();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        PhotonView pv = PV(collision);
        if (pvMine.Controller==pv.Controller&& pv.CompareTag("Player"))
        {
            pv.GetComponent<PlayerCtrl>().BuffTime();
                pv.GetComponent<PlayerCtrl>().moveSpeed *= 2;
                pv.GetComponent<PlayerCtrl>().slideSpeed *= 2;
            PhotonNetwork.Destroy(gameObject);

        }
    }
}
