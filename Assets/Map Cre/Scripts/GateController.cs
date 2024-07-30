using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Vector3 originalPosition;

    private readonly float moveReturnDelay = 1.0f;
    private readonly float moveDistance = 3.0f;
    private float moveDelay;
    private float moveSpeed;
    private int random;

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        random = Random.Range(0, 4);

        if (random == 0)
        {
            moveDelay = 4.0f;
            moveSpeed = 1.0f;

            while (true)
            {
                yield return new WaitForSeconds(moveDelay);
                float distanceMoved = 0f;

                while (distanceMoved < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
                    distanceMoved += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(moveReturnDelay);
                float distanceReturned = 0f;

                while (distanceReturned < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
                    distanceReturned += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                transform.position = originalPosition;
                StopCoroutine(MoveRoutine());
                yield return StartCoroutine(MoveRoutine());
            }
        }

        else if (random == 1)
        {
            moveDelay = 2.0f;
            moveSpeed = 2.0f;

            while (true)
            {
                yield return new WaitForSeconds(moveDelay);
                float distanceMoved = 0f;

                while (distanceMoved < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
                    distanceMoved += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(moveReturnDelay);
                float distanceReturned = 0f;

                while (distanceReturned < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
                    distanceReturned += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                transform.position = originalPosition;
                StopCoroutine(MoveRoutine());
                yield return StartCoroutine(MoveRoutine());
            }
        }

        else if (random == 2)
        {
            moveDelay = 1.0f;
            moveSpeed = 4.0f;

            while (true)
            {
                yield return new WaitForSeconds(moveDelay);
                float distanceMoved = 0f;

                while (distanceMoved < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
                    distanceMoved += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(moveReturnDelay);
                float distanceReturned = 0f;

                while (distanceReturned < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
                    distanceReturned += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                transform.position = originalPosition;
                StopCoroutine(MoveRoutine());
                yield return StartCoroutine(MoveRoutine());
            }
        }

        else if (random == 3)
        {
            moveDelay = 0.5f;
            moveSpeed = 8.0f;

            while (true)
            {
                yield return new WaitForSeconds(moveDelay);
                float distanceMoved = 0f;

                while (distanceMoved < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
                    distanceMoved += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(moveReturnDelay);
                float distanceReturned = 0f;

                while (distanceReturned < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
                    distanceReturned += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                transform.position = originalPosition;
                StopCoroutine(MoveRoutine());
                yield return StartCoroutine(MoveRoutine());
            }
        }
    }
}