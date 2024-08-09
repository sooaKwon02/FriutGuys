using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFireController : MonoBehaviour
{
    private Ray ray;
    private RaycastHit raycastHit;

#pragma warning disable IDE0052
    private Vector3 forward = Vector3.forward;
#pragma warning restore IDE0052

    private Transform playerTransform;
    private Transform objectsCore;
    private Transform firePosition;

    private bool canFire;

    private void Awake()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerGameObject.transform;
        objectsCore = GetComponentInParent<CapsuleCollider>().transform;
        firePosition = GetComponentInChildren<LineRenderer>().transform;
    }

    private void Update()
    {
        objectsCore.LookAt(playerTransform);
        transform.rotation = objectsCore.rotation;
        Vector3 rayOrigin = firePosition.position;
        Vector3 rayDirection = firePosition.forward;
        ray = new Ray(rayOrigin, rayDirection);
        Debug.DrawRay(rayOrigin, rayDirection * 50, Color.green);

        if (Physics.Raycast(ray, out raycastHit, 50.0f))
        {
            if (raycastHit.collider.CompareTag("Player") && canFire == false)
            {
                canFire = true;
                StartCoroutine(FireRoutine());
            }
        }
    }

    private IEnumerator FireRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            BulletController.instance.GetBullet(firePosition.position, firePosition.rotation);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(3.0f);
        canFire = false;
        StopCoroutine(FireRoutine());
    }

    private void OnDisable()
    {
        canFire = false;
        StopAllCoroutines();
    }
}