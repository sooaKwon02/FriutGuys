using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Vector3 originalPosition;

    private readonly float moveDelay = 0.1f;
    private readonly float moveSpeed = 10.0f;
    private float moveDistance;

    public int count = 0;
    public bool reverse = false;

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        if (count == 0)
        {
            moveDistance = 27.8f;

            while (true)
            {
                yield return new WaitForSeconds(moveDelay);
                float distanceMoved = 0f;

                while (distanceMoved < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                    distanceMoved += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(moveDelay);
                float distanceReturned = 0f;

                while (distanceReturned < moveDistance)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                    distanceReturned += moveSpeed * Time.deltaTime;
                    yield return null;
                }

                transform.position = originalPosition;
                StopCoroutine(MoveRoutine());
                yield return StartCoroutine(MoveRoutine());
            }
        }

        else if (count == 1)
        {
            moveDistance = 13.9f;

            if (reverse == true)
            {
                while (true)
                {
                    yield return new WaitForSeconds(moveDelay);
                    float distanceMoved = 0f;

                    while (distanceMoved < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                        distanceMoved += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    yield return new WaitForSeconds(moveDelay);
                    float distanceReturned = 0f;

                    while (distanceReturned < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                        distanceReturned += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    transform.position = originalPosition;
                    StopCoroutine(MoveRoutine());
                    yield return StartCoroutine(MoveRoutine());
                }
            }

            else if (reverse == false)
            {
                while (true)
                {
                    yield return new WaitForSeconds(moveDelay);
                    float distanceMoved = 0f;

                    while (distanceMoved < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                        distanceMoved += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    yield return new WaitForSeconds(moveDelay);
                    float distanceReturned = 0f;

                    while (distanceReturned < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                        distanceReturned += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    transform.position = originalPosition;
                    StopCoroutine(MoveRoutine());
                    yield return StartCoroutine(MoveRoutine());
                }
            }
        }

        else if (count == 2)
        {
            moveDistance = 6.95f;

            if (reverse == true)
            {
                while (true)
                {
                    yield return new WaitForSeconds(moveDelay);
                    float distanceMoved = 0f;

                    while (distanceMoved < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                        distanceMoved += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    yield return new WaitForSeconds(moveDelay);
                    float distanceReturned = 0f;

                    while (distanceReturned < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                        distanceReturned += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    transform.position = originalPosition;
                    StopCoroutine(MoveRoutine());
                    yield return StartCoroutine(MoveRoutine());
                }
            }

            else if (reverse == false)
            {
                while (true)
                {
                    yield return new WaitForSeconds(moveDelay);
                    float distanceMoved = 0f;

                    while (distanceMoved < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                        distanceMoved += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    yield return new WaitForSeconds(moveDelay);
                    float distanceReturned = 0f;

                    while (distanceReturned < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                        distanceReturned += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    transform.position = originalPosition;
                    StopCoroutine(MoveRoutine());
                    yield return StartCoroutine(MoveRoutine());
                }
            }
        }

        else if (count == 3)
        {
            moveDistance = 3.475f;

            if (reverse == true)
            {
                while (true)
                {
                    yield return new WaitForSeconds(moveDelay);
                    float distanceMoved = 0f;

                    while (distanceMoved < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                        distanceMoved += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    yield return new WaitForSeconds(moveDelay);
                    float distanceReturned = 0f;

                    while (distanceReturned < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                        distanceReturned += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    transform.position = originalPosition;
                    StopCoroutine(MoveRoutine());
                    yield return StartCoroutine(MoveRoutine());
                }
            }

            else if (reverse == false)
            {
                while (true)
                {
                    yield return new WaitForSeconds(moveDelay);
                    float distanceMoved = 0f;

                    while (distanceMoved < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                        distanceMoved += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    yield return new WaitForSeconds(moveDelay);
                    float distanceReturned = 0f;

                    while (distanceReturned < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                        distanceReturned += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    transform.position = originalPosition;
                    StopCoroutine(MoveRoutine());
                    yield return StartCoroutine(MoveRoutine());
                }
            }
        }

        else if (count == 4)
        {
            moveDistance = 1.7375f;

            if (reverse == true)
            {
                while (true)
                {
                    yield return new WaitForSeconds(moveDelay);
                    float distanceMoved = 0f;

                    while (distanceMoved < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                        distanceMoved += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    yield return new WaitForSeconds(moveDelay);
                    float distanceReturned = 0f;

                    while (distanceReturned < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                        distanceReturned += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    transform.position = originalPosition;
                    StopCoroutine(MoveRoutine());
                    yield return StartCoroutine(MoveRoutine());
                }
            }

            else if (reverse == false)
            {
                while (true)
                {
                    yield return new WaitForSeconds(moveDelay);
                    float distanceMoved = 0f;

                    while (distanceMoved < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
                        distanceMoved += moveSpeed * Time.deltaTime;
                        yield return null;
                    }

                    yield return new WaitForSeconds(moveDelay);
                    float distanceReturned = 0f;

                    while (distanceReturned < moveDistance)
                    {
                        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
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
}