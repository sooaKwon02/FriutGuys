using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    private readonly float rotationSpeed = 25.0f;

    public bool reverse = false;

    private void Update()
    {
        if (!reverse)
        {
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        }

        else
        {
            transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        }
    }
}