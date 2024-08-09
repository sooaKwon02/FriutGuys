using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBallController : MonoBehaviour
{
    private readonly float spawnDelay = 1.0f;
    private float spawnTimer = 0.0f;

    public Transform[] spawnPoints;
    public GameObject deathBallPrefab;
    public Transform deathBallStorage;

    List<GameObject> deathBalls = new List<GameObject>();

    private void Start()
    {
        CreateDeathBalls(3);
    }

    private void Update()
    {
        SpawnDeathBalls();
    }

    private void CreateDeathBalls(int deathBallCount)
    {
        for (int i = 0; i < deathBallCount; i++)
        {
            GameObject deathBall = Instantiate(deathBallPrefab) as GameObject;
            deathBall.transform.SetParent(deathBallStorage);
            deathBall.SetActive(false);
            deathBalls.Add(deathBall);
        }
    }

    private GameObject GetDeathBall()
    {
        GameObject reqDeathBall = null;

        for (int i = 0; i < deathBalls.Count; i++)
        {
            if (deathBalls[i].activeSelf == false)
            {
                reqDeathBall = deathBalls[i];
                break;
            }
        }

        if (reqDeathBall == null)
        {
            GameObject newDeathBall = Instantiate(deathBallPrefab) as GameObject;
            newDeathBall.transform.SetParent(deathBallStorage);
            deathBalls.Add(newDeathBall);
            reqDeathBall = newDeathBall;
        }

        reqDeathBall.SetActive(true);
        return reqDeathBall;
    }

    private void SpawnDeathBalls()
    {
        if (spawnTimer > spawnDelay)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[randomIndex];
            GameObject deathBall = GetDeathBall();
            deathBall.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
            deathBall.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            spawnTimer = 0.0f;
        }

        spawnTimer += Time.deltaTime;
    }
}