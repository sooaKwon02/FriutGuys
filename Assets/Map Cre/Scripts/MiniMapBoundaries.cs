using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapBoundaries : MonoBehaviour
{
    [SerializeField]
    private Transform upperBoundary;

    [SerializeField]
    private Transform lowerBoundary;

    public Vector2 FindNormalizedPosition(Vector3 position)
    {
        float xPosition = Mathf.InverseLerp(lowerBoundary.position.x, upperBoundary.position.x, position.x);
        float zPosition = Mathf.InverseLerp(lowerBoundary.position.z, upperBoundary.position.z, position.z);
        return new Vector2(xPosition, zPosition);
    }
}