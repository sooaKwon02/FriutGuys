using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform player;

    public float camSpeed = 4.0f;

    public float slideSpeed = 5.0f;
    public float slideCooltime = 1.0f;

    private bool isSliding = false;
    private float slideTimer = 0.0f;
    private float cooltimeTimer = 0.0f;

    public float jumpForce = 5.0f;
    [SerializeField]
    bool isGround = false;
    bool isJump = false;

    public GameObject slideCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        LookAround();
        CheckGround();
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;
        }
        Slide();
    }

    void FixedUpdate()
    {
        PlayerMove();
        Sliding();
        Jump();
    }

    void PlayerMove()
    {
        Debug.DrawRay(cam.position, 
            new Vector3(cam.forward.x, 0f, cam.forward.z).normalized, Color.red);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //카메라가 보는 앞 방향 
        Vector3 moveInput = new Vector2(h, v);
        bool isMove = moveInput.magnitude != 0;
        
        anim.SetBool("isMove", isMove);
        anim.SetFloat("MoveX", h);
        anim.SetFloat("MoveY", v);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cam.forward.x, 0f, cam.forward.z).normalized;
            Vector3 lookRight = new Vector3(cam.right.x, 0f, cam.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            float rotSpeed = 10f;

            // 플레이어가 바라볼 목표 방향
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            //player회전
            player.rotation = Quaternion.Slerp(player.rotation, targetRot, Time.deltaTime * rotSpeed);
            //움직임
            rb.position += moveDir * Time.deltaTime * 5.0f;
        }
    }

    void LookAround()
    {

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * camSpeed);
        Vector3 camAngle = cam.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if(x < 180f)
        {
            x = Mathf.Clamp(x, 0f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cam.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    void Jump()
    {
        if (isJump)
        {
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
            if (hit.transform.tag != null)
            {
                isGround = true;
                return;
            }
        }
        isGround = false;
    }

    void Slide()
    {
        //재시작시간이 0보다 크면 빼주기
        if (cooltimeTimer > 0)
        {
            cooltimeTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && cooltimeTimer <= 0)
        {
            isSliding = true;
            slideTimer = 1.0f;
            cooltimeTimer = 1.0f;
            //슬라이딩 콜라이더 키고
            slideCollider.SetActive(true);
            //원래콜라이더 꺼주기
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            anim.SetTrigger("slide");
        }
    }

    void Sliding()
    {
        if (isSliding)
        {
            if (slideTimer > 0)
            {
                //슬라이딩 방향 정하고 슬라이딩
                Vector3 slideDir = player.transform.forward;
                //rb의 방향을 읽어와 일정한 속도로 움직임
                rb.MovePosition(rb.position + slideDir * slideSpeed * Time.deltaTime);
                //슬라이딩 시간 줄이기
                slideTimer -= Time.deltaTime;
            }
            else
            {
                //슬라이딩 타이머가 0이면 슬라이딩 종료
                isSliding = false;

                //슬라이딩 콜라이더 끄고
                slideCollider.SetActive(false);
                //원래 콜라이더 킴
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
            }
        }
    }
}
