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
        List<int> Indexs = new List<int>();

        while (Indexs.Count < fallCubes)
        {
            int randomIndex = Random.Range(0, colliders.Length);

            if (!Indexs.Contains(randomIndex))
            {
                Indexs.Add(randomIndex);
            }
        }

        foreach (int index in Indexs)
        {
            Collider collider = colliders[index].GetComponent<Collider>();

            if (collider != null)
            {
                collider.isTrigger = true;

            }
        }

        foreach (int index in Indexs)
        {
            Animator animator = animators[index].GetComponent<Animator>();

            if (animator != null)
            {
                animator.enabled = true;
            }
        }
    }
}