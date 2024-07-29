using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviourPun, IPunObservable
{
   
    PhotonView pv;
    private Rigidbody rb;
    private Animator anim;
    private CapsuleCollider coll;

    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private Transform player;
    private Transform myTr;
    public float camSpeed = 4.0f;

    public float slideSpeed = 5.0f;
    public float slideCooltime = 1.0f;

    private bool isSliding = false;
    private float slideTimer = 0.0f;
    private float cooltimeTimer = 0.0f;

    public float jumpForce = 5.0f;

    private float jumpCooltimeTimer = 0.0f;

    [SerializeField]
    bool isGround = false;
    bool isJump = false;
    bool isMove = false;

    public GameObject slideCollider;

    public Transform catchPoint;
    private GameObject catchObject = null;

    //public GameObject failedText;

    Vector3 currPos = Vector3.zero;
    Quaternion currRot = Quaternion.identity;
    float jumpY;

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
        if (GameObject.FindGameObjectWithTag("Holder"))
        {
            transform.SetParent(GameObject.FindGameObjectWithTag("Holder").transform);
            GameObject.FindGameObjectWithTag("Holder").GetComponent<CharacterCustom>().Hold();
        }    
    }
   
  
    void Update()
    {
        if (pv.IsMine)
        {
            LookAround();
            CheckGround();
            if (Input.GetKeyDown(KeyCode.Space) && isGround && pv.IsMine)
            {
                isJump = true;
            }
            Slide();
        }else
        {
            myTr.position = Vector3.Lerp(myTr.position, currPos, Time.deltaTime * 3.0f);
            player.rotation = Quaternion.Slerp(player.rotation, currRot, Time.deltaTime * 3.0f);
            Vector3 velocity = rb.velocity;
            velocity.y = Mathf.Lerp(velocity.y, jumpY, Time.deltaTime * 30.0f);
            rb.velocity = velocity;            
        }
        Debug.Log(isMove);
    }
    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            if (!isSliding)
            {
                PlayerMove();
            }
            //PlayerMove();
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
            jumpY=(float)stream.ReceiveNext();
        }
    }
   

    void PlayerMove()
    {
        Debug.DrawRay(cam.transform.position, 
            new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized, Color.red);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

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

            float rotSpeed = 10f;

            // �÷��̾ �ٶ� ��ǥ ����
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            //playerȸ��
            player.rotation = Quaternion.Slerp(player.rotation, targetRot, Time.deltaTime * rotSpeed);
            //������
            rb.position += moveDir * Time.deltaTime * 5.0f;
        }
    }

    void LookAround()
    {

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * camSpeed);
        Vector3 camAngle = cam.transform.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        //ī�޶��� ������ 180�� ���϶��
        if(x < 180f)
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
        Debug.DrawRay(rb.position, Vector3.down * 0.1f ,Color.red);

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

        if (Input.GetKeyDown(KeyCode.LeftControl) && cooltimeTimer <= 0)
        {
            isMove = false;
            isSliding = true;
            slideTimer = 1.0f;
            cooltimeTimer = 1.0f;
            //�����̵� �ݶ��̴� Ű��
            slideCollider.SetActive(true);
            //�����ݶ��̴� ���ֱ�
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
                //�����̵� ���� ���ϰ� �����̵�
                Vector3 slideDir = player.transform.forward;
                //rb�� ������ �о�� ������ �ӵ��� ������
                rb.MovePosition(rb.position + slideDir * slideSpeed * Time.deltaTime);
                //�����̵� �ð� ���̱�
                slideTimer -= Time.deltaTime;
            }
            else
            {
                //�����̵� Ÿ�̸Ӱ� 0�̸� �����̵� ����
                isSliding = false;
                isMove = true;

                //�����̵� �ݶ��̴� ����
                slideCollider.SetActive(false);
                //���� �ݶ��̴� Ŵ
                coll.enabled = true;
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("@@");
            anim.SetTrigger("Die");
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
}