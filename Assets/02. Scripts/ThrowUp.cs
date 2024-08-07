using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowUp : MonoBehaviour
{
    [HideInInspector]
    public Item item;
    public MeshFilter mesh;

    public Transform throwDir;
    public void ItemSet(Item _item)
    {
        item = _item;
        mesh.sharedMesh = _item.mesh;   
    }
    public void Throw()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
           GameObject obj= PhotonNetwork.Instantiate("Prefabs/"+item.name, throwDir.position, Quaternion.identity);
            obj.transform.localScale = new Vector3(5f, 5f, 5f);
            if (obj.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb=obj.AddComponent<Rigidbody>();
                rb.AddForce(throwDir.forward*500);
            }
            mesh.sharedMesh = null;
            
        }

    }
}
