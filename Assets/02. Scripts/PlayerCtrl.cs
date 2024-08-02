using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerCtrl : MonoBehaviourPun, IPunObservable
{
    PhotonView pv;
    private Rigidbody rb;
    public Animator anim;
    private CapsuleCollider coll;

    public GameObject cam;
    [SerializeField]
    private Transform player;
    private Transform myTr;

    public float camSpeed = 4.0f;
    public float moveSpeed = 5.0f;
    float rotSpeed = 10f;

    public float slideSpeed = 5.0f;
    public float slideCooltime = 1.0f;

    private bool isSliding = false;
    private float slideTimer = 0.0f;
    private float cooltimeTimer = 0.0f;

    public float jumpForce = 5.0f;

    private float jumpCooltimeTimer = 0.0f;

    [SerializeField]
    bool isGround = false;
    [SerializeField]
    bool isJump = false;

    bool isMove = false;
    bool isColl = false;

    public GameObject slideCollider;

    //public float grabDistance = 2.0f;
    //private GameObject grabTarget;
    ////private SpringJoint springJoint;
    //private FixedJoint fixedJoint;
    //private bool isGrab = false;

    //public GameObject failedText;

    Vector3 currPos = Vector3.zero;
    Quaternion currRot = Quaternion.identity;
    float jumpY;

    public bool pullForce;
    public float pullStrength, pushStrength;
    public float pullRange = 1.0f, pullRadius = 1.5f;

    public Collider targetObject;

    public Transform holdPosition;//, pushPosition;

    void Awake()
    {
        myTr = GetComponent<Transform>();

        currPos = myTr.position;
        currRot = player.rotation;

        DontDestroyOnLoad(this);
        pv = GetComponent<PhotonView>();
        PhotonNetwork.SendRate = 120;
        pv.Synchronization = ViewSynchronization.Unreliable;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<CapsuleCollider>();
        //failedText.SetActive(false);    
    }

    void Update()
    {
        if (pv.IsMine)
        {
            LookAround();
            CheckGround();
            if (Input.GetKeyDown(KeyCode.Space) && isGround && pv.IsMine && !isColl)
            {
                isJump = true;
            }
            Slide();
            //잡기 아직 멀음..
            //Grab();
            //GrabEnd();
            //Debug.DrawRay(transform.position, player.transform.forward * grabDistance, Color.blue);
            //if (Input.GetKeyDown(KeyCode.LeftShift))
            //{
            //    pullForce = true;
            //    GetPullObject();
            //}
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    PullForce();
            //}
            //if (Input.GetKeyUp(KeyCode.LeftShift))
            //{
            //    anim.SetBool("isCatch", false);
            //    moveSpeed = 5.0f;
            //}
            //Debug.DrawRay(holdPosition.transform.position, holdPosition.transform.forward * pullRange, Color.green);
        }
        else
        {
            myTr.position = Vector3.Lerp(myTr.position, currPos, Time.deltaTime * 3.0f);
            player.rotation = Quaternion.Slerp(player.rotation, currRot, Time.deltaTime * 3.0f);
            Vector3 velocity = rb.velocity;
            velocity.y = Mathf.Lerp(velocity.y, jumpY, Time.deltaTime * 30.0f);
            rb.velocity = velocity;
        }
    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            if (!isSliding)
            {
                PlayerMove();
            }
            Sliding();
            Jump();
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //로컬 플레이어의 위치 정보를 송신
        if (stream.IsWriting)
        {
            //박싱
            stream.SendNext(myTr.position);
            stream.SendNext(player.rotation);
            stream.SendNext(rb.velocity.y);
        }
        //원격 플레이어의 위치 정보를 수신
        else
        {
            //언박싱
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
            jumpY = (float)stream.ReceiveNext();
        }
    }
    float v;

    void PlayerMove()
    {
        Debug.DrawRay(cam.transform.position,
            new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized, Color.red);
        float h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //ī�޶� ���� �� ���� 
        Vector3 moveInput = new Vector2(h, v);
        //어느방향으로든 움직이면 true
        isMove = moveInput.magnitude != 0;

        anim.SetBool("isMove", isMove);
        anim.SetFloat("MoveX", h);
        anim.SetFloat("MoveY", v);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
            Vector3 lookRight = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            // �÷��̾ �ٶ� ��ǥ ����
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            //playerȸ��
            player.rotation = Quaternion.Slerp(player.rotation, targetRot, Time.deltaTime * rotSpeed);
            //������
            rb.position += moveDir * Time.deltaTime * moveSpeed;
        }
    }

    void LookAround()
    {

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * camSpeed);
        Vector3 camAngle = cam.transform.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        //ī�޶��� ������ 180�� ���϶��
        if (x < 180f)
        {
            //0�� 70������ ������ ����
            x = Mathf.Clamp(x, 0f, 70f);
        }
        else
        {
            //180�� �̻��̶�� 
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cam.transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    void Jump()
    {
        if (jumpCooltimeTimer > 0)
        {
            jumpCooltimeTimer -= Time.deltaTime;
        }

        if (isJump)
        {
            jumpCooltimeTimer = 1.0f;
            Vector3 jumpVel = Vector3.up * jumpForce;//Mathf.Sqrt(jumpForce * -Physics.gravity.y);
            rb.AddForce(jumpVel, ForceMode.Impulse);
            anim.SetTrigger("Jump");
            isJump = false;
        }
    }

    void CheckGround()
    {
        RaycastHit hit;
        Debug.DrawRay(rb.position, Vector3.down * 0.1f, Color.red);

        if (Physics.Raycast(rb.position, Vector3.down, out hit, 0.1f))
        {
            if (hit.collider != null)
            {
                isGround = true;
                return;
            }
        }
        isGround = false;
    }

    void Slide()
    {
        //����۽ð��� 0���� ũ�� ���ֱ�
        if (cooltimeTimer > 0)
        {
            cooltimeTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && cooltimeTimer <= 0 && !isColl)
        {
            isMove = false;
            isSliding = true;
            slideTimer = 1.0f;
            cooltimeTimer = 1.0f;
            //slide콜라이더 켜주고
            slideCollider.SetActive(true);
            //원래 콜라이더 꺼주기
            coll.enabled = false;
            anim.SetTrigger("slide");
        }
    }

    void Sliding()
    {
        if (isSliding)
        {
            if (slideTimer > 0)
            {
                //플레이어의 앞방향
                Vector3 slideDir = player.transform.forward;
                rb.MovePosition(rb.position + slideDir * slideSpeed * Time.deltaTime);
                //타이머
                slideTimer -= Time.deltaTime;
            }
            else
            {
                //�����̵� Ÿ�̸Ӱ� 0�̸� �����̵� ����
                isSliding = false;
                isMove = true;

                //slide콜라이더 꺼주고
                slideCollider.SetActive(false);
                //원래 콜라이더 다시 켜기
                coll.enabled = true;
            }
        }
    }

    public void GetPullObject()
    {
        targetObject = null;
        RaycastHit hit;

        if (Physics.Raycast(holdPosition.transform.position, holdPosition.transform.forward, out hit, pullRange))
        {
            targetObject = hit.collider;
            Debug.Log(targetObject);
        }
    }
    public void PullForce()
    {
        if (targetObject != null)
        {
            anim.SetBool("isCatch", true);
            if (targetObject.GetComponent<Rigidbody>() && v < 0)
            {
                Vector3 dir = holdPosition.position - targetObject.transform.position;
                dir.y = 0;
                //moveSpeed = moveSpeed / 2.0f;
                //rotSpeed = 0;
                targetObject.GetComponent<Rigidbody>().velocity = dir * pullStrength * Time.deltaTime;
            }
            else if (targetObject.GetComponent<Rigidbody>() && v > 0)
            {
                Vector3 dir = targetObject.transform.position - holdPosition.position;
                dir.y = 0;
                //moveSpeed = moveSpeed / 2.0f;
                //rotSpeed = 0;
                targetObject.GetComponent<Rigidbody>().velocity = dir * pullStrength * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Obstacle"))
        {
            //장애물에 부딪혔을 시 슬라이딩과 점프를 하지 못하도록
            isColl = true;

            anim.SetTrigger("Dizzy");

            //1초 뒤 다시 점프와 슬라이딩 가능
            StartCoroutine(ReMove());

        }
        if (coll.transform.tag == "Bullet")
        {
            Destroy(coll.gameObject);
            pv.RPC("Des", RpcTarget.Others, coll);
        }
        if (coll.transform.tag == "Bullet")
        {
            Destroy(coll.gameObject);
            pv.RPC("Des", RpcTarget.Others, coll);
        }
    }
    [PunRPC]
    void Des(GameObject obj)
    {
        Destroy(obj);
    }

    IEnumerator ReMove()
    {
        yield return new WaitForSeconds(1f);

        isColl = false;
    }

    //잡기 아직 구현중..
    //public float grabDistance = 2.0f;
    //private GameObject grabTarget;
    //private SpringJoint springJoint;
    //[SerializeField]
    //Transform GrabPoint = null;
    //private bool isGrab = false;
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

    //                springJoint = grabTarget.AddComponent<SpringJoint>();
    //                springJoint.autoConfigureConnectedAnchor = false;
    //                springJoint.connectedAnchor = GrabPoint.localPosition;
    //                springJoint.damper = 10000;
    //                springJoint.spring = 10000;
    //                springJoint.minDistance = 0.5f;
    //                springJoint.maxDistance = 1.0f;
    //                springJoint.enableCollision = true;
    //                springJoint.breakForce = 10000;
    //                springJoint.breakTorque = 10000;
    //                springJoint.connectedBody = rb;
    //            }
    //        }
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

    //void GrabEnd()
    //{
    //    if (Input.GetKeyUp(KeyCode.LeftShift))
    //    {
    //        if (!isGrab) return;

    //        if (springJoint != null)
    //        {
    //            Destroy(springJoint);
    //            springJoint = null;
    //            grabTarget = null;
    //            isGrab = false;
    //        }
    //    }
    //}
}
    
