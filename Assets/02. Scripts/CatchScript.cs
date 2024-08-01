using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{ }
    //public float grabDistance = 2.0f;
    //private GameObject grabTarget;
    //private SpringJoint springJoint;
    //private FixedJoint fixedJoint;
    //[SerializeField]
    //Transform GrabPoint = null;
    //private bool isGrab = false;
    //Animator anim;
    //Rigidbody rb;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    anim = GetComponentInParent<Animator>();
    //}



    //[SerializeField] Transform GrabPoint = null;
    //[SerializeField] private LayerMask AccesibleLayers = new LayerMask();
//    [SerializeField] private static float breakForce = 10000, breakTorque = 10000;

//    private Collider[] ObjectsInCloseProximity;
//    [HideInInspector] public bool isGrabbing = false;
//    //private SpringJoint springJoint;
//    public GameObject GrabbedObj;

//    private void FixedUpdate()
//    {
//        if ((springJoint != null && springJoint.connectedBody == null) || (springJoint == null && GrabbedObj != null))
//        {
//            ReleaseObj(GrabbedObj);
//        }
//    }
//    private void Update()
//    {
//        TryGrabObj();
//    }
//    public void TryGrabObj()
//    {
//        ObjectsInCloseProximity = Physics.OverlapBox(GrabPoint.position, new Vector3(0.4f, 1, 0.6f), Quaternion.identity);

//        if (ObjectsInCloseProximity.Length > 0)
//        {
//            int id = 0;
//            while (id < ObjectsInCloseProximity.Length)
//            {
//                Collider obj = ObjectsInCloseProximity[id];
//                if (obj.tag != "Bullet" && obj.attachedRigidbody == GetComponent<Rigidbody>())
//                {
//                    id++;
//                    continue;
//                }

//                if (!isGrabbing && Input.GetKeyDown(KeyCode.LeftShift))
//                {
//                    GrabAndAssign(obj);
//                }
//                else if(isGrabbing && Input.GetKeyUp(KeyCode.LeftShift)) 
//                {
//                    ReleaseObj(GrabbedObj);
//                }
//                break;
//            }
//        }
//    }

//    private void GrabAndAssign(Collider obj)
//    {
//        var rb = obj.attachedRigidbody;
//        GrabbedObj = obj.gameObject;

//        if (obj.gameObject.tag == "Bullet")
//        {
//            Destroy(rb);
//            obj.transform.parent.position = GrabPoint.position;
//            obj.transform.parent.SetParent(GrabPoint);
//        }
//        else
//{
//    springJoint = gameObject.AddComponent<SpringJoint>();
//    springJoint.autoConfigureConnectedAnchor = false;
//    springJoint.connectedAnchor = GrabPoint.localPosition;
//    springJoint.damper = 100000;
//    springJoint.spring = 100000;
//    springJoint.minDistance = 0.8f;
//    springJoint.enableCollision = true;
//    springJoint.breakForce = breakForce;
//    springJoint.breakTorque = breakTorque;
//    springJoint.connectedBody = rb;
//}

//isGrabbing = true;
//GetComponent<PlayerCtrl>().anim.SetBool("isCatch", true);
//    }

//    private void ReleaseObj(GameObject obj)
//{
//    if (obj == null) return;

//    if (obj.tag == "Bullet")
//    {
//        obj.transform.SetParent(null);
//        Rigidbody rb = obj.AddComponent<Rigidbody>();
//        rb.mass = 1;
//        rb.drag = 1;
//        rb.angularDrag = 1;
//        rb.interpolation = RigidbodyInterpolation.Interpolate;
//    }
//    else
//    {
//        if (springJoint != null)
//            Destroy(springJoint);
//    }

//    GrabbedObj = null;
//    isGrabbing = false;
//    GetComponent<PlayerCtrl>().anim.SetBool("isCatch", false);
//}
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

    //        if (Physics.Raycast(transform.position, player.transform.forward, out RaycastHit hit, grabDistance))
    //        {
    //            if (hit.collider.CompareTag("Player"))
    //            {
    //                Debug.Log(hit.collider.gameObject.name);
    //                grabTarget = hit.collider.gameObject;

    //                springJoint = gameObject.AddComponent<SpringJoint>();
    //                springJoint.connectedBody = grabTarget.GetComponent<Rigidbody>();
    //                springJoint.spring = 1000.0f;
    //                springJoint.damper = 0.0f;
    //                springJoint.minDistance = 0.0f;
    //                springJoint.maxDistance = 0.1f;
    //                springJoint.autoConfigureConnectedAnchor = false;
    //                springJoint.connectedAnchor = GrabPoint.localPosition;
    //                springJoint.damper = 100000;
    //                springJoint.spring = 100000;
    //                springJoint.minDistance = 0.8f;
    //                springJoint.enableCollision = true;
    //                springJoint.breakForce = 10000;
    //                springJoint.breakTorque = 10000;
    //                springJoint.connectedBody = rb;
    //                if (grabTarget != null)
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

    ////아무때나 키 누르면 전의 오브젝트 물체가 플레이어를 따라옴
    ////null값으로 만들어줘야 함.
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject == grabTarget)
    //    {
    //        target = null;
    //    }
    //}
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

