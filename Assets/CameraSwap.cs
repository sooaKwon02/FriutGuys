using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    private void Awake()
    {
        PhotonView pv=GetComponentInParent<PhotonView>();
        if(pv.IsMine)
        {
           GameObject obj= GameObject.FindGameObjectWithTag("MainCamera");
            obj.transform.SetParent(transform);
            obj.transform.localPosition = new Vector3(0, -0.71f, -6.15f);
        }
    }   
}
