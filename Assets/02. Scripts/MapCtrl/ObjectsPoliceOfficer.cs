using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoliceOfficer : MonoBehaviour
{
    private GameObject shield;

    private readonly float moveDistance = 99.0f;
    private readonly float moveSpeed = 5.0f;

    private void OnEnable()
    {
        StartCoroutine(MoveRoutine());
        ShieldDeploy();
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

    private void ShieldDeploy()
    {
        GameObject shieldObject = FindChildWithTag(transform, "Shield");
        shield = shieldObject;
        shield.SetActive(true);
    }

    private GameObject FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }

            GameObject found = FindChildWithTag(child, tag);

            if (found != null)
            {
                return found;
            }
        }

        return null;
    }
}