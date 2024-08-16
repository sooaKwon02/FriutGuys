using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Death Ball"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}