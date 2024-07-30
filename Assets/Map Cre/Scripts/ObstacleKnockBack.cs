using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleKnockBack : MonoBehaviour
{
    private readonly float bounceForce = 2.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounce = (collision.transform.position - transform.position).normalized;
            bounce.y = 0;
            rigidbody.AddForce(bounce * bounceForce, ForceMode.Impulse);
        }
    }
}