using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public RespawnManager RespawnManager;
    float rand;
    Vector3 pos;
    private void Update()
    {
        rand = Random.Range(-10.0f, 10.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pos = new Vector3(transform.position.x + rand, transform.position.y, transform.position.z);
            other.GetComponent<PlayerCtrl>().point=pos;
        }
    }
}
