using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public RespawnManager RespawnManager;
    float rand;
    private void Update()
    {
        rand = Random.Range(-10.0f, 10.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          StartCoroutine(RespawnManager.RespawnInfo(transform));
        }
    }
}
