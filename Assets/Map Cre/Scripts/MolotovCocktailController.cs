using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovCocktailController : MonoBehaviour
{
    private readonly float spawnDelay = 5.0f;
    private float spawnTimer = 0.0f;

    public Transform[] spawnPoints;
    public GameObject molotovCocktailPrefab;
    public Transform molotovCocktailStorage;

    List<GameObject> molotovCocktails = new List<GameObject>();

    private void Start()
    {
        CreateMolotovCocktails(5);
    }

    private void Update()
    {
        SpawnMolotovCocktails();
    }

    private void CreateMolotovCocktails(int molotovCocktailsCount)
    {
        for (int i = 0; i < molotovCocktailsCount; i++)
        {
            GameObject molotovCocktail = Instantiate(molotovCocktailPrefab) as GameObject;
            molotovCocktail.transform.SetParent(molotovCocktailStorage);
            molotovCocktail.SetActive(false);
            molotovCocktails.Add(molotovCocktail);
        }
    }

    private GameObject GetMolotovCocktail()
    {
        GameObject reqMolotovCocktail = null;

        for (int i = 0; i < molotovCocktails.Count; i++)
        {
            if (molotovCocktails[i].activeSelf == false)
            {
                reqMolotovCocktail = molotovCocktails[i];
                break;
            }
        }

        if (reqMolotovCocktail == null)
        {
            GameObject newMolotovCocktail = Instantiate(molotovCocktailPrefab) as GameObject;
            newMolotovCocktail.transform.SetParent(molotovCocktailStorage);
            molotovCocktails.Add(newMolotovCocktail);
            reqMolotovCocktail = newMolotovCocktail;
        }

        reqMolotovCocktail.SetActive(true);
        return reqMolotovCocktail;
    }

    private void SpawnMolotovCocktails()
    {
        if (spawnTimer > spawnDelay)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[randomIndex];
            GameObject molotovCocktail = GetMolotovCocktail();
            molotovCocktail.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
            spawnTimer = 0.0f;
        }

        spawnTimer += Time.deltaTime;
    }
}