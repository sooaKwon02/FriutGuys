using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutCtrl : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    private Vector3 offset;
    private Vector3 targetPosition;

    private void Awake()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        //child = transform.GetChild(0).GetComponent<Transform>();
        //Debug.Log(child.gameObject.name);
    }

    //void Start()
    //{
    //    offset = rb.transform.position - transform.position;
    //}
    //private void Update()
    //{
    //    transform.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
    //}
    ////void FixedUpdate()
    ////{
    ////    //Vector3 targetPosition = transform.position + (Quaternion.Euler(0, transform.eulerAngles.y, 0) * offset);  
    ////    rb.MovePosition(targetPosition);
    ////}

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision != null)
    //    {
    //        targetPosition = transform.position + (Quaternion.Euler(0, transform.eulerAngles.y, 0) * offset);
    //        rb.MovePosition(targetPosition);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        // 플레이어와 원판이 충돌했을 때, 부모 관계를 설정하여 이동시킬 수 있습니다.
    //        collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //        transform.SetParent(this.transform);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject == disc.gameObject)
    //    {
    //        // 플레이어가 원판에서 떨어졌을 때 부모 관계를 해제합니다.
    //        rb.isKinematic = false;
    //        transform.SetParent(null);
    //    }
    //}

    //부딪히면 플레이어가 움직이게
    //부딪혀야면 계산이 되고 fixed가 동작할수있게
}
