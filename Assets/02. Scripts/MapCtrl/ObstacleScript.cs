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
    //�ڽĿ�����Ʈ�� Rigidbody�� ���� Collider�� �߰��ؼ� ����� ���,
    //�θ���ڽ��� �ϳ��� �ν���
}
