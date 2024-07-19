using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float speed = 25f;
    public float force = 300.0f;
    public Transform obstacleParent;
    private void Update()
    {
        obstacleParent.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            //local좌표계로는 앞방향이 y축임
            Vector3 dir = transform.up.normalized;
            rb.AddForce(rb.position + dir * force );
            collision.gameObject.GetComponentInChildren<Animator>().SetTrigger("Die");
        }
    }
}
