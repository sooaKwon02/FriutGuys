using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    Transform respawnArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = respawnArea.position;
            //Destroy(other.gameObject);
        }
    }

    public void RespawnInfo(Transform respawnInfo)
    {
        respawnArea = respawnInfo;
    }
}
