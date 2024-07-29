using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    Vector3 respawnArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = respawnArea;
            //Destroy(other.gameObject);
        }
    }

   public IEnumerator RespawnInfo(Vector3 respawnInfo)
    {
        yield return new WaitForSeconds(0.5f);
        respawnArea = respawnInfo;
    }
}
