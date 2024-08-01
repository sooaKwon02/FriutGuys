using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController : MonoBehaviour
{
    private float rotationSpeed = 0.0f;

    public bool reverse = false;

    private void Start()
    {
        StartCoroutine(RotationSpeedChange());
    }

    private void Update()
    {
        if (!reverse)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }

        else
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
        }
    }

    private IEnumerator RotationSpeedChange()
    {
        rotationSpeed = Random.Range(5, 21);
        yield return new WaitForSeconds(30);
    }
}