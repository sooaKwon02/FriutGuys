using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    public static ExplosiveController instance;
    public GameObject explosivePrefab;

    List<GameObject> explosives = new List<GameObject>();

    private void Awake()
    {
        if (ExplosiveController.instance == null)
        {
            ExplosiveController.instance = this;
        }
    }

    private void Start()
    {
        CreateExplosives(5);
    }

    private void CreateExplosives(int explosiveCount)
    {
        for (int i = 0; i < explosiveCount; i++)
        {
            GameObject explosive = Instantiate(explosivePrefab) as GameObject;
            explosive.transform.parent = transform;
            explosive.SetActive(false);
            explosives.Add(explosive);
        }
    }

    public GameObject GetExplosive(Vector3 position)
    {
        GameObject reqExplosive = null;

        for (int i = 0; i < explosives.Count; i++)
        {
            if (explosives[i].activeSelf == false)
            {
                reqExplosive = explosives[i];
                break;
            }
        }

        if (reqExplosive == null)
        {
            GameObject newExplosive = Instantiate(explosivePrefab) as GameObject;
            newExplosive.transform.parent = transform;
            explosives.Add(newExplosive);
            reqExplosive = newExplosive;
        }

        reqExplosive.SetActive(true);
        reqExplosive.transform.position = position;
        return reqExplosive;
    }
}