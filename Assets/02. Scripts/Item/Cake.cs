using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : UseItem
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
        if (pvMine.Controller != pv.Controller && pv.CompareTag("Player"))
        {
            pv.GetComponent<PlayerCtrl>().DeBuffTime();
                pv.GetComponent<PlayerCtrl>().camHide.color = new Color(0, 0, 0, 1);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
