using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLineController : MonoBehaviour
{
    public Transform[] spawnPositions;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int index = Random.Range(0, spawnPositions.Length);
            other.transform.SetPositionAndRotation(spawnPositions[index].position, spawnPositions[index].rotation);
            other.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int index = Random.Range(0, spawnPositions.Length);
            other.transform.SetPositionAndRotation(spawnPositions[index].position, spawnPositions[index].rotation);
            other.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int index = Random.Range(0, spawnPositions.Length);
            other.transform.SetPositionAndRotation(spawnPositions[index].position, spawnPositions[index].rotation);
            other.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}