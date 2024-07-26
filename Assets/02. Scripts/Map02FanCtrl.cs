using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map02FanCtrl : MonoBehaviour
{
    float flyForce;
    public Transform target;

    public float rotSpeed = 10f;

    private void Update()
    {
        transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        flyForce = Random.Range(10.0f, 15.0f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = other.transform.position - transform.position;
            direction.y = 0; 
            direction.Normalize();

            other.GetComponent<Rigidbody>().AddForce(direction * flyForce);
        }
    }
}
