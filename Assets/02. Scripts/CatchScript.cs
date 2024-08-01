using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    public float grabDistance = 2.0f;
    private GameObject grabTarget;
    //private SpringJoint springJoint;
    private FixedJoint fixedJoint;
    private bool isGrab = false;
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
    //void Update()
    //{

    //    Grab();
    //    GrabEnd();
    //}

    //void Grab()
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        if (isGrab) return;

    //        //if (Physics.Raycast(transform.position, player.transform.forward, out RaycastHit hit, grabDistance))
    //        //{
    //        //if (hit.collider.CompareTag("Player"))
    //        //{
    //        //Debug.Log(hit.collider.gameObject.name);
    //        //grabTarget = hit.collider.gameObject;

    //        //springJoint = gameObject.AddComponent<SpringJoint>();
    //        //springJoint.connectedBody = grabTarget.GetComponent<Rigidbody>();
    //        //springJoint.spring = 1000.0f;
    //        //springJoint.damper = 0.0f;
    //        //springJoint.minDistance = 0.0f;
    //        //springJoint.maxDistance = 0.1f;
    //        if (grabTarget != null)
    //        {
    //            fixedJoint = transform.parent.parent.gameObject.AddComponent<FixedJoint>();
    //            fixedJoint.connectedBody = grabTarget.GetComponent<Rigidbody>();
    //            isGrab = true;
    //        }


    //        //}
    //        //}
    //    }
    //}
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        grabTarget = other.gameObject;
    //        Debug.Log(grabTarget);
    //    }
    //}

    ////�ƹ����� Ű ������ ���� ������Ʈ ��ü�� �÷��̾ �����
    ////null������ �������� ��.
    ////void OnTriggerExit(Collider other)
    ////{
    ////    if (other.gameObject == grabTarget)
    ////    {
    ////        target = null;
    ////    }
    ////}
    //void GrabEnd()
    //{
    //    if (Input.GetKeyUp(KeyCode.LeftShift))
    //    {
    //        if (!isGrab) return;

    //        //if (springJoint != null)
    //        //{
    //        //    Destroy(springJoint);
    //        //    springJoint = null;
    //        //    grabTarget = null;
    //        //    isGrab = false;
    //        //}

    //        if (fixedJoint != null)
    //        {

    //            Destroy(fixedJoint);
    //            fixedJoint = null;
    //            grabTarget = null;
    //            isGrab = false;

    //        }
    //    }

    //}

    //public bool pullForce;
    //public float pullStrength, pushStrength;
    //public float pullRange = 1.0f, pullRadius = 1.5f;

    //public Collider targetObject;

    //public Transform holdPosition;//, pushPosition;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        pullForce = true;
    //        GetPullObject();
    //    }
    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        PullForce();
    //    }
    //    if(Input.GetKeyUp(KeyCode.LeftShift))
    //    {

    //    }
    //    Debug.DrawRay(transform.position, transform.forward*pullRange, Color.blue);
    //}
    //public void GetPullObject()
    //{
    //    targetObject = null;
    //    RaycastHit hit;

    //    if(Physics.Raycast(transform.position, transform.forward, out hit, pullRange))
    //    {
    //        targetObject = hit.collider;
    //        Debug.Log(targetObject);
    //    }
    //}
    //public void PullForce()
    //{
    //    if(targetObject != null)
    //    {
    //        if (targetObject.GetComponent<Rigidbody>())
    //        {
    //            Vector3 dir = holdPosition.position - targetObject.transform.position;
    //            dir.y = 0;

    //            targetObject.GetComponent<Rigidbody>().velocity = dir * pullStrength * Time.deltaTime;
    //        }
    //    }
    //}
}
