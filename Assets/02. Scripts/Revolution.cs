using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour
{

    public float moveSpeed = 5f; // �̵� �ӵ�
    public float grabDistance = 2f; // ��� �Ÿ�
    private GameObject grabbedObject; // ���� ������Ʈ
    private SpringJoint springJoint; // ������ ����Ʈ
    private bool isGrabbing = false; // ��� ����

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.G)) // 'G' Ű�� ��� �õ�
        {
            TryGrab();
        }
        if (Input.GetKeyUp(KeyCode.G)) // 'G' Ű�� ������ ��� ����
        {
            ReleaseGrab();
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0, v) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }

    void TryGrab()
    {
        if (isGrabbing) return;

        // �������� Ray�� ��Ƽ� ��ȣ�ۿ� ������ ������Ʈ ����
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Player")) // �ٸ� �÷��̾� ����
            {
                grabbedObject = hit.collider.gameObject;
                springJoint = gameObject.AddComponent<SpringJoint>();
                springJoint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
                springJoint.spring = 5000f;
                springJoint.damper = 0f;
                springJoint.minDistance = 0f;
                springJoint.maxDistance = 0f;
                isGrabbing = true;

                // ��� �ִϸ��̼� Ʈ����
                Animator animator = GetComponent<Animator>();
                if (animator != null)
                {
                    //animator.SetTrigger("Grab");
                }
            }
        }
    }

    void ReleaseGrab()
    {
        if (!isGrabbing) return;

        if (springJoint != null)
        {
            Destroy(springJoint);
            springJoint = null;
            grabbedObject = null;
            isGrabbing = false;

            // ��� ���� �ִϸ��̼� Ʈ����
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                //animator.SetTrigger("Release");
            }
        }
    }

}
