using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpeed : MonoBehaviour
{
    public float speed = 25f;
    public float force = 300.0f;

    private void Update()
    {
        Move();
    }

    private void Move()
    { 
        transform.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
    }

    IEnumerator MoveSpeed()
    {

        yield return new WaitForSeconds(10.0f);
    }
}
