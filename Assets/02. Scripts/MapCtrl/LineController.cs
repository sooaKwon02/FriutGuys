using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private Collider[] colliders;
    private Animator[] animators;

    private readonly int fallCubes = 5;

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        animators = GetComponentsInChildren<Animator>();
    }

    private void Start()
    {
        List<int> indexes = new List<int>();

        while (indexes.Count < fallCubes)
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
                collider.isTrigger = true;

            }
        }

        foreach (int index in indexes)
        {
            Animator animator = animators[index];

            if (animator != null)
            {
                animator.enabled = true;
            }
        }
    }
}