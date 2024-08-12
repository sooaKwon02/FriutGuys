using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(Des());
    }
    IEnumerator Des()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
