using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    private readonly float spawnDelay = 1.0f;
    private float spawnTimer = 0.0f;
    private float random = 0.0f;

    public Transform[] spawnPoints;
    public GameObject objectsCivilianPrefab;
    public GameObject objectsPoliceOfficerPrefab;
    public Transform objectsStorage;

    List<GameObject> ObjectsCivilians = new List<GameObject>();
    List<GameObject> ObjectsPoliceOfficers = new List<GameObject>();

    private void Start()
    {
        CreateCivilians(5);
        CreatePoliceOfficer(5);
    }

    private void Update()
    {
        SpawnObjects();
    }

    private void CreateCivilians(int civilianCount)
    {
        for (int i = 0; i < civilianCount; i++)
        {
            GameObject civilian = Instantiate(objectsCivilianPrefab) as GameObject;
            civilian.transform.SetParent(objectsStorage);
            civilian.SetActive(false);
            ObjectsCivilians.Add(civilian);
        }
    }

    private void CreatePoliceOfficer(int policeOfficerCount)
    {
        for (int i = 0; i < policeOfficerCount; i++)
        {
            GameObject policeOfficer = Instantiate(objectsPoliceOfficerPrefab) as GameObject;
            policeOfficer.transform.SetParent(objectsStorage);
            policeOfficer.SetActive(false);
            ObjectsPoliceOfficers.Add(policeOfficer);
        }
    }

    private GameObject GetCivilian()
    {
        GameObject reqCivilian = null;

        for (int i = 0; i < ObjectsCivilians.Count; i++)
        {
            if (ObjectsCivilians[i].activeSelf == false)
            {
                reqCivilian = ObjectsCivilians[i];
                break;
            }
        }

        if (reqCivilian == null)
        {
            GameObject newCivilian = Instantiate(objectsCivilianPrefab) as GameObject;
            newCivilian.transform.SetParent(objectsStorage);
            ObjectsCivilians.Add(newCivilian);
            reqCivilian = newCivilian;
        }

        reqCivilian.SetActive(true);
        return reqCivilian;
    }

    private GameObject GetPoliceOfficer()
    {
        GameObject reqPoliceOfficer = null;

        for (int i = 0; i < ObjectsPoliceOfficers.Count; i++)
        {
            if (ObjectsPoliceOfficers[i].activeSelf == false)
            {
                reqPoliceOfficer = ObjectsPoliceOfficers[i];
                break;
            }
        }

        if (reqPoliceOfficer == null)
        {
            GameObject newPoliceOfficer = Instantiate(objectsPoliceOfficerPrefab) as GameObject;
            newPoliceOfficer.transform.SetParent(objectsStorage);
            ObjectsPoliceOfficers.Add(newPoliceOfficer);
            reqPoliceOfficer = newPoliceOfficer;
        }

        reqPoliceOfficer.SetActive(true);
        return reqPoliceOfficer;
    }

    private void SpawnObjects()
    {
        if (spawnTimer > spawnDelay)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[randomIndex];
            random = Random.Range(0, 4);

            if (random == 0 || random == 1 || random == 2)
            {
                GameObject objectsCivilian = GetCivilian();
                objectsCivilian.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
            }

            else if (random == 3)
            {
                GameObject objectsPoliceOfficer = GetPoliceOfficer();
                objectsPoliceOfficer.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
            }

            spawnTimer = 0.0f;
        }

        spawnTimer += Time.deltaTime;
    }
}