using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    private readonly float pushPower = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody rigidbody = collision.collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddForce((transform.forward + transform.up) * pushPower, ForceMode.Impulse);
            }
        }
    }
}