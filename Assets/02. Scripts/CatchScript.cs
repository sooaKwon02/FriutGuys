using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;
    Animator anim;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInParent<Animator>();
    }

    //void Update()
    //{
    //    // Ű�� ������ �� ��ü�� �ڽ����� �����
    //    if (Input.GetKeyDown(KeyCode.LeftControl))
    //    {
    //        if (target != null)
    //        {
    //            target.transform.SetParent(transform);
    //            anim.SetBool("isCatch", true);
    //        }
    //        else
    //        {
    //            //��ü�� null�̸� ����� �ִϸ��̼�
    //            anim.SetTrigger("NonCatch");
    //        }
    //    }

    //    // Ű�� ���� �� �ڽĿ��� ����
    //    if (Input.GetKeyUp(KeyCode.LeftControl))
    //    {
    //        if(target != null)
    //            target.transform.SetParent(null);
    //        anim.SetTrigger("Catch");
    //        anim.SetBool("isCatch", false);
    //    }
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other != null)//���� �ʿ��� tag�� ���濹��
    //    {
    //        target = other.gameObject;
    //    }
    //}

    ////�ƹ����� Ű ������ ���� ������Ʈ ��ü�� �÷��̾ �����
    ////null������ �������� ��.
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject == target)
    //    {
    //        target = null;
    //    }
    //}
    void Update()
    {
        //    if (Input.GetKeyDown(KeyCode.LeftShift))
        //    {
        //        if (target != null)
        //        {
        //            Debug.Log("!!!!!");
        //            Rigidbody rb = target.GetComponent<Rigidbody>();
        //            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY
        //| RigidbodyConstraints.FreezePositionZ;

        //        }
        //    }

        //    if (Input.GetKeyUp(KeyCode.LeftShift))
        //    {
        //        Rigidbody rb = target.GetComponent<Rigidbody>();
        //        rb.constraints = RigidbodyConstraints.None;
        //    }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(target != null)
            {
                Debug.Log("@@@");
                FixedJoint fj = target.GetComponent<FixedJoint>();
                Rigidbody targetRb = target.GetComponent<Rigidbody>();
                targetRb.isKinematic = true;
                fj.connectedBody = rb;

                Debug.Log(fj.connectedBody);
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (target != null)
            {
                Rigidbody targetRb = target.GetComponent<Rigidbody>();
                FixedJoint fj = target.GetComponent<FixedJoint>();
                fj.connectedBody = null;
                targetRb.isKinematic = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log(other.name);
            target = other.gameObject;
        }
    }

    //�ƹ����� Ű ������ ���� ������Ʈ ��ü�� �÷��̾ �����
    //null������ �������� ��.
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
        }
    }
}
