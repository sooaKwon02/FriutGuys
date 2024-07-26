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
        yield return new WaitForSeconds(5.0f);

        if (groundPrefabs.Count > 2)
        {
            int num = Random.Range(0, groundPrefabs.Count);
            Rigidbody rb = groundPrefabs[num].GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            Destroy(groundPrefabs[num], 8.0f);
            groundPrefabs.RemoveAt(num);

            StartCoroutine(FallFloor());
        }
    }
}
