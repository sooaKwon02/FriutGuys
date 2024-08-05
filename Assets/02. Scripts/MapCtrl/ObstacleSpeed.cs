using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpeed : MonoBehaviour
{
    public float speed = 25f;
    private float timer = 0.0f;

    private void Update()
    {
        Move();
    }

    private void Move()
    { 
        transform.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if(timer > 10.0f)
        {
            speed += 5.0f;
            timer = 0;
        }
    }
}
