using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRandomize : MonoBehaviour
{
    private MeshRenderer[] meshRenderers;
    private Collider[] colliders;

    private readonly int removeObstacles = 4;

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    private void Start()
    {
        List<int> indexes = new List<int>();

        while (indexes.Count < removeObstacles)
        {
            int randomIndex = Random.Range(0, colliders.Length);

            if (!indexes.Contains(randomIndex))
            {
                indexes.Add(randomIndex);
            }
        }

        foreach (int index in indexes)
        {
            Collider collider = colliders[index];

            if (collider != null)
            {
                collider.enabled = false;
            }
        }

        foreach (int index in indexes)
        {
            MeshRenderer meshRenderer = meshRenderers[index];

            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }
    }
}