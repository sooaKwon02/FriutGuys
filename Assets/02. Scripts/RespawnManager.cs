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
            Instantiate(other.gameObject, respawnArea.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    public void RespawnInfo(Transform respawnInfo)
    {
        respawnArea = respawnInfo;
    }
}
