using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCtrl : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> groundPrefabs;


    void Start()
    {
        StartCoroutine(FallFloor());
    }


    IEnumerator FallFloor()
    {
        if (groundPrefabs.Count > 2)
        {
            yield return new WaitForSeconds(10.0f);
            int num = Random.Range(0, groundPrefabs.Count);

            Rigidbody rb = groundPrefabs[num].GetComponentInChildren<Rigidbody>();
            Animator anim = groundPrefabs[num].GetComponent<Animator>();

            anim.SetBool("isShake", true);

            yield return new WaitForSeconds(5.0f);
            if (rb != null)
            {
                anim.SetBool("isShake", false);
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            Destroy(groundPrefabs[num], 8.0f);
            groundPrefabs.RemoveAt(num);

            StartCoroutine(FallFloor());
        }
    }
}
