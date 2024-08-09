using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoliceOfficer : MonoBehaviour
{
    private readonly float moveDistance = 99.0f;
    private readonly float moveSpeed = 5.0f;

    private void OnEnable()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        float distanceMoved = 0f;

        while (distanceMoved < moveDistance)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
            distanceMoved += moveSpeed * Time.deltaTime;
            yield return null;
        }

        StopCoroutine(MoveRoutine());
        gameObject.SetActive(false);
    }
}