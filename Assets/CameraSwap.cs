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
            gameObject.AddComponent<AudioListener>();
        }
    }   
}
