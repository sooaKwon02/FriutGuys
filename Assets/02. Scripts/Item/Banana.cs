using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : UseItem
{
    PhotonView pvMine;
    private void Start()
    {
        
    }
    private void Awake()
    {
        pvMine = GetComponent<PhotonView>();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        PhotonView pv=PV(collision);

        if ( pv!=null&&pvMine.Controller != pv.Controller && pv.CompareTag("Player"))
        {
            pv.GetComponent<Rigidbody>().AddForce(pv.GetComponent<Rigidbody>().velocity * 2);
        }
    }
    
}
