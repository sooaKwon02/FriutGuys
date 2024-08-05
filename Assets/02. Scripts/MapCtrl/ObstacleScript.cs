using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float force = 300.0f;
    public float speed = 25f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = transform.forward.normalized;
            rb.AddForce(rb.position + dir * force);
        }
    }
    //자식오브젝트에 Rigidbody가 없고 Collider만 추가해서 사용할 경우,
    //부모와자식을 하나로 인식함
}
