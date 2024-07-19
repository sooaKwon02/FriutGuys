using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    private GameObject target = null;
    Animator anim;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        // 키를 눌렀을 때 물체를 자식으로 만들기
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (target != null)
            {
                target.transform.SetParent(transform);
                anim.SetBool("isCatch", true);
            }
            else
            {
                //물체가 null이면 못잡는 애니메이션
                anim.SetTrigger("NonCatch");
            }
        }

        // 키를 뗐을 때 자식에서 해제
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if(target != null)
                target.transform.SetParent(null);
            anim.SetTrigger("Catch");
            anim.SetBool("isCatch", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null)//추후 맵에서 tag로 변경예정
        {
            target = other.gameObject;
        }
    }

    //아무때나 키 누르면 전의 오브젝트 물체가 플레이어를 따라옴
    //null값으로 만들어줘야 함.
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
        }
    }
}
