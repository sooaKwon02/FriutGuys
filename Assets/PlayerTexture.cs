using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTexture : MonoBehaviour
{
    GameObject player;
    private void Awake()
    {
        player =GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().isKinematic = true;   
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            transform.position=player.transform.position+new Vector3(0,1,3);
            transform.LookAt(player.transform.position+new Vector3(0,1,0));
        }
    }
}
