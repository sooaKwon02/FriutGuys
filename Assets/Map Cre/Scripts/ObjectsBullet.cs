using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsBullet : MonoBehaviour
{
    private ScoreController score;
    private GameObject hitAlarm;

    private readonly float moveSpeed = 10.0f;

    private void Awake()
    {
        score = GameObject.Find("UI Text Score").GetComponent<ScoreController>();
        hitAlarm = GameObject.Find("UI Text Hit Alarm");
    }

    private void Update()
    {
        float moveZ = moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, moveZ);
    }

    private void OnEnable()
    {
        Invoke(nameof(TimedBullet), 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitAlarm.SetActive(true);
            score.score -= 1;
            gameObject.SetActive(false);
        }
    }

    private void TimedBullet()
    {
        gameObject.SetActive(false);
    }
}