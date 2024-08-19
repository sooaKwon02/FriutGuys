using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsBullet : MonoBehaviour
{
    private readonly float moveSpeed = 3.0f;

    private void Update()
    {
        float moveZ = moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, moveZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(UIController.instance.HitRoutine());
        }

        else if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}