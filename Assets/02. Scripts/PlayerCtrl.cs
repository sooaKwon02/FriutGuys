using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviourPun, IPunObservable
{
    public PhotonView pv;
    public Rigidbody rb;
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
    //리스폰 포인트
    public Vector3 point;
    [SerializeField]
    bool isGround = false;
    [SerializeField]
    bool isJump = false;

    bool isMove = false;
    public bool isColl = false;

    public GameObject slideCollider;

    public GameObject Menu;

    public Text playerTxt;
    public Image camHide;
    Vector3 currPos = Vector3.zero;
    Quaternion currRot = Quaternion.identity;
    float jumpY;

    public Collider targetObject;

    public Transform holdPosition;

    public bool isGoalin = false;
    public bool isAlive = false;

    public CharacterCustom custom;
    AudioSource audiosource;
    [SerializeField]
    AudioClip[] audioClip;
 
    public bool cookie=false;
    bool stop;
    public ParticleSystem[] par;
    public bool startGame;
    void Awake()
    {
        stop = false;
        audiosource = GetComponent<AudioSource>();
        myTr = GetComponent<Transform>();

        currPos = myTr.position;
        currRot = player.rotation;

        DontDestroyOnLoad(this);
        pv = GetComponent<PhotonView>();
        PhotonNetwork.SendRate = 10;
        pv.Synchronization = ViewSynchronization.ReliableDeltaCompressed;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<CapsuleCollider>();
        //failedText.SetActive(false);
        isGoalin = false;
        isAlive = false;
    }
    void Start()
    {
        startGame = false;
        if (pv.IsMine)
        {
            custom = GetComponent<CharacterCustom>();
            camHide.gameObject.SetActive(true);
        }     
    }

    void Update()
    {
        if (isGoalin && Input.GetMouseButtonDown(0)&&pv.IsMine)
        {
            Watching();
        }
        if (pv.IsMine && !stop)
        {
            
            LookAround();
            CheckGround();
            if (Input.GetKeyDown(KeyCode.Space) && isGround && pv.IsMine && !isColl)
            {
                isJump = true;
            }
            if(custom&& startGame)
            {
                custom.UseItem();
            }
            Slide();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuOnOff();
            }
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

    int watchIndex = -1;
    void Watching()
    {
        PlayerCtrl[] pv = FindObjectsOfType<PlayerCtrl>();
        int startIndex = (watchIndex + 1) % pv.Length;
        for (int i = 0; i < pv.Length; i++)
        {
            int index = (startIndex + i) % pv.Length;

            if (!pv[index].GetComponent<PlayerCtrl>().isGoalin)
            {
                PlayerCtrl nextPlayer = pv[index];
                if (nextPlayer.GetComponent<PlayerCtrl>().cam != null)
                {
                    Camera.main.transform.SetParent(nextPlayer.cam.transform);
                    Camera.main.transform.localPosition = new Vector3(0, -4f, 5f);
                    Camera.main.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
                    watchIndex = index;
                    return;
                }
            }
        }
    }

    void MenuOnOff()
    {
        if (Menu.activeSelf)
        {
            Menu.SetActive(false);
        }
        else
            Menu.SetActive(true);
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

            Quaternion targetRot = Quaternion.LookRotation(moveDir);

            player.rotation = Quaternion.Slerp(player.rotation, targetRot, Time.deltaTime * rotSpeed);

            rb.position += moveDir * Time.deltaTime * moveSpeed;
        }
    }

    void LookAround()
    {

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * camSpeed);
        Vector3 camAngle = cam.transform.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, 0f, 70f);
        }
        else
        {
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
            Vector3 jumpVel = Vector3.up * jumpForce;
            rb.AddForce(jumpVel, ForceMode.Impulse);
            anim.SetTrigger("Jump");
            isJump = false;
        }
    }
    float timer = 0.0f;
    void CheckGround()
    {
        RaycastHit hit;
        Debug.DrawRay(rb.position, Vector3.down * 1.0f, Color.red);

        if (Physics.Raycast(rb.position, Vector3.down, out hit, 0.1f))
        {
            if (hit.collider != null)
            {
                isGround = true;
                isColl = false;
                timer = 0;
                anim.SetBool("isFall", false);
                return;
            }
        }
        
        isGround = false;
        if (!isGround) 
        { 
            timer += Time.deltaTime;
            if(timer > 1.5f)
            {
                isColl = true;
                anim.SetBool("isFall", true);
            }
        }
    }

    void Slide()
    {
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
        if (cookie)
        {
            if (coll.transform.CompareTag("Player") || coll.transform.CompareTag("SlideCollider"))
            {
            
                Rigidbody targetRb = coll.rigidbody;
                if (targetRb == null)
                {
                    targetRb = coll.transform.GetComponentInParent<Rigidbody>();
                }
                if (targetRb != null)
                {
                    targetRb.AddForce(rb.velocity * 1000, ForceMode.Impulse);
                }            
            }
        }
    }
    public void BuffTime(int i)
    {
        StartCoroutine(BuffTimeCo(i));
    }
    IEnumerator BuffTimeCo(int i)
    {
        yield return new WaitForSeconds(10f);
        cookie = false;
        moveSpeed = 5f;
        jumpForce = 5f;
        par[i].Stop();
    }
    public void DeBuffTime(int i)
    {
        StartCoroutine(DeBuffTimeCo(i));
    }
    IEnumerator DeBuffTimeCo(int i)
    {
        yield return new WaitForSeconds(10f);
        moveSpeed = 5f;
        jumpForce = 5f;
        camHide.color = new Color(0, 0, 0, 0);
        par[i].Stop();
    }
   
    void Des(GameObject obj)
    {
        Destroy(obj);
    }

    IEnumerator ReMove()
    {
        yield return new WaitForSeconds(1f);

        isColl = false;
    }

    public void RespawnPointSet()
    {
        if (pv.IsMine)
        {
            transform.position = point;
            pv.RPC("OtherRespawnSet", RpcTarget.Others, point);
        }
    }
    [PunRPC]
    void OtherRespawnSet(Vector3 _point)
    {
        if (!pv.IsMine)
        {
            transform.position = _point;
        }
    }

    [PunRPC]
    public void vic()
    {
        moveSpeed = 0f;
        isColl = true;
        playerTxt.text = isGoalin ? playerTxt.text = "성공" : playerTxt.text = "실패";
    }
    public void GameOver()
    {
        if (pv.IsMine)
        {
            StartCoroutine(Over());
        }
    }
    IEnumerator Over()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LeaveRoom();
    }
  

}