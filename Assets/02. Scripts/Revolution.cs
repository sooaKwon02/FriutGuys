using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour
{

    public float moveSpeed = 5f; // 이동 속도
    public float grabDistance = 2f; // 잡기 거리
    private GameObject grabbedObject; // 잡은 오브젝트
    private SpringJoint springJoint; // 스프링 조인트
    private bool isGrabbing = false; // 잡기 상태

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.G)) // 'G' 키로 잡기 시도
        {
            TryGrab();
        }
        if (Input.GetKeyUp(KeyCode.G)) // 'G' 키를 놓으면 잡기 해제
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

        // 앞쪽으로 Ray를 쏘아서 상호작용 가능한 오브젝트 감지
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Player")) // 다른 플레이어 감지
            {
                grabbedObject = hit.collider.gameObject;
                springJoint = gameObject.AddComponent<SpringJoint>();
                springJoint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
                springJoint.spring = 5000f;
                springJoint.damper = 0f;
                springJoint.minDistance = 0f;
                springJoint.maxDistance = 0f;
                isGrabbing = true;

                // 잡기 애니메이션 트리거
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

            // 잡기 해제 애니메이션 트리거
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                //animator.SetTrigger("Release");
            }
        }
    }

}
